from idautils import *
from idaapi import *
import idc
import struct
import os

def isThereCode():
	for segment_ea in Segments():
		segment = idaapi.getseg(segment_ea)
		if segment.perm & idaapi.SEGPERM_EXEC == 0:
			continue
		for location in Functions(SegStart(segment.startEA), SegEnd(segment.startEA)):
			for block in idaapi.FlowChart(idaapi.get_func(location)):
				if GetMnem(block.startEA) != "":
					return True
	return False


#Wait for analysis to end
autoWait()

#Is there even code in this file
if not isThereCode():
	print "NO CODE!"
	idc.Exit(0)

#Command line stuff
basicBlockFile = idc.ARGV[1]
baseDir = idc.ARGV[2].lower()
isNewFile = not os.path.isfile(basicBlockFile)
if not baseDir.endswith("\\"):
	baseDir += "\\"
filename = GetInputFilePath().lower().replace(get_root_filename().lower(), "").replace(baseDir, "") + get_root_filename()

#Initial stuff
base = idaapi.get_imagebase()
allBlocks = {}
BBcount = 0
Fcount = 0

#Where we write
file = open(basicBlockFile, 'ab')
if isNewFile:
	file.write("BBL")
file.write(struct.pack("<I", len(filename)))
file.write(filename)

#Size of the file virtual space
lastEA = base
for segment_ea in Segments():
	segment = idaapi.getseg(segment_ea)
	if SegEnd(segment_ea) > lastEA:
		lastEA = SegEnd(segment_ea)
file.write(struct.pack("<I", lastEA - base))
	
#Main work
for segment_ea in Segments():
	segment = idaapi.getseg(segment_ea)
	if segment.perm & idaapi.SEGPERM_EXEC == 0:
		continue
	
	for location in Functions(SegStart(segment.startEA), SegEnd(segment.startEA)):
		Fcount += 1
		blocks = idaapi.FlowChart(idaapi.get_func(location))
		for block in blocks:
			BBcount += 1
			if block.startEA not in allBlocks:
				if GetMnem(block.startEA) == "":
					break
				file.write(struct.pack("<IIB", block.startEA - base, idaapi.get_fileregion_offset(block.startEA), idaapi.get_byte(block.startEA)))
				allBlocks[block.startEA] = True
file.write(struct.pack("<IIB", 0, 0, 0))
file.close()
print "Discovered %d basic blocks in %d functions" % (BBcount, Fcount)
idc.Exit(0)