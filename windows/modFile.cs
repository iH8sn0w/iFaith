using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;
static class modFile
{
	public static bool Loaded = false;
	public static bool SecondBuild = false;
	[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
	public static extern long GetWindowLong(System.IntPtr hWnd, Int32 nIndex);
	[DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
	public static extern long SetWindowLong(System.IntPtr hWnd, Int32 nIndex, long dwNewLong);
	private const  GWL_EXSTYLE = (-20);
	private const  WS_EX_CLIENTEDGE = 0x200;
	public static void SaveToDisk(string resourceName, string fileName)
	{
		// Get a reference to the running application.
		Assembly assy = Assembly.GetExecutingAssembly();
		// Loop through each resource, looking for the image name (case-insensitive).
		foreach (string resource in assy.GetManifestResourceNames()) {
			if (resource.ToLower().IndexOf(resourceName.ToLower()) != -1) {
				// Get the embedded file from the assembly as a MemoryStream.
				using (System.IO.Stream resourceStream = assy.GetManifestResourceStream(resource)) {
					if (resourceStream != null) {
						using (BinaryReader reader = new BinaryReader(resourceStream)) {
							// Read the bytes from the input stream.
							byte[] buffer = reader.ReadBytes(Convert.ToInt32(resourceStream.Length));
							using (FileStream outputStream = new FileStream(fileName, FileMode.Create)) {
								using (BinaryWriter writer = new BinaryWriter(outputStream)) {
									// Write the bytes to the output stream.
									writer.Write(buffer);
								}
							}
						}
					}
				}
				break; // TODO: might not be correct. Was : Exit For
			}
		}
	}
	public static void SetMdiClientBorder(bool showBorder)
	{
		foreach (Control c in MDIMain.Controls) {
			if (c is MdiClient) {
				long windowLong = GetWindowLong(c.Handle, GWL_EXSTYLE);
				if (showBorder) {
					windowLong = windowLong | WS_EX_CLIENTEDGE;
				} else {
					windowLong = windowLong & (!WS_EX_CLIENTEDGE);
				}
				SetWindowLong(c.Handle, GWL_EXSTYLE, windowLong);
				c.Width = c.Width + 1;
				break; // TODO: might not be correct. Was : Exit For
			}
		}
	}
	public static string BuildFilter(string strExtension)
	{
		string functionReturnValue = null;
		functionReturnValue = "";
		if (strExtension.PadLeft(1) != ".") {
			functionReturnValue = "(*." + strExtension + ")|" + "*." + strExtension;
		} else if (strExtension.PadLeft(1) == ".") {
			functionReturnValue = "(*" + strExtension + ")|" + "*" + strExtension;
		}
		return functionReturnValue;
	}
	public static string FileOpenDialog(string strExtension, string strInitDir)
	{
		string functionReturnValue = null;
		System.Windows.Forms.OpenFileDialog oFileDialog = new System.Windows.Forms.OpenFileDialog();
		string strfilter = BuildFilter(strExtension);
		functionReturnValue = "";
		var _with1 = oFileDialog;
		_with1.Filter = "iDevice Software File (*.ipsw) |*.ipsw;";
		_with1.DefaultExt = strExtension;
		_with1.InitialDirectory = strInitDir;
		_with1.ShowDialog();
		if (System.Windows.Forms.DialogResult.OK) {
			functionReturnValue = _with1.FileName;
		} else if (System.Windows.Forms.DialogResult.Cancel) {
		}
		return functionReturnValue;
	}
	public static bool File_Copy(string strSource, string strDestination, bool bolOverwrite, ref string strError = "")
	{
		bool functionReturnValue = false;
		if (strDestination.Length & strSource.Length != 0) {
			try {
				File.Copy(strSource, strDestination, bolOverwrite);
				functionReturnValue = true;
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool File_Rename(string strSource, string strNewName, ref string strError = "")
	{
		bool functionReturnValue = false;
		if (strNewName.Length & strSource.Length != 0) {
			try {
				File.Move(strSource, strNewName);
				functionReturnValue = true;
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool File_Move(string strSource, string strDestination, ref string strError = "")
	{
		bool functionReturnValue = false;
		if (strDestination.Length & strSource.Length != 0) {
			try {
				File.Move(strSource, strDestination);
				functionReturnValue = true;
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool File_Exists(string strFile)
	{
		bool functionReturnValue = false;

		if (strFile.Length != 0) {
			FileInfo oFile = new FileInfo(strFile);
			if (oFile.Exists == true) {
				functionReturnValue = true;
			} else {
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool File_Delete(string strFilename, ref string strError = "")
	{
		bool functionReturnValue = false;
		if (strFilename.Length != 0) {
			try {
				System.IO.File.Delete(strFilename);
				functionReturnValue = true;
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool Directory_Exists(string StrFolder)
	{
		bool functionReturnValue = false;
		if (StrFolder.Length != 0) {
			DirectoryInfo oDirectory = new DirectoryInfo(StrFolder);
			if (oDirectory.Exists == true) {
				functionReturnValue = true;
			} else {
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool Create_Directory(string strDirectoryName, ref string strError = "")
	{
		bool functionReturnValue = false;
		bool bolExitst = false;
		if (strDirectoryName.Length != 0) {
			try {
				bolExitst = Directory_Exists(strDirectoryName);
				if (!bolExitst) {
					Directory.CreateDirectory(strDirectoryName);
					functionReturnValue = true;
				}
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static bool Folder_Delete(string strFolderame, ref string strError = "")
	{
		bool functionReturnValue = false;
		if (strFolderame.Length != 0) {
			try {
				System.IO.DirectoryInfo Directory = new System.IO.DirectoryInfo(strFolderame);
				Directory.Delete(true);
				functionReturnValue = true;
			} catch (Exception oExc) {
				strError = oExc.Message;
				functionReturnValue = false;
			}
		}
		return functionReturnValue;
	}
	public static long GetFileSize(string MyFilePath)
	{
		FileInfo MyFile = new FileInfo(MyFilePath);
		long FileSize = MyFile.Length;
		return FileSize;
	}
	public static string GetFileContents(string FullPath, ref string ErrInfo = "")
	{
		string strContents = null;
		StreamReader objReader = null;
		try {
			objReader = new StreamReader(FullPath);
			strContents = objReader.ReadToEnd();
			objReader.Close();
			return strContents;
		} catch (Exception Ex) {
			ErrInfo = Ex.Message;
		}
		return "";
	}
	public static bool SaveTextToFile(string strData, string FullPath, string ErrInfo = "")
	{

		string Contents = "";
		bool bAns = false;
		StreamWriter objReader = null;
		try {
			objReader = new StreamWriter(FullPath);
			objReader.Write(strData);
			objReader.Close();
			bAns = true;
		} catch (Exception Ex) {
			ErrInfo = Ex.Message;
		}
		return bAns;
	}
}
