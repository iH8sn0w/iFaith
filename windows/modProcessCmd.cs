using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
 // ERROR: Not supported in C#: OptionDeclaration
static class modProcessCmd
{
	public static string board = "nxxap";
	public static string deviceversion = "";
	//Constants
	public static string iDevice = "";
	public static string IPSWVersion = "";
	public static string cmdline = "";
	public const int NORMAL_PRIORITY_CLASS = 0x20;
	public const int HIGH_PRIORITY_CLASS = 0x80;
	public const short INFINITE = -1;
	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	public static extern void Sleep(long dwMilliseconds);
	private const short STARTF_USESHOWWINDOW = 1;

	private const short SW_HIDE = 0;
	//public Types
	public struct STARTUPINFO
	{
		public int cb;
		public string lpReserved;
		public string lpDesktop;
		public string lpTitle;
		public int dwX;
		public int dwY;
		public int dwXSize;
		public int dwYSize;
		public int dwXCountChars;
		public int dwYCountChars;
		public int dwFillAttribute;
		public int dwFlags;
		public short wShowWindow;
		public short cbReserved2;
		public int lpReserved2;
		public int hStdInput;
		public int hStdOutput;
		public int hStdError;
	}
	public static void Delay(double dblSecs)
	{
		const double OneSec = 1.0 / (1440.0 * 60.0);
		System.DateTime dblWaitTil = default(System.DateTime);
		DateAndTime.Now.AddSeconds(OneSec);
		dblWaitTil = DateAndTime.Now.AddSeconds(OneSec).AddSeconds(dblSecs);
		while (!(DateAndTime.Now > dblWaitTil)) {
			Application.DoEvents();
			// Allow windows messages to be processed
		}
	}
	public struct PROCESS_INFORMATION
	{
		public int hProcess;
		public int hThread;
		public int dwProcessID;
		public int dwThreadID;
	}
	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	//API Declarations
	public static extern int WaitForSingleObject(int hHandle, int dwMilliseconds);
	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	//UPGRADE_WARNING: Structure PROCESS_INFORMATION may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	//UPGRADE_WARNING: Structure STARTUPINFO may require marshalling attributes to be passed as an argument in this Declare statement. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	public static extern int CreateProcessA(int lpApplicationName, string lpCommandLine, int lpProcessAttributes, int lpThreadAttributes, int bInheritHandles, int dwCreationFlags, int lpEnvironment, int lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, ref PROCESS_INFORMATION lpProcessInformation);
	[DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	public static extern int CloseHandle(int hObject);

	//example use
	//cmdline = "%comspec% /c """ & App.Path & "\7z.exe x -oIPSW ,original.ipsw"""
	//or
	//cmdline = "7z.exe x -oIPSW ,original.ipsw"
	//or
	//cmdline = "DEL fstab.original /f /q"

	//ExecCmd (cmdline, True) 'False = show windows

	public static int ExecCmd(string cmdline, bool HideWindow = false)
	{
		PROCESS_INFORMATION Proc = default(PROCESS_INFORMATION);
		STARTUPINFO start = null;
		short ReturnValue = 0;

		//Hide window?
		if ((HideWindow)) {
			start.dwFlags = STARTF_USESHOWWINDOW;
			start.wShowWindow = SW_HIDE;
		}


		//Initialize The STARTUPINFO Structure
		start.cb = Strings.Len(start);

		//Start The Shelled Application
		ReturnValue = CreateProcessA(0, cmdline, 0, 0, 1, HIGH_PRIORITY_CLASS, 0, 0, ref start, ref Proc);

		//Wait for The Shelled Application to Finish
		do {
			ReturnValue = WaitForSingleObject(Proc.hProcess, 0);
			System.Windows.Forms.Application.DoEvents();
			Sleep(10);
		} while (!(ReturnValue != 258));

		//Close Handle to Shelled Application
		ReturnValue = CloseHandle(Proc.hProcess);
	}
}
