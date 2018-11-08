from Libs.Google import doGoogleSearch
from Libs.Web import downloadFile, doApiReq, getMyIp
import time
import md5
import thread
import threading
import getopt
import sys

class FileCollector:
	apiUrl = "http://127.0.0.1:8000/API/Collecter"
	botName = None
	googleDomain = "ee"
	timeBetweenSearches = 45
	maxThreads = 8
	maxSize = 10 * 1024 * 1024
	logLevel = 1

	threadingLock = threading.Lock()	
	searchesList = []
	lastSearch = 0
	sentFiles = 0

	def log(self, msg, level = 0):
		if level < self.logLevel:
			return
		if level == 0:
			print "[INFO-ALL]: %s" % msg
		if level == 1:
			print "[INFO-IMPORTANT]: %s" % msg
		elif level == 2:
			print "[WARNING]: %s" % msg

	def error(self, msg):
		print "[ERROR]: %s" % msg
		exit()

	
	def downloadFile(self, project, magicNumber, url):
		while True:
			self.threadingLock.acquire()
			if len(self.searchesList) < self.maxThreads*2:
				self.searchesList.append((project, magicNumber, url))
				self.threadingLock.release()
				break
			self.threadingLock.release()
			time.sleep(1)
	
	def downloadingThread(self):
		while True:
			self.threadingLock.acquire()
			if len(self.searchesList)>0:
				(project, magicNumber, url) = self.searchesList.pop(0)
				self.threadingLock.release()
				self.downloadFileActual(project, magicNumber, url)
				continue
			self.threadingLock.release()
			time.sleep(1)

	def downloadFileActual(self, project, magicNumber, url):
		self.log("Starting to download from: %s" % url)
		entireFile = downloadFile(url, magicNumber, self.maxSize)
		if entireFile is not None:
			m = md5.new()
			m.update(entireFile)
			hashStr = m.hexdigest()
			self.sendFile(project, hashStr, url, len(entireFile))
		return True
			
	def sendFile(self, project, hash, url, size):
		self.log("Sending info about file: %s" % url)
		(success, data) = doApiReq("%s/addTestcase" % self.apiUrl, {'project_id': project, 'hash': hash, 'url': url, 'size': str(size)})
		
	def singleLoop(self, project, extension, magicNumber, searchTerm):
		self.log("Starting searching with term '%s' for extension '%s'" % (searchTerm, extension), 1)
		for nr in xrange(5):
			if time.time()-self.lastSearch < self.timeBetweenSearches:
				toSleep = self.timeBetweenSearches + 1 - (time.time()-self.lastSearch)
				self.log("Sleeping %d seconds before new search" % toSleep, 1)
				time.sleep(toSleep)

			self.log("googleSearch('%s', %d, %d)" % (searchTerm, nr * 100, 100), 1)
			locations = doGoogleSearch("filetype%3A" + extension + " " + searchTerm, nr * 100, 100, self.googleDomain)
			self.log("found %d links" %  len(locations), 1)
			self.lastSearch = time.time()
			if len(locations) == 0:
				break
			for url in locations:
				self.downloadFile(project, magicNumber, url)
				
	def singlePrefix(self, project, extension, magicNumber, prefix):
		self.log("Starting searching with prefix '%s' for extension '%s'" % (prefix, extension), 1)
		for x in xrange(26):
			self.singleLoop(project, extension, magicNumber, prefix + chr(ord('A') + x))	
			
	def askName(self):
		self.log("Asking name from the server", 1)
		(success, data) = doApiReq("%s/getName" % self.apiUrl)
		if not success:
			self.error("Could not get client code - error from server side: " + data)
		return data

		
	def registerName(self):
		self.log("Registering name '%s' to the server" % self.botName, 1)
		(success, data) = doApiReq("%s/registerBot?code=%s" % (self.apiUrl, self.botName))
		if not success:
			self.error("Could not registerg client code: " + data)
			
	def getOrders(self):
		self.log("Asking orders from the server", 1)
		(success, data) = doApiReq("%s/getRange?code=%s" % (self.apiUrl, self.botName))
		if not success:
			return None
			
		conf = {
			'project': data['project_id'],
			'extension': data['extension'],
			'prefix': data['search_str'],
			'magic': data['magic'].decode("hex")
		}
		return conf
		
	def markDone(self, project, prefix):
		self.log("Ending search range for '%s'" % prefix, 1)
		(success, data) = doApiReq("%s/endRange?code=%s&search_str=%s&project_id=%s" % (self.apiUrl, self.botName, prefix, project))
		if not success:
			self.log("Could not end search range: " + data, 2)
			return False
		return True
			
	def generateDefaultName(self):
		ipStr = getMyIp()
		if ipStr is None:
			self.botName = None
		else:
			self.botName = "COMPUTER_" + ipStr

			
	def start(self):
		if self.botName is None:
			if self.botName is None:
				self.botName = self.askName()
			self.log("Got myself a name '%s'" % self.botName, 1)
		else:
			self.registerName()
			self.log("Have already a name '%s'" % self.botName, 1)
			
		for x in xrange(self.maxThreads):
			thread.start_new_thread(self.downloadingThread, ())
			
		while True:
			order = self.getOrders()
			if order is None:
				self.log("No commands for me :(", 2)
				time.sleep(8)
				continue
			self.singlePrefix(order['project'], order['extension'], order['magic'], order['prefix'])
			self.markDone(order['project'], order['prefix'])
		
	
obj = FileCollector()
	
try:
	opts, args = getopt.getopt(sys.argv[1:], "u:p:t:s:g:n:l:", ["url=", "threads=", "sleep=", "google=", "name=", "log="])
except getopt.GetoptError as err:
	print str(err)
	exit()
	
for o, a in opts:
	if o in ("-u", "--url"):
		obj.apiUrl = a
	elif o in ("-t", "--threads"):
		obj.maxThreads = int(a)
	elif o in ("-s", "--sleep"):
		obj.timeBetweenSearches = int(a)
	elif o in ("-g", "--google"):
		obj.googleDomain = a
	elif o in ("-n", "--name"):
		obj.botName = a
	elif o in ("-l", "--log"):
		obj.logLevel = int(a)

if obj.botName is None:
	obj.generateDefaultName()
				
obj.start()