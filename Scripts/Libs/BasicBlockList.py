import struct
import shutil
import os

def addBreakpoints(basicbloockFile, baseDir):
	if not baseDir.endswith("\\"):
		baseDir = baseDir + "\\"

	f_bbl = open(basicbloockFile, "rb")
	if f_bbl.read(3) != "BBL":
		return "Basicblock file does not have correct magic value"

	modified_files = []
	try:
		while True:
			tmp = f_bbl.read(4)
			if len(tmp) < 4:
				break
			filenameLen = struct.unpack("<I", tmp)[0]
			filename = f_bbl.read(filenameLen)
			f_bbl.read(4)

			modified_files.append(baseDir + filename)
			if os.path.isfile(baseDir + filename + ".backup"):
				shutil.move(baseDir + filename + ".backup", baseDir + filename)			
			shutil.copyfile(baseDir + filename, baseDir + filename + ".backup_tmp")
			f = file(baseDir + filename, 'r+b')
			while True:
				(rva, offset, value) = struct.unpack("<IIB", f_bbl.read(9))
				if rva == 0:
					break
				f.seek(offset)
				tmp = ord(f.read(1))
				if tmp != value:
					f.close()
					raise Exception("File %s seems to not be same as deffined in basicblocks file: 0x%02X vs 0x%02X" % (baseDir + filename, tmp, value))
				f.seek(offset)
				f.write(chr(0xCC))
			f.close()
		
		for modified_file in modified_files:
			shutil.move(modified_file + ".backup_tmp", modified_file + ".backup")
	except:
		for modified_file in modified_files:
			os.remove(modified_file)
			shutil.move(modified_file + ".backup_tmp", modified_file)
		raise

