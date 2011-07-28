using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
static class iFaith
{
	//
	public static string temppath = "";
	public static bool Debug_Mode = false;
	public static bool OhNoesShutDOWN = false;
	public static bool ResetDFUInstructions = false;
	public static bool DFUConnected = false;
	public static bool CancelDownload = false;
	public static string Quotation = "\"";
	public static string rootfs = "";
	public static string StatusLabel = "";
	//
		//1
	public static string AppleLogoBLOB = "";
		//2
	public static string BatCharg0BLOB = "";
		//3
	public static string BatCharg1BLOB = "";
		//4
	public static string BatFullBLOB = "";
		//5 *ONLY if FW has it!*
	public static string NeedServiceBLOB = "";
		//6
	public static string BatLow0BLOB = "";
		//7
	public static string BatLow1BLOB = "";
		//8
	public static string DeviceTreeBLOB = "";
		//9
	public static string GlyphChrgBLOB = "";
		//10
	public static string GlyphPluginBLOB = "";
		//11
	public static string iBootBLOB = "";
		//12
	public static string LLBBlob = "";
		//13
	public static string RecLogoBLOB = "";
		//14
	public static string KernelBLOB = "";
	//
	public static bool iNeedServiceBlob = false;
	public static bool atv2mode = false;
	//
	public static string iRecovery = "\"" + "s-irecovery.exe" + "\"" + " ";
	public static string ECID = "x";
	public static string iosversion = "0";
	public static string theIPSWhash = "null";
	public static string realiosversion = "0";
	public static string ipsw = "";
	//
	public static string xml_revision = "";
	public static string xml_ios = "";
	public static string xml_model = "";
	public static string xml_board = "";
	public static string xml_ecid = "";
	public static string blob_logo = "";
	public static string blob_chg0 = "";
	public static string blob_chg1 = "";
	public static string blob_batf = "";
	public static string blob_bat0 = "";
	public static string blob_bat1 = "";
	public static string blob_dtre = "";
	public static string blob_glyc = "";
	public static string blob_glyp = "";
	public static string blob_nsrv = "";
	public static string blob_ibot = "";
	public static string blob_illb = "";
	public static string blob_recm = "";
	public static string blob_krnl = "";
	public static string xml_md5 = "";
	public static string xml_ipsw_md5 = "";
	public static string IPSWurl = "http://localhost";
	public static byte[] String_To_Bytes(string strInput)
	{
		// i variable used to hold position in string  
		int i = 0;
		// x variable used to hold byte array element position  
		int x = 0;
		// allocate byte array based on half of string length  
		byte[] bytes = new byte[(strInput.Length) / 2];
		// loop through the string - 2 bytes at a time converting it to decimal equivalent
		// and store in byte array  
		while (strInput.Length > i + 1) {
			long lngDecimal = Convert.ToInt32(strInput.Substring(i, 2), 16);
			bytes[x] = Convert.ToByte(lngDecimal);
			i = i + 2;
			x += 1;
		}
		// return the finished byte array of decimal values  
		return bytes;
	}
	private static T InlineAssignHelper<T>(ref T target, T value)
	{
		target = value;
		return value;
	}
	public static string MD5CalcString(string strData)
	{
		System.Security.Cryptography.MD5CryptoServiceProvider objMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] arrData = null;
		byte[] arrHash = null;
		// first convert the string to bytes (using UTF8 encoding for unicode characters)
		arrData = System.Text.Encoding.UTF8.GetBytes(strData);
		// hash contents of this byte array
		arrHash = objMD5.ComputeHash(arrData);
		// thanks objects
		objMD5 = null;
		// return formatted hash
		return ByteArrayToString(arrHash);
	}
	public static string ByteArrayToString(byte[] arrInput)
	{
		System.Text.StringBuilder strOutput = new System.Text.StringBuilder(arrInput.Length);
		for (int i = 0; i <= arrInput.Length - 1; i++) {
			strOutput.Append(arrInput[i].ToString("X2"));
		}
		return strOutput.ToString().ToLower();
	}
	public static string getSHA1Hash(string strToHash)
	{
		System.Security.Cryptography.SHA1CryptoServiceProvider sha1Obj = new System.Security.Cryptography.SHA1CryptoServiceProvider();
		byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);
		bytesToHash = sha1Obj.ComputeHash(bytesToHash);
		string strResult = "";
		foreach (byte b in bytesToHash) {
			strResult += b.ToString("x2");
		}
		return strResult.ToUpper();
	}
	public static object Find_SHSH_offset(string infile)
	{
		//Loadup img3...
		BinaryReader Reader = new BinaryReader(File.OpenRead(infile));
		//Advance to 0xC (SHSH offset origin)
		Reader.BaseStream.Seek(0xc, SeekOrigin.Begin);
		byte[] SHSH_bytes = Reader.ReadBytes(4);
		string SHSH_bytes_string = ByteArrayToString(SHSH_bytes);
		string shsh_orign = SHSH_bytes_string.Substring(6, 2) + SHSH_bytes_string.Substring(4, 2) + SHSH_bytes_string.Substring(2, 2) + SHSH_bytes_string.Substring(0, 2);
		Reader.Close();
		return SubHex(shsh_orign, "2C");
	}
	public static object SubHex(string Address, string Value)
	{
		int add_value = int.Parse(Value, System.Globalization.NumberStyles.HexNumber);
		int add_address = int.Parse(Address, System.Globalization.NumberStyles.HexNumber);
		int i = add_address - add_value;
		long finale = Convert.ToInt64(i);
		return finale;
	}
	public static object SubUpHex(string Address, string Value)
	{
		int add_value = int.Parse(Value.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
		int add_address = int.Parse(Address.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
		int i = add_address - add_value;
		string finale = "0x" + i.ToString("X");
		return finale;
	}
	public static object AddUpHex(string Address, string Value)
	{
		string Final_Value = Value.Replace("0x", "");
		string Final_Address = Address.Replace("0x", "");
		int add_value = int.Parse(Final_Value, System.Globalization.NumberStyles.HexNumber);
		int add_address = int.Parse(Final_Address, System.Globalization.NumberStyles.HexNumber);
		int i = add_value + add_address;
		string finale = "0x" + i.ToString("X");
		return finale;
	}
	public static void AddHex(string Address, string Value)
	{
		string Final_Value = Value.Replace("0x", "");
		string Final_Address = Address.Replace("0x", "");
		int add_value = int.Parse(Final_Value, System.Globalization.NumberStyles.HexNumber);
		int add_address = int.Parse(Final_Address, System.Globalization.NumberStyles.HexNumber);
		int i = add_value + add_address;
		string finale = "0x" + i.ToString("X");
		CurrentAddr = finale;
	}
	public static void setenv(string Variable, string value)
	{
		cmdline = iRecovery + "-c " + "\"" + "setenv " + Variable + " " + value + "\"";
		ExecCmd(cmdline, true);
		cmdline = iRecovery + "-c " + "\"" + "saveenv" + "\"";
		ExecCmd(cmdline, true);
	}
	public static void getenv(string Variable)
	{
		if (Run.DumpMode == true) {
			Process p = new Process();
			p.StartInfo.FileName = "s-irecovery.exe";
			p.StartInfo.Arguments = "-g " + Variable;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardError = true;
			p.OutputDataReceived += Run.update_1_shsh;
			//AddHandler p.ErrorDataReceived, AddressOf Run.update_1_shsh
			p.Start();
			System.IO.StreamWriter SW = p.StandardInput;
			p.BeginOutputReadLine();
			p.Dispose();
		} else {
			if (string.IsNullOrEmpty(Variable)) {
				Interaction.MsgBox("No Variable was specified!", MsgBoxStyle.Critical);
				return;
			}
			Run.getenvbox.Text = "";
			Process p = new Process();
			p.StartInfo.FileName = "s-irecovery.exe";
			p.StartInfo.Arguments = "-g " + Variable;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardError = true;
			p.OutputDataReceived += Run.update_1;
			//AddHandler p.ErrorDataReceived, AddressOf Run.update_1
			p.Start();
			System.IO.StreamWriter SW = p.StandardInput;
			p.BeginOutputReadLine();
			p.Dispose();
		}
	}
	public static void iRecovery_FBwrite(string Text)
	{
		cmdline = iRecovery + "-c " + "\"" + "go fbecho " + Text + "\"";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_cmd(string CMD)
	{
		cmdline = iRecovery + "-c " + "\"" + CMD + "\"";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_file(string File)
	{
		cmdline = iRecovery + "-f " + "\"" + File + "\"";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_exploit()
	{
		cmdline = iRecovery + "-e";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_pie(string Address)
	{
		cmdline = iRecovery + "-c " + "\"" + "go pie " + Address + "\"";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_pie2(string Address)
	{
		cmdline = iRecovery + "-c " + "\"" + "go pie2 " + Address + "\"";
		ExecCmd(cmdline, true);
	}
	public static void iRecovery_SHA1_memory(string FromAddress, string ToAddress, string Destination)
	{
		cmdline = iRecovery + "-c " + "\"" + "go memory sha1 " + FromAddress + " " + ToAddress + " " + Destination + "\"";
		ExecCmd(cmdline, true);
	}
	public static void xpwntool_nokeys(string infile, string outfile)
	{
		cmdline = "xpwntool.exe " + "\"" + infile + "\"" + " " + "\"" + outfile + "\"";
		ExecCmd(cmdline, true);
	}
	public static void xpwntool(string infile, string outfile, string IVKey, string Key)
	{
		cmdline = "xpwntool.exe " + "\"" + infile + "\"" + " " + "\"" + outfile + "\"" + " -iv " + IVKey + " -k " + Key;
		ExecCmd(cmdline, true);
	}
	public static void xpwntool_template_no_keys(string infile, string outfile, string Template)
	{
		cmdline = "xpwntool.exe " + "\"" + infile + "\"" + " " + "\"" + outfile + "\"" + " -t " + "\"" + Template + "\"";
		ExecCmd(cmdline, true);
	}
	public static void xpwntool_template(string infile, string outfile, string Template, string IVKey, string Key)
	{
		cmdline = "xpwntool.exe " + "\"" + infile + "\"" + " " + "\"" + outfile + "\"" + " -t " + "\"" + Template + "\"" + " -iv " + IVKey + " -k " + Key;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_add(string Volume, string File2Add, string Path)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " add " + Quotation + File2Add + Quotation + " " + Path;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_addall(string Volume, string Folder2Add)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " addall " + Quotation + Folder2Add + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_chmod(string Volume, string File2Chmod, string chmod)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " chmod " + chmod + " " + File2Chmod;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_chown(string Volume, string File2Chown, string Owner, string Group)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " chown " + Owner + " " + Group + " " + File2Chown;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_mv(string Volume, string File2Move, string NewPath)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " mv " + File2Move + " " + NewPath;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_mkdir(string Volume, string DIRnameNpath)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " mkdir " + DIRnameNpath;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_rm(string Volume, string File2RemoveNpath)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " rm " + File2RemoveNpath;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_extract(string Volume, string File2ExtractNPath, string LocalLocation)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " extract " + File2ExtractNPath + " " + Quotation + LocalLocation + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_symlink(string Volume, string PlaceofSymlink, string Location2symlink)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " symlink " + PlaceofSymlink + " " + Location2symlink;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_grow(string Volume, string SizeInBytes)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " grow " + SizeInBytes;
		ExecCmd(cmdline, true);
	}
	public static void hfsplus_untar(string Volume, string Tar)
	{
		cmdline = Quotation + "hfsplus.exe" + Quotation + " " + Quotation + Volume + Quotation + " untar " + Quotation + Tar + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void Rename_File(string OriginalFilePath, string NewFileNAME)
	{
		cmdline = "cmd /c rename " + Quotation + OriginalFilePath + Quotation + " " + Quotation + NewFileNAME + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void Delete_File(string FilePath)
	{
		cmdline = "cmd /c DEL " + Quotation + FilePath + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void Merge_File(string FirstFile, string SecondFile, string FinalFile)
	{
		cmdline = "cmd /c type " + Quotation + FirstFile + Quotation + " " + Quotation + SecondFile + Quotation + " >> " + Quotation + FinalFile + Quotation;
		ExecCmd(cmdline, true);
	}
	public static void vfdecrypt(string EncryptedVolume, string DecryptedVolumePath, string vfdecryptkey)
	{
		cmdline = Quotation + "vfdecrypt.exe" + Quotation + " -i " + Quotation + EncryptedVolume + Quotation + " -k " + vfdecryptkey + " -o " + Quotation + DecryptedVolumePath + Quotation;
		ExecCmd(cmdline, true);
	}
	private static ArrayList GenerateFileList(string Dir)
	{
		ArrayList fils = new ArrayList();
		bool Empty = true;
		foreach (string file in Directory.GetFiles(Dir)) {
			// add each file in directory
			fils.Add(file);
			Empty = false;
		}
		if (Empty) {
			if (Directory.GetDirectories(Dir).Length == 0) {
				// if directory is completely empty, add it
				fils.Add(Dir);
			}
		}
		foreach (string dirs in Directory.GetDirectories(Dir)) {
			// recursive
			foreach (object obj in GenerateFileList(dirs)) {
				fils.Add(obj);
			}
		}
		// return file list
		return fils;
	}
	public static void ReplateText(string inputPlist, string outputPlist, string whatToReplace, string replaceWithThis)
	{
		dynamic fso = null;
		dynamic inputFile = null;
		dynamic outputFile = null;
		string str = null;
		fso = Interaction.CreateObject("Scripting.FileSystemObject");
		//1 means for reading
		inputFile = fso.OpenTextFile(inputPlist, 1);
		str = inputFile.ReadAll;
		//modify this string, replace required characters
		str = Strings.Replace(str, whatToReplace, replaceWithThis);
		//write back
		outputFile = fso.CreateTextFile(outputPlist, true);
		outputFile.Write(str);
	}
	public static void bspatch(string infile, string outfile, string Patchfile)
	{
		cmdline = "bspatch.exe " + "\"" + infile + "\"" + " " + "\"" + outfile + "\"" + " " + "\"" + Patchfile + "\"";
		ExecCmd(cmdline, true);
	}
	public static void Downloader()
	{
		SaveToDisk("msvcr100d.dll", temppath + "\\msvcr100d.dll");
		SaveToDisk("dl.exe", temppath + "\\dl.exe");
		if (iDevice == "iPhone 3GS") {
			cmdline = "dl.exe -iphone3gs";
			ExecCmd(cmdline, true);
		} else if (board == "n92ap") {
			cmdline = "dl.exe -verizoni4";
			ExecCmd(cmdline, true);
		} else if (board == "n90ap") {
			cmdline = "dl.exe -iphone4";
			ExecCmd(cmdline, true);
		} else if (iDevice == "iPod Touch 2G") {
			cmdline = "dl.exe -ipt2g";
			ExecCmd(cmdline, true);
		} else if (iDevice == "iPod Touch 3G") {
			cmdline = "dl.exe -ipt3";
			ExecCmd(cmdline, true);
		} else if (iDevice == "iPod Touch 4") {
			cmdline = "dl.exe -ipt4";
			ExecCmd(cmdline, true);
		} else if (iDevice == "Apple TV 2") {
			cmdline = "dl.exe -atv2";
			ExecCmd(cmdline, true);
		} else if (iDevice == "iPad 1") {
			cmdline = "dl.exe -ipad";
			ExecCmd(cmdline, true);
		}
		if (File_Exists("iBSS." + board + ".RELEASE.dfu") == true & File_Exists("iBoot." + board + ".RELEASE.img3") == true) {
			return;
		} else {
			Interaction.MsgBox("Error -2 (Download Failure)", MsgBoxStyle.Critical);
			Application.Exit();
		}
		File_Delete("dl.exe");
		File_Delete("msvcr100d.dll");
	}

	public static void UnzipIPSW()
	{
		//Removing Previous...
		Folder_Delete(temppath + "\\IPSW");
		//Unzip IPSW...
		using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(ipsw)) {
			Application.DoEvents();
			zip1.ExtractAll(temppath + "\\IPSW", true);
			zip1.Dispose();
		}
	}
	public static bool FileInUse(string sFile)
	{
		if (System.IO.File.Exists(sFile)) {
			try {
				short F = FileSystem.FreeFile();
				FileSystem.FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite);
				FileSystem.FileClose(F);
			} catch {
				return true;
			}
		}
	}
	public class WebFileDownloader
	{
		public event AmountDownloadedChangedEventHandler AmountDownloadedChanged;
		public delegate void AmountDownloadedChangedEventHandler(long iNewProgress);
		public event FileDownloadSizeObtainedEventHandler FileDownloadSizeObtained;
		public delegate void FileDownloadSizeObtainedEventHandler(long iFileSize);
		public event FileDownloadCompleteEventHandler FileDownloadComplete;
		public delegate void FileDownloadCompleteEventHandler();
		public event FileDownloadFailedEventHandler FileDownloadFailed;
		public delegate void FileDownloadFailedEventHandler(Exception ex);

		private string mCurrentFile = string.Empty;
		public string CurrentFile {
			get { return mCurrentFile; }
		}
		public bool DownloadFile(string URL, string Location)
		{
			try {
				mCurrentFile = GetFileName(URL);
				WebClient WC = new WebClient();
				WC.DownloadFile(URL, Location);
				if (FileDownloadComplete != null) {
					FileDownloadComplete();
				}
				return true;
			} catch (Exception ex) {
				if (FileDownloadFailed != null) {
					FileDownloadFailed(ex);
				}
				return false;
			}
		}
		private string GetFileName(string URL)
		{
			try {
				return URL.Substring(URL.LastIndexOf("/") + 1);
			} catch (Exception ex) {
				return URL;
			}
		}
		public bool DownloadFileWithProgress(string URL, string Location)
		{
			bool functionReturnValue = false;
			FileStream FS = null;
			try {
				mCurrentFile = GetFileName(URL);
				WebRequest wRemote = null;
				byte[] bBuffer = null;
				bBuffer = new byte[257];
				int iBytesRead = 0;
				int iTotalBytesRead = 0;

				FS = new FileStream(Location, FileMode.Create, FileAccess.Write);
				wRemote = WebRequest.Create(URL);
				WebResponse myWebResponse = wRemote.GetResponse();
				if (FileDownloadSizeObtained != null) {
					FileDownloadSizeObtained(myWebResponse.ContentLength);
				}
				Stream sChunks = myWebResponse.GetResponseStream();
				do {
					if (iFaith.CancelDownload == true) {
						iFaith.CancelDownload = false;
						sChunks.Close();
						FS.Close();
						return functionReturnValue;
					}
					iBytesRead = sChunks.Read(bBuffer, 0, 256);
					FS.Write(bBuffer, 0, iBytesRead);
					iTotalBytesRead += iBytesRead;
					if (myWebResponse.ContentLength < iTotalBytesRead) {
						if (AmountDownloadedChanged != null) {
							AmountDownloadedChanged(myWebResponse.ContentLength);
						}
					} else {
						if (AmountDownloadedChanged != null) {
							AmountDownloadedChanged(iTotalBytesRead);
						}
					}
				} while (!(iBytesRead == 0));
				Welcome_ipsw.progresstxt.Text = "Writing file to Hard Drive...";
				sChunks.Close();
				FS.Close();
				if (FileDownloadComplete != null) {
					FileDownloadComplete();
				}
				return true;
			} catch (Exception ex) {
				if ((FS != null)) {
					FS.Close();
					FS = null;
				}
				if (FileDownloadFailed != null) {
					FileDownloadFailed(ex);
				}
				return false;
			}
			return functionReturnValue;
		}
		public static string FormatFileSize(long Size)
		{
			try {
				int KB = 1024;
				int MB = KB * KB;
				// Return size of file in kilobytes.
				if (Size < KB) {
					return (Size.ToString("D") + " bytes");
				} else {
					switch (Size / KB) {
						case  // ERROR: Case labels with binary operators are unsupported : LessThan
1000:
							return (Size / KB).ToString("N") + "KB";
						case  // ERROR: Case labels with binary operators are unsupported : LessThan
1000000:
							return (Size / MB).ToString("N") + "MB";
						case  // ERROR: Case labels with binary operators are unsupported : LessThan
10000000:
							return (Size / MB / KB).ToString("N") + "GB";
					}
				}
			} catch (Exception ex) {
				return Size.ToString();
			}
		}
	}
}
