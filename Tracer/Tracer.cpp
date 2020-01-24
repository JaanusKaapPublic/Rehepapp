#include "stdafx.h"
#include "Tracer.h"
#include <atlbase.h>
#include <string>


void Tracer::toLower(WCHAR* data)
{
	for (int x = 0; x < wcslen(data); x++)
		data[x] = towlower(data[x]);
}

Tracer::Tracer()
{
	processesLen = 0;
	definedFilesLen = 0;
	maxTimeAfterUnique = 0;
}

ProcessData* Tracer::getProc(unsigned int pid)
{
	for (unsigned int x = 0; x < processesLen; x++)
		if (processes[x]->pid == pid)
			return processes[x];
	return NULL;
}

ThreadData* Tracer::getThread(ProcessData* proc, unsigned int tid)
{
	for (unsigned int x = 0; x < proc->threadsLen; x++)
		if (proc->threads[x]->tid == tid)
			return proc->threads[x];
	return NULL;
}

void Tracer::addProcess(WCHAR* filename, DWORD pid, HANDLE proc, DWORD tid, HANDLE thread, void* baseAddress)
{
	toLower(filename);
	printf("[INFO] New process(%d) started with thread %d\n", pid, tid);
	ProcessData *newProc = new ProcessData;

	ZeroMemory(newProc->threads, sizeof(ProcessData::threads));
	ZeroMemory(newProc->files, sizeof(ProcessData::files));
	newProc->pid = pid;
	newProc->proc = proc;
	newProc->threadsLen = 0;
	newProc->filesLen = 0;

	processes[processesLen++] = newProc;
	addThread(pid, tid, thread);
	addMapped(pid, filename, baseAddress);
}


void Tracer::addThread(DWORD pid, DWORD tid, HANDLE thread)
{
	printf("[INFO] New thread(%d) to process %d\n", tid, pid);
	ThreadData *newThread = new ThreadData;
	newThread->handle = thread;
	newThread->tid = tid;

	ProcessData* proc = getProc(pid);
	proc->threads[proc->threadsLen++] = newThread;
}

void Tracer::addMapped(DWORD pid, WCHAR* filename, void* baseAddress)
{
	if (!memcmp(filename, L"\\\\?\\", 8))
		filename += 4;
	toLower(filename);
	DefinedFile* definedFile = getDefinedFile(filename);
	if (!definedFile)
		return;
	printf("[INFO] Image with BB information %S loaded to process(%d) at %016X\n", filename, pid, baseAddress);

	ProcessData* proc = getProc(pid);
	MappedFile* mappedFile = new MappedFile;
	mappedFile->baseAddress = baseAddress;
	mappedFile->definition = definedFile;
	proc->files[proc->filesLen++] = mappedFile;

	char tmpStr[128];
	tmpStr[0] = 0x1;
	fwrite(tmpStr, 1, 1, outputFile);

	fwrite(&pid, 4, 1, outputFile);

	tmpStr[0] = proc->filesLen - 1;
	fwrite(tmpStr, 1, 1, outputFile);
	
	sprintf(tmpStr, "%S", filename);
	fwrite(tmpStr + baseDirLen, 1, strlen(tmpStr) + 1 - baseDirLen, outputFile);
}

void Tracer::removeProcess(DWORD pid)
{
	printf("[INFO] Stopped process(%d)\n", pid);
	for (unsigned int x = 0; x < processesLen; x++)
	{
		if (processes[x]->pid == pid)
		{
			for (unsigned int y = 0; y < processes[x]->threadsLen; y++)
				delete processes[x]->threads[y];
			for (unsigned int y = 0; y < processes[x]->filesLen; y++)
				delete processes[x]->files[y];

			delete processes[x];
			for (unsigned int y = x+1; y < processesLen; y++)
				processes[y-1] = processes[y];
			processesLen--;
			break;
		}
	}
}

void Tracer::removeThread(DWORD pid, DWORD tid)
{
	printf("[INFO] Stopped thread(%d) in process(%d)\n", tid, pid);
	ProcessData* proc = getProc(pid);
	for (unsigned int x = 0; x < proc->threadsLen; x++)
	{
		if (proc->threads[x]->tid == tid)
		{
			delete proc->threads[x];
			for (unsigned int y = x + 1; y < proc->threadsLen; y++)
				proc->threads[y - 1] = proc->threads[y];
			proc->threadsLen--;
			break;
		}
	}
}

void Tracer::removeMapped(DWORD pid, void* baseAddress)
{
	ProcessData* proc = getProc(pid);
	for (unsigned int x = 0; x < proc->filesLen; x++)
	{
		if (proc->files[x]->baseAddress == baseAddress)
		{
			printf("[INFO] Removed image %S loaded to process(%d)\n", proc->files[x]->definition->filename, pid);
			delete proc->files[x];
			for (unsigned int y = x + 1; y < proc->filesLen; y++)
				proc->files[y - 1] = proc->files[y];
			proc->filesLen--;
			break;
		}
	}
}

bool Tracer::loadBBfile(WCHAR* name)
{
	FILE* f = _wfopen(name, L"rb");
	fpos_t pos;
	if (fgetc(f) != 'B' || fgetc(f) != 'B' || fgetc(f) != 'L')
	{
		printf("[ERROR] File %S is not correct Basic Block List file!\n");
		exit(1);
	}
	while (true)
	{
		char* fname;
		WCHAR* fnameW;
		int tmp;
		fread(&tmp, sizeof(tmp), 1, f);
		if (feof(f))
			break;
		fname = new char[tmp + 1];
		fread(fname, sizeof(char), tmp, f);
		fname[tmp] = 0x00;
		fnameW = new WCHAR[tmp + baseDirLen + 1];
		memcpy(fnameW, baseDir, baseDirLen*2);
		mbstowcs(fnameW + baseDirLen, fname, tmp + 1);
		toLower(fnameW);
		fread(&tmp, sizeof(tmp), 1, f);

		DefinedFile* defFile = new DefinedFile;
		defFile->filename = fnameW;
		defFile->size = tmp;
		defFile->originalValues = new unsigned short[tmp];
		if (!defFile->originalValues)
		{
			printf("[ERROR] Could not allocate %d sized buffer\n", tmp);
			exit(1);
		}

		for (int x = 0; x < tmp; x++)
			defFile->originalValues[x] = 0x00;

		while (true)
		{
			unsigned int rva, offset;
			unsigned char val;
			fread(&rva, 4, 1, f);
			fread(&offset, 4, 1, f);
			fread(&val, 1, 1, f);
			
			if (!rva && !offset && !val)
				break;

			defFile->originalValues[rva] = 0x100 | val;
		}
		definedFiles[definedFilesLen++] = defFile;
		printf("[INFO] Loaded breakpoint data about file %S\n", fnameW);
		fgetpos(f, &pos);
		if (fgetc(f) != 'B' || fgetc(f) != 'B' || fgetc(f) != 'L') //Bypass one stupid problem, will fix at some point
			fsetpos(f, &pos);
	}

	return true;
}

DefinedFile* Tracer::getDefinedFile(WCHAR* name)
{
	for (int x = 0; x < definedFilesLen; x++)
	{
		if (!wcscmp(name, definedFiles[x]->filename))
			return definedFiles[x];
	}
	return NULL;
}

bool Tracer::exceptionHappened(DWORD pid, DWORD tid, void* location)
{
	ProcessData* proc = getProc(pid);
	for (int x = 0; x < proc->filesLen; x++)
	{
		if (proc->files[x]->baseAddress < location && (char*)proc->files[x]->baseAddress + proc->files[x]->definition->size > location)
		{
			unsigned int rva = (unsigned int)((char*)location - proc->files[x]->baseAddress);
			unsigned short val = proc->files[x]->definition->originalValues[rva];
			if (!(val & 0x100))
				return false;
			if (!(val & 0x200))
			{
				lastUniqueBlock = time(NULL);
				proc->files[x]->definition->originalValues[rva] |= 0x200;
				char tmp;
				tmp = 0x2;
				fwrite(&tmp, 1, 1, outputFile);
				fwrite(&pid, 4, 1, outputFile);
				tmp = x;
				fwrite(&tmp, 1, 1, outputFile);
				fwrite(&rva, 4, 1, outputFile);
			}

			unsigned char originalValue = val & 0xFF;
			CONTEXT context;
			context.ContextFlags = CONTEXT_CONTROL;
			HANDLE threadH = getThread(proc, tid)->handle;
			if (!GetThreadContext(threadH, &context))
			{
				printf("[ERROR] GetThreadContext for thread handle %d did not work: %d\n", getThread(proc, tid)->handle, GetLastError());
				exit(1);
			}
#ifdef _WIN64
			context.Rip--;
			if (!WriteProcessMemory(proc->proc, (void*)context.Rip, &originalValue, 1, NULL))
			{
				printf("[ERROR] WriteProcessMemory did not work\n");
					exit(1);
			}
			if (!FlushInstructionCache(proc->proc, (void*)context.Rip, 1))
			{
				printf("[ERROR] FlushInstructionCache failed\n");
				exit(1);
			}
#else
			context.Eip--;
			if (!WriteProcessMemory(proc->proc, (void*)context.Eip, &originalValue, 1, NULL))
			{
				printf("[ERROR] WriteProcessMemory did not work\n");
				exit(1);
			}
			if (!FlushInstructionCache(proc->proc, (void*)context.Eip, 1))
			{
				printf("[ERROR] FlushInstructionCache failed\n");
				exit(1);
			}
#endif
			if (!SetThreadContext(threadH, &context))
			{
				printf("[ERROR] SetThreadContext for thread handle %d did not work: %d\n", getThread(proc, tid)->handle, GetLastError());
				exit(1);
			}
			return true;
		}
	}
	return false;
}

void Tracer::setTime(int time)
{
	maxTimeAfterUnique = time;
}

bool Tracer::setSaveFile(WCHAR* name)
{
	outputFile = _wfopen(name, L"wb");
	fwrite("BBC", 1, 3, outputFile);
	if (!outputFile)
	{
		printf("[ERROR] Could not create file '%S'\n", name);
		exit(1);
	}
	return true;
}

bool Tracer::setBaseDir(WCHAR* name)
{
	baseDirLen = wcslen(name);
	baseDir = name;
	return true;
}

bool Tracer::start(WCHAR* name, WCHAR* commandline)
{
	STARTUPINFO startupInfo;
	PROCESS_INFORMATION ProcessInformation;
	bool started = false;

	ZeroMemory(&startupInfo, sizeof(startupInfo));
	ZeroMemory(&ProcessInformation, sizeof(ProcessInformation));
	startupInfo.cb = sizeof(startupInfo);

	if (!CreateProcess(name, commandline, 0, 0, true, DEBUG_PROCESS, 0, 0, &startupInfo, &ProcessInformation))
	{
		printf("[ERROR] Could not create process!");
		exit(1);
	}
	lastUniqueBlock = time(NULL);

	while (!started || processesLen > 0)
	{
		DEBUG_EVENT DebugEvent;
		DWORD status = DBG_EXCEPTION_NOT_HANDLED;

		if (maxTimeAfterUnique != 0 && lastUniqueBlock + maxTimeAfterUnique < time(NULL))
		{
			break;
		};

		if (!WaitForDebugEvent(&DebugEvent, 1000))
		{
			if (maxTimeAfterUnique != 0 && lastUniqueBlock + maxTimeAfterUnique < time(NULL))
			{
				break;
			};
			continue;
		}

		switch (DebugEvent.dwDebugEventCode)
		{
		case EXCEPTION_DEBUG_EVENT:
			if (DebugEvent.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT)
			{
				if (exceptionHappened(DebugEvent.dwProcessId, DebugEvent.dwThreadId, DebugEvent.u.Exception.ExceptionRecord.ExceptionAddress))
					status = DBG_CONTINUE;
			}
			/*
			else
			{
				printf("[INFO] EXCEPTION-CODE: %d (0x%X) at %08X --- lastUniqueBlock=%d  maxTimeAfterUnique=%d  time=%d\n", DebugEvent.u.Exception.ExceptionRecord.ExceptionCode, DebugEvent.u.Exception.ExceptionRecord.ExceptionCode, DebugEvent.u.Exception.ExceptionRecord.ExceptionAddress, lastUniqueBlock, maxTimeAfterUnique, time(NULL));
				switch (DebugEvent.u.Exception.ExceptionRecord.ExceptionCode)
				{
				case EXCEPTION_ACCESS_VIOLATION:
					printf("        |-EXCEPTION_ACCESS_VIOLATION\n");
					break;
				case EXCEPTION_ARRAY_BOUNDS_EXCEEDED:
					printf("        |-EXCEPTION_ARRAY_BOUNDS_EXCEEDED\n");
					break;
				case EXCEPTION_DATATYPE_MISALIGNMENT:
					printf("        |-EXCEPTION_DATATYPE_MISALIGNMENT\n");
					break;
				case EXCEPTION_FLT_DENORMAL_OPERAND:
					printf("        |-EXCEPTION_FLT_DENORMAL_OPERAND\n");
					break;
				case EXCEPTION_FLT_DIVIDE_BY_ZERO:
					printf("        |-EXCEPTION_FLT_DIVIDE_BY_ZERO\n");
					break;
				case EXCEPTION_FLT_INEXACT_RESULT:
					printf("        |-EXCEPTION_FLT_INEXACT_RESULT\n");
					break;
				case EXCEPTION_FLT_INVALID_OPERATION:
					printf("        |-EXCEPTION_FLT_INVALID_OPERATION\n");
					break;
				case EXCEPTION_FLT_OVERFLOW:
					printf("        |-EXCEPTION_FLT_OVERFLOW\n");
					break;
				case EXCEPTION_FLT_STACK_CHECK:
					printf("        |-EXCEPTION_FLT_STACK_CHECK\n");
					break;
				case EXCEPTION_FLT_UNDERFLOW:
					printf("        |-EXCEPTION_FLT_UNDERFLOW\n");
					break;
				case EXCEPTION_ILLEGAL_INSTRUCTION:
					printf("        |-EXCEPTION_ILLEGAL_INSTRUCTION\n");
					break;
				case EXCEPTION_IN_PAGE_ERROR:
					printf("        |-EXCEPTION_IN_PAGE_ERROR\n");
					break;
				case EXCEPTION_INT_DIVIDE_BY_ZERO:
					printf("        |-EXCEPTION_INT_DIVIDE_BY_ZERO\n");
					break;
				case EXCEPTION_INT_OVERFLOW:
					printf("        |-EXCEPTION_INT_OVERFLOW\n");
					break;
				case EXCEPTION_INVALID_DISPOSITION:
					printf("        |-EXCEPTION_INVALID_DISPOSITION\n");
					break;
				case EXCEPTION_NONCONTINUABLE_EXCEPTION:
					printf("        |-EXCEPTION_NONCONTINUABLE_EXCEPTION\n");
					break;
				case EXCEPTION_PRIV_INSTRUCTION:
					printf("        |-EXCEPTION_PRIV_INSTRUCTION\n");
					break;
				case EXCEPTION_SINGLE_STEP:
					printf("        |-EXCEPTION_SINGLE_STEP\n");
					break;
				case EXCEPTION_STACK_OVERFLOW:
					printf("        |-EXCEPTION_STACK_OVERFLOW\n");
					break;
				default:
					printf("        |-UNKNOWN EXCEPTION\n");
					break;
				}
			}
			*/
			break;
		case CREATE_THREAD_DEBUG_EVENT:
			addThread(DebugEvent.dwProcessId, DebugEvent.dwThreadId, DebugEvent.u.CreateThread.hThread);
			break;
		case CREATE_PROCESS_DEBUG_EVENT:
		{
			WCHAR filename[256];
			GetFinalPathNameByHandleW(DebugEvent.u.CreateProcessInfo.hFile, filename, 512, NULL);
			addProcess(filename, DebugEvent.dwProcessId, DebugEvent.u.CreateProcessInfo.hProcess, DebugEvent.dwThreadId, DebugEvent.u.CreateProcessInfo.hThread, DebugEvent.u.CreateProcessInfo.lpBaseOfImage);
			started = true;
			break;
		}
		case EXIT_THREAD_DEBUG_EVENT:
			removeThread(DebugEvent.dwProcessId, DebugEvent.dwThreadId);
			break;
		case EXIT_PROCESS_DEBUG_EVENT:
			removeProcess(DebugEvent.dwProcessId);
			break;
		case LOAD_DLL_DEBUG_EVENT:
		{
			WCHAR filename[256];
			GetFinalPathNameByHandleW(DebugEvent.u.LoadDll.hFile, filename, 512, NULL);
			addMapped(DebugEvent.dwProcessId, filename, DebugEvent.u.LoadDll.lpBaseOfDll);
			break;
		}
		case UNLOAD_DLL_DEBUG_EVENT:
		{
			removeMapped(DebugEvent.dwProcessId, DebugEvent.u.UnloadDll.lpBaseOfDll);
			break;
		}
		case RIP_EVENT:
			printf("[INFO] RIP_EVENT\n");
			break;
		default:
			printf("[INFO] UNKNOWN EVENT code: %08X\n", DebugEvent.dwDebugEventCode);
		}

		if (!ContinueDebugEvent(DebugEvent.dwProcessId, DebugEvent.dwThreadId, status))
		{
			printf("[INFO] Continue failed - me SAD :'(\n");
			break;
		}
	}
	return true;
}


//prog.exe [BB file] [TIME] [output] [executable] [params.....]
int wmain(int argc, wchar_t* argv[])
{
	std::wstring bbFile;
	std::wstring outputFile;
	std::wstring executable;
	std::wstring baseDir;
	std::wstring commandline;
	int time;

	if (argc < 6)
	{
		printf("SYNTAX: %S basicBlockFile timeToWait outputFile baseDirectory executable [commandline params]", argv[0]);
		exit(1);
	}


	bbFile = argv[1];
	time = _wtoi(argv[2]);
	outputFile = argv[3];
	baseDir = argv[4];
	executable = argv[5];

	commandline = L"\"" + executable + L"\" ";
	for (int x = 6; x < argc; x++)
	{
		commandline += L"\"";
		commandline += argv[x];
		commandline += L"\" ";
	}

	std::wstring escape = L"\\";
	if (!std::equal(baseDir.rbegin(), baseDir.rend(), escape.rbegin()))
		baseDir += escape;
		
	Tracer* trace = new Tracer();
	trace->setBaseDir((WCHAR*)baseDir.c_str());
	trace->loadBBfile((WCHAR*)bbFile.c_str());
	trace->setTime(time);
	trace->setSaveFile((WCHAR*)outputFile.c_str());
	trace->start((WCHAR*)executable.c_str(), (WCHAR*)commandline.c_str());
	printf("DONE");
    return 0;
}

