using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
public class MDIMain
{
	[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	private static extern long OpenIcon(long hWnd);
	[DllImport("user32", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	private static extern long FindWindow(string lpClassName, string lpWindowName);
	[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	private static extern long SetForegroundWindow(long hWnd);
	public string VersionNumber = "1.2";
	private void MDIMain_ByeBye(System.Object Sender, System.EventArgs e)
	{
		//Safely shutdown iBoot/DFU searcher...
		iAcqua.iLeft = true;
		OhNoesShutDOWN = true;
	}
	private void MDIMain_Load(System.Object sender, System.EventArgs e)
	{
		SetMdiClientBorder(false);
		foreach (Control ctl in this.Controls) {
			if (ctl is MdiClient) {
				ctl.BackColor = this.BackColor;
			}
		}
		// Display a child form.
		Form frm = new Form();
		frm.MdiParent = this;
		frm.Width = this.Width / 2;
		frm.Height = this.Height / 2;
		frm.Show();
		frm.Hide();

		this.Text = "iFaith v" + VersionNumber + " -- By: iH8sn0w";

		Welcome.MdiParent = this;
		Welcome.Show();
		Welcome.Button1.Enabled = false;
		About.MdiParent = this;
		About.Show();
		About.BringToFront();
		temppath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\iFaith";
		Folder_Delete(temppath);
		Create_Directory(temppath);
		// SendKeys
		Microsoft.Win32.RegistryKey NewKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion", true);
		string GetVal = Convert.ToString(NewKey.GetValue("ProductName"));
		if (GetVal.Contains("Windows XP")) {
			NewKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{FF66E9F6-83E7-3A3E-AF14-8DE9A809A6A4}", true);
			try {
				GetVal = Convert.ToString(NewKey.GetValue("DisplayName"));
			} catch (Exception ex) {
				//uh-oh...
			}
			if (GetVal.Contains("C++")) {
			//MsgBox("INSTALLED!", MsgBoxStyle.Information)
			} else {
				InstallVCPP.RunWorkerAsync();
				//MsgBox("NOT INSTALLED!", MsgBoxStyle.Information)
			}
		}
	}

	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		dfu.CleaniREB();
	}

	private void BackgroundWorker1_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		Delay(0.1);
	}

	private void BackgroundWorker1_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
	{
		this.TopMost = true;
		this.Activate();
		this.Focus();
		Delay(1);
		FlashWindowClass.FlashWindow(this, false, false);
		this.TopMost = false;
	}
	private void InstallVCPP_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		//Visual C++ 2008 Redistributable Installer
		//Copyright (C) 2007 Microsoft
		SaveToDisk("vcpp.zip", temppath + "\\vcpp.zip");
		using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(temppath + "\\vcpp.zip")) {
			zip1.ExtractAll(temppath + "\\");
			zip1.Dispose();
		}
		Delay(1);
		//write to buffer
		cmdline = Quotation + temppath + "\\install.exe" + Quotation + " /q";
		ExecCmd(cmdline, true);
	}
	public MDIMain()
	{
		Load += MDIMain_Load;
		FormClosing += MDIMain_ByeBye;
	}
}
public class FlashWindowClass
{
	[DllImport("User32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
	private static extern int FlashWindowEx(ref FLASHWINFO FWInfo);

	private const Int32 FLASHW_STOP = 0;
	private const Int32 FLASHW_CAPTION = 0x1L;
	private const Int32 FLASHW_TRAY = 0x2L;
	private const Int32 FLASHW_ALL = (FLASHW_CAPTION | FLASHW_TRAY);
	private const Int32 FLASHW_TIMER = 0x4L;

	private const Int32 FLASHW_TIMERNOFG = 0xcL;
	private struct FLASHWINFO
	{
		public int cbSize;
		public IntPtr hwnd;
		public int dwFlags;
		public int uCount;
		public int dwTimeout;
	}

	public static Int32 FlashWindow(Form frm)
	{
		return FlashWindow(frm, true, true, 5);
	}

	public static Int32 FlashWindow(Form frm, bool flashTitleBar, bool flashTray)
	{
		return FlashWindow(frm, flashTitleBar, flashTray, 5);
	}

	public static Int32 FlashWindow(Form frm, int flashAmount)
	{
		return FlashWindow(frm, true, true, flashAmount);
	}

	public static Int32 FlashWindow(Form frm, bool flashTitleBar, bool flashTray, int flashAmount)
	{
		try {
			FLASHWINFO fwi = new FLASHWINFO();
			var _with1 = fwi;
			_with1.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(fwi);
			_with1.hwnd = frm == null ? IntPtr.Zero : frm.Handle;
			if (flashTitleBar)
				_with1.dwFlags = _with1.dwFlags | FLASHW_CAPTION;
			if (flashTray)
				_with1.dwFlags = _with1.dwFlags | FLASHW_TRAY;
			_with1.uCount = flashAmount;
			_with1.dwTimeout = 250;
			return FlashWindowEx(ref fwi);
		} catch {
			return -1;
		}
	}

}
