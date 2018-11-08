import urllib2
import requests
import json
import socket
import ssl

ctx = ssl.create_default_context()
ctx.check_hostname = False
ctx.verify_mode = ssl.CERT_NONE

def downloadFile(url, magicNumber = None, maxSize = 10 * 1024 * 1024):
	try:
		f = urllib2.urlopen(url, timeout=10, context=ctx)

		if magicNumber is None:
			entireFile = f.read(maxSize + 1)
		else:
			if f.read(len(magicNumber)) != magicNumber:
				f.close()
				return None
			entireFile = magicNumber + f.read(maxSize + 1)
		f.close()

		if len(entireFile) > maxSize:
			return None
		return entireFile
	except:
		return None

def doApiReq(url, post = None):
	try:
		if post is None:
			r = requests.get(url)
		else:
			r = requests.post(url, data=post)
		#print url
		#print post
		#print r.text
		data = json.loads(r.text)
		if data['error'] != 0 or 'data' not in data:
			if 'debug' in data:
				return (False, data['message'] + ' --- ' + data['debug'])
			else:
				return (False, data['message'])
		else:
			return (True, data['data'])
	except:
		raise
		return (False, "Connection exception")

def getMyIp():
	try:
		s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
		s.connect(("8.8.8.8", 80))
		ipStr = s.getsockname()[0]
		s.close()
		return ipStr
	except:
		pass
	return None