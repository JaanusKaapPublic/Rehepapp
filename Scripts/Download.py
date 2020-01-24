from Libs.Google import doGoogleSearch
from Libs.Web import downloadFile, doApiReq, getMyIp
import time
import md5
import thread
import threading
import getopt
import sys

class FileDownloader:
	apiUrl = "http://127.0.0.1:8000/API/Collecter"
	output = ".\\out"
	project = "XXX"
	skip = 0
	count = 100
	logLevel = 1
	maxThreads = 8	
	ext = "xxx"

	threadingLock = threading.Lock()	
	searchesList = []
	done = 0

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
	
	def downloadingThread(self):
		while True:
			self.threadingLock.acquire()
			if len(self.searchesList)>0:
				file = self.searchesList.pop(0)
				self.log("Starting to download from: %s" % file['url'])
				self.threadingLock.release()
				self.downloadFileActual(file['url'], file['hash'])
				self.threadingLock.acquire()
				self.done += 1
				self.threadingLock.release()
				continue
			self.threadingLock.release()
			break

	def downloadFileActual(self, url, hash):
		entireFile = downloadFile(url)
		if entireFile is None or len(entireFile) == 0:
			return False
		f = open("%s\\%s.%s" % (self.output, hash, self.ext), "wb")
		f.write(entireFile)
		f.close()
		return True
		
	def downloadList(self):
		(success, data) = doApiReq("%s/getLinks?project=%s&skip=%d&count=%d" % (self.apiUrl, self.project, self.skip, self.count))
		if(success):
			self.count = len(data)
			self.searchesList = data
			
	def start(self):			
		self.downloadList()
		for x in xrange(self.maxThreads-1):
			thread.start_new_thread(self.downloadingThread, ())
		self.downloadingThread()
		
		while True:
			self.threadingLock.acquire()
			if self.done == self.count:
				self.threadingLock.release()
				break
			self.threadingLock.release()
			time.sleep(1)
		time.sleep(4)       
		
	
obj = FileDownloader()
	
try:
	opts, args = getopt.getopt(sys.argv[1:], "u:p:t:o:s:c:l:e:", ["url=", "project=", "threads=", "output=", "skip=", "count=", "log=", "ext="])
except getopt.GetoptError as err:
	print str(err)
	exit()
	
for o, a in opts:
	if o in ("-u", "--url"):
		obj.apiUrl = a
	elif o in ("-p", "--project"):
		obj.project = a
	elif o in ("-t", "--threads"):
		obj.maxThreads = int(a)
	elif o in ("-o", "--output"):
		obj.output = a
	elif o in ("-s", "--skip"):
		obj.skip = int(a)
	elif o in ("-c", "--count"):
		obj.count = int(a)
	elif o in ("-l", "--log"):
		obj.logLevel = int(a)
	elif o in ("-e", "--ext"):
		obj.ext = a
				
obj.start()
print "Done"