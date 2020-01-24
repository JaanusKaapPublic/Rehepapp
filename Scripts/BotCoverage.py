from Libs.Web import downloadFile, doApiReq, getMyIp
from Libs.BasicBlockList import addBreakpoints
import time
import json
import getopt
import sys
import os
import array
import threading
import thread

class Coverage:
	project = None
	apiUrl = "http://127.0.0.1:8000/API/Coverage" 
	botName = None
	filesDir = None
	extension = None
	basicblockFile = None
	baseDir = None
	executable = None
	waitTime = 10
	downloadThreads = 8
	downloadLimit = 40
	commandsToRun = []
	logLevel = 1
	
	threadingLock = threading.Lock()	
	downloads = []

	def log(self, msg, level = 0):
		if level < self.logLevel:
			return
		if level == 0:
			print "[INFO-ALL]: %s" % msg
		if level == 1:
			print "[INFO-IMPORTANT]: %s" % msg
		elif level == 2:
			print "[WARNING]: %s" % msg
		elif level == 3:
			print "[ERROR]: %s" % msg
			
	def error(self, msg):
		self.log(msg, 3)
		exit(0)
	
	def runCommands(self):
		for cmd in self.commandsToRun:
			os.system(cmd)
					
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

			
	def getCoverageTarget(self):
		while True:
			try:
				self.log("Asking coverage target", 0)
				(success, data) = doApiReq("%s/getTestcase?code=%s&project=%s" % (self.apiUrl, self.botName, self.project))
				if not success:
					return None
			
				coverage = {
					'id': data['id'],
					'hash': data['hash'],
					'location': data['url']
				}
				return coverage
			except:
				self.log("Failed to ask coverage target", 1)
				return None
		
			
	def generateDefaultName(self):
		ipStr = getMyIp()
		if ipStr is None:
			self.botName = None
		else:
			self.botName = "COMPUTER_" + ipStr
			
	def startTracer(self, fname):
		print "Tracer.exe %s %d temp.bbc \"%s\" \"%s\" \"%s\" > log.txt" % (self.basicblockFile, self.waitTime, self.baseDir, self.executable, fname)
		os.system("Tracer.exe %s %d temp.bbc \"%s\" \"%s\" \"%s\" > log.txt" % (self.basicblockFile, self.waitTime, self.baseDir, self.executable, fname))
		
	def sendCoverage(self, testcaseId):
		f = open('temp.bbc', 'rb')
		content = f.read()
		content = array.array('B', content)
		f.close()
		
		pos = 3
		nrOfBBs = 0
		modules = {}
		recordedModules = {}
		while pos<len(content):
			if content[pos] == 0x1:
				pos += 1
				pid = int(content[pos]) + int(content[pos + 1]) * 0x100 + int(content[pos + 2]) * 0x10000 + int(content[pos + 3]) * 0x1000000
				pos += 4
				id = content[pos] + (pid * 1000)
				pos += 1
				moduleName = ""
				while content[pos] != 0x00:
					moduleName += chr(content[pos])
					pos += 1
				pos += 1
				recordedModules[id] = moduleName
				if moduleName not in modules:
					modules[moduleName] = []
			elif content[pos] == 0x2:	
				pos += 1
				pid = int(content[pos]) + int(content[pos + 1]) * 0x100 + int(content[pos + 2]) * 0x10000 + int(content[pos + 3]) * 0x1000000
				pos += 4
				id = content[pos] + (pid * 1000)
				pos += 1
				rva = int(content[pos]) + int(content[pos + 1]) * 0x100 + int(content[pos + 2]) * 0x10000 + int(content[pos + 3]) * 0x1000000
				pos += 4
				moduleName = recordedModules[id]
				if rva not in modules[moduleName]:
					modules[moduleName].append(rva)	
					nrOfBBs += 1
		result = {}
		result['client'] = self.botName
		result['id'] = testcaseId
		result['coverage'] = []

		for moduleName in modules:
			moduleObj = {}
			moduleObj['name'] = moduleName
			moduleObj['basicblocks'] = modules[moduleName]
			result['coverage'].append(moduleObj)
		
		self.log("Reporting %d modules and %d blocks" % (len(modules), nrOfBBs), 1)
		while True:
			start = time.time()
			(success, data) = doApiReq("%s/addCoverage" % (self.apiUrl), json.dumps(result))
			end = time.time()
			if success:
				self.log("Coverage sent in %d seconds" % (end-start), 1)
				break
			self.log("Failed to send coverage: %s" % (data), 2)
			time.sleep(39)
		
	def doCoverage(self):
		try:
			id = None
			fname = None
			while id is None:
				self.threadingLock.acquire()
				if len(self.downloads) > 0:
					(id, fname) = self.downloads.pop(0)
					self.threadingLock.release()
				else:
					self.threadingLock.release()
					time.sleep(2)

			if fname is not None and not os.path.isfile(fname):
				fname = None
					
			if fname is None:
				self.log("*Non existing file", 1)
				open('temp.bbc', 'wb').close()
			else:
				self.log("*" + fname, 1)
				self.startTracer(fname)
			self.log("*Sending coverage", 0)
			time1 = time.time()
			self.sendCoverage(id)
			time2 = time.time()
			self.log("*Sending ended: %f" % (time2-time1), 0)
			time.sleep(1)
			if fname is not None:
				if os.path.isfile(fname):
					os.remove(fname)
			if os.path.isfile('temp.bbc'):
				os.remove('temp.bbc')
			
		except Exception:
			raise
			
	def doDownload(self, id, hash, location):
		try:
			data = downloadFile(location)

			if data is not None:
				fname = self.filesDir + hash + "." + self.extension
				f = open(fname, "wb")
				f.write(data)
				f.close()
				self.threadingLock.acquire()
				self.downloads.append((id, fname))
				self.threadingLock.release()
				self.log("Succeeded to download file: %s" % location, 1)
		except:
			self.log("Failed to download file: %s" % location, 1)
			self.threadingLock.acquire()
			self.downloads.append((id, None))
			self.threadingLock.release()
		
	def downloadingThread(self):
		while True:
			self.threadingLock.acquire()
			if len(self.downloads) >= self.downloadLimit:
				self.threadingLock.release()
				time.sleep(10)
				continue
			self.threadingLock.release()
			data = self.getCoverageTarget()	
			if data is None:
				time.sleep(30)
				continue
			self.doDownload(data['id'], data['hash'], data['location'])			
		
	def start(self):
		if self.botName is None:
			self.generateDefaultName()
			if self.botName is None:
				self.botName = self.askName()
			self.log("Got myself a name '%s'" % self.botName, 1)
		else:
			self.registerName()
			self.log("Already had name %s" % self.botName, 1)

		addBreakpoints(self.basicblockFile, self.baseDir)
		
		for x in xrange(self.downloadThreads):
			thread.start_new_thread(self.downloadingThread, ())
		
		while True:
			self.doCoverage()
			
	def loadConf(self):
		data = json.load(open("coverage.conf", "rb"))
		
		if "url" in data:
			self.apiUrl = data["url"]
		else:
			if "host" in data:
				self.apiUrl = "http://%s:8000/API/Coverage" % data["host"]
				
		if "project" in data:
			self.project = data["project"]
		else:
			error("No project specified")
			
		if "extension" in data:
			self.extension = data["extension"]
		else:
			error("No extension specified")
			
		if "tmpDir" in data:
			self.filesDir = data["tmpDir"]
			if self.filesDir[-1] != "\\" and self.filesDir[-1] != "/":
				self.filesDir += "\\" 
		else:
			error("No tmpDir specified")
			
		if "codeblocks" in data:
			self.basicblockFile = data["codeblocks"]
		else:
			error("No codeblocks specified")
			
		if "executable" in data:
			self.executable = data["executable"]
		else:
			error("No executable specified")
			
		if "baseDir" in data:
			self.baseDir = data["baseDir"]
			while self.baseDir[-1] == "\\" and self.baseDir[-1] == "/":
				self.baseDir = self.baseDir[0:-1]
		else:
			error("No baseDir specified")
						
		if "waitTime" in data:
			self.waitTime = data["waitTime"]
		if "botName" in data:
			self.botName = data["botName"]			
		if "preCmds" in data:
			for cmd in data["preCmds"]:
				self.commandsToRun.append(cmd)
		
	
obj = Coverage()
obj.loadConf()				
obj.start()