#pragma once
#include "stdafx.h"
#include <map>
#include <time.h>  
#include <Windows.h>
#include <wchar.h>

#define MAX_PROCESSES 32
#define MAX_THREADS_PER_PROCESS 128
#define MAX_MAPPED_FILES_PER_PROCESS 1024
#define MAX_DEFINED_FILES 1024

typedef struct ThreadData
{
	HANDLE handle;
	unsigned int tid;
}ThreadData;


typedef struct DefinedFile
{
	WCHAR* filename;
	unsigned int size;
	unsigned short* originalValues;
}DefinedFile;

typedef struct MappedFile
{
	void* baseAddress;
	DefinedFile* definition;
};

typedef struct ProcessData
{
	HANDLE proc;
	unsigned int pid;
	ThreadData* threads[MAX_THREADS_PER_PROCESS];
	unsigned int threadsLen = 0;
	MappedFile* files[MAX_MAPPED_FILES_PER_PROCESS];
	unsigned int filesLen = 0;
}ProcessData;


class Tracer
{
public:
	Tracer();
	bool start(WCHAR* name, WCHAR* commandline);
	bool loadBBfile(WCHAR* name);
	void setTime(int time);
	bool setSaveFile(WCHAR* name);
	bool setBaseDir(WCHAR* name);
private:
	void addProcess(WCHAR* filename, DWORD pid, HANDLE proc, DWORD tid, HANDLE thread, void* baseAddress);
	void addThread(DWORD pid, DWORD tid, HANDLE thread);
	void addMapped(DWORD pid, WCHAR* filename, void* baseAddress);

	void removeProcess(DWORD pid);
	void removeThread(DWORD pid, DWORD tid);
	void removeMapped(DWORD pid, void* baseAddress);

	bool exceptionHappened(DWORD pid, DWORD tid, void* location);

	void toLower(WCHAR* data);

	DefinedFile* getDefinedFile(WCHAR* name);
	ProcessData* getProc(unsigned int pid);
	ThreadData* getThread(ProcessData* proc, unsigned int tid);

	ProcessData* processes[MAX_PROCESSES];
	unsigned int processesLen;
	DefinedFile* definedFiles[MAX_DEFINED_FILES];
	unsigned int definedFilesLen;
	int maxTimeAfterUnique;
	int lastUniqueBlock;
	FILE *outputFile;
	WCHAR* baseDir;
	int baseDirLen;
};