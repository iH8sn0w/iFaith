using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.IO;
public class sn0wbreeze
{
	//      <---- nand0/nor0 images ---->
	public string logoName = "apple.img3";
	public string chg0Name = "bat.chrg0.img3";
	public string chg1Name = "bat.chrg1.img3";
	public string batfName = "bat.full.img3";
	public string low0Name = "bat.low0.img3";
	public string low1Name = "bat.low1.img3";
	public string dtreName = "DeviceTree.img3";
	public string GchgName = "Glyph.Charging.img3";
	public string GpinName = "Glyph.Plugin.img3";
	public string ibotName = "iBoot.img3";
	public string illbName = "LLB.img3";
		//Only Applies to iOS 3.x and back.
	public string nsrvName = "NeedService.img3";
	public string recmName = "RecoveryLogo.img3";
	//      <---- nand0/nor0 images ---->
	private void Welcome_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(35, 0);
		Status.Invoke((MethodInvoker)Center_Stuffz);
		ThreadPool.QueueUserWorkItem(GoGoGadgetSN0W);
	}
	public void LoadiREB()
	{
		if (xml_model == "Apple TV 2") {
			atv2mode = true;
			iDevice = "Apple TV 2";
		} else {
			iDevice = "";
			atv2mode = false;
		}
		iREB_mode = true;
		dfu.MdiParent = MDIMain;
		MDIMain.dfuinstructions.Visible = true;
		MDIMain.dfuinstructionstxt.Visible = true;
		MDIMain.dfuinstructionstxt.Text = "DFU Mode Pwner";
		MDIMain.Button1.Visible = true;
		MDIMain.blue.Visible = true;
		dfu.Show();
		this.Dispose();
	}
	public void Manifest_Parser()
	{
		//Load manifest to RTB
		manifest.LoadFile(temppath + "\\IPSW\\Firmware\\all_flash\\all_flash." + xml_board + ".production\\manifest", RichTextBoxStreamType.PlainText);
		if (xml_ios.Substring(0, 1) == "3") {
			//For iOS 3.x
			//We're going to need to patch needservice.img3! >:P
			illbName = manifest.Lines(0).ToString;
			ibotName = manifest.Lines(1).ToString;
			dtreName = manifest.Lines(2).ToString;
			logoName = manifest.Lines(3).ToString;
			recmName = manifest.Lines(4).ToString;
			nsrvName = manifest.Lines(5).ToString;
			low0Name = manifest.Lines(6).ToString;
			low1Name = manifest.Lines(7).ToString;
			GchgName = manifest.Lines(8).ToString;
			GpinName = manifest.Lines(9).ToString;
			chg0Name = manifest.Lines(10).ToString;
			chg1Name = manifest.Lines(11).ToString;
			batfName = manifest.Lines(12).ToString;
		} else {
			//The others.
			//Fuck you needservice! We don't like you anymoar!
			illbName = manifest.Lines(0).ToString;
			ibotName = manifest.Lines(1).ToString;
			dtreName = manifest.Lines(2).ToString;
			logoName = manifest.Lines(3).ToString;
			recmName = manifest.Lines(4).ToString;
			low0Name = manifest.Lines(5).ToString;
			low1Name = manifest.Lines(6).ToString;
			GchgName = manifest.Lines(7).ToString;
			GpinName = manifest.Lines(8).ToString;
			chg0Name = manifest.Lines(9).ToString;
			chg1Name = manifest.Lines(10).ToString;
			batfName = manifest.Lines(11).ToString;
		}
		manifest.Dispose();
		//Done.
	}
	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		dynamic Answer = null;
		Answer = Interaction.MsgBox("Are you sure you want to cancel?", MsgBoxStyle.YesNo, "iFaith");
		if (Answer == Constants.vbYes) {
			Interaction.Shell("cmd /c taskkill /f /t /im xpwntool.exe", AppWinStyle.Hide);
			Interaction.Shell("cmd /c taskkill /f /t /im vfdecrypt.exe", AppWinStyle.Hide);
			Interaction.Shell("cmd /c taskkill /f /t /im hfsplus.exe", AppWinStyle.Hide);
			// Display a child form.
			Form frm = new Form();
			frm.MdiParent = MDIMain;
			frm.Width = this.Width / 2;
			frm.Height = this.Height / 2;
			frm.Show();
			frm.Hide();

			Welcome.MdiParent = MDIMain;
			Welcome.Show();
			Welcome.Button1.Enabled = false;
			About.MdiParent = MDIMain;
			About.Show();
			About.BringToFront();
			this.Close();
			this.Dispose();
		} else {
			return;
		}
	}
	private void sn0w_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		ThreadPool.QueueUserWorkItem(GoGoGadgetSN0W);
	}
	public void Update_Progress_Bar()
	{
		ProgressBar1.Value = ProgressBar1.Value + 10;
	}
	public void Update_Status()
	{
		Status.Text = StatusLabel;
		Status.Left = (Width / 2) - (Status.Width / 2);
	}
	public void Center_Stuffz()
	{
		Button2.Left = (Width / 2) - (Button2.Width / 2);
	}
	public void GoGoGadgetSN0W()
	{
		StatusLabel = "Preparing...";
		Status.Invoke((MethodInvoker)Update_Status);
		Delay(2);
		//Display App...
		IPSW_vars();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Extracting Resources...";
		Status.Invoke((MethodInvoker)Update_Status);
		ExtractResources();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Extracting IPSW...";
		Status.Invoke((MethodInvoker)Update_Status);
		UnzipIPSW();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Parsing manifest...";
		Status.Invoke((MethodInvoker)Update_Status);
		//
		this.Invoke((MethodInvoker)Manifest_Parser);
		//
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Processing Root Filesystem...";
		Status.Invoke((MethodInvoker)Update_Status);
		Processrootfs();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Processing Ramdisk...";
		Status.Invoke((MethodInvoker)Update_Status);
		PatchRamdisk();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Signing IPSW...";
		Status.Invoke((MethodInvoker)Update_Status);
		iFaith();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Processing iBSS...";
		Status.Invoke((MethodInvoker)Update_Status);
		PatchiBSS();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Zipping *signed* IPSW...";
		Status.Invoke((MethodInvoker)Update_Status);
		ZipIPSW();
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		StatusLabel = "Cleaning Up...";
		Status.Invoke((MethodInvoker)Update_Status);
		Status.Invoke((MethodInvoker)Update_Progress_Bar);
		Cleanup();
		GoGoGadgetiREB();
	}
	public void GoGoGadgetiREB()
	{
		//
		Status.Invoke((MethodInvoker)Hide_Cancel);
		//
		//Hide iFaith shit...
		StatusLabel = "Press the " + Quotation + "Proceed" + Quotation + " button to get your iDevice ready for restore!";
		Status.Invoke((MethodInvoker)Update_Status);
	}
	public void Hide_Cancel()
	{
		title.Text = "Done building IPSW...";
		ProgressBar1.Visible = false;
		cancel.Visible = false;
		pwnrequired.Visible = true;
		spinny.Visible = false;
		ProgressBar1.Value = 0;
		Button2.Visible = true;
		Button2.Left = (Width / 2) - (Button2.Width / 2);
	}
	public void Processrootfs()
	{
		if (xml_model == "Apple TV 2" & xml_ios == "4.1 (8M89)") {
			return;
		} else if (xml_model == "iPod Touch 3G" & xml_ios == "3.1.1 (7C145)") {
			return;
		} else {
			Rename_File(temppath + "\\IPSW\\" + rootfs, rootfs + ".orig");
			vfdecrypt(temppath + "\\IPSW\\" + rootfs + ".orig", temppath + "\\IPSW\\" + rootfs, rootfs_key);
			Delete_File(temppath + "\\IPSW\\" + rootfs + ".orig");
		}
	}
	public void ExtractResources()
	{
		SaveToDisk("Resources.zip", temppath + "\\Resources.zip");
		SaveToDisk("iFaith." + xml_ipsw_md5 + ".zip", temppath + "\\iFaith.bundle.zip");

		//Primary Resources...
		using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(temppath + "\\Resources.zip")) {
			zip1.ExtractAll(temppath + "\\", true);
			zip1.Dispose();
		}
		//iFaith bundle...
		using (Ionic.Zip.ZipFile zip1 = Ionic.Zip.ZipFile.Read(temppath + "\\iFaith.bundle.zip")) {
			zip1.ExtractAll(temppath + "\\", true);
			zip1.Dispose();
		}
	}
	public void PatchRamdisk()
	{
		string RamdiskPath = temppath + "\\IPSW\\ramdisk.dmg";
		//Renaming...
		Rename_File(temppath + "\\IPSW\\" + Ramdisk, Ramdisk + ".orig");
		//Ramdisk Decryption...
		if (string.IsNullOrEmpty(RamdiskIV)) {
			xpwntool_nokeys(temppath + "\\IPSW\\" + Ramdisk + ".orig", RamdiskPath);
		} else {
			xpwntool(temppath + "\\IPSW\\" + Ramdisk + ".orig", RamdiskPath, RamdiskIV, RamdiskKey);
		}
		//Were going to detect the ramdisk size, then + 2621440 (2.5MB) bytes.
		System.IO.FileInfo ramdiskfile = null;
		string rd_size = null;
		int originalsize = 0;
		ramdiskfile = My.Computer.FileSystem.GetFileInfo(RamdiskPath);
		//Storing Ramdisk size to integer...
		originalsize = ramdiskfile.Length;
		//Adding 2621440 (2.5MB) to file size...
		rd_size = originalsize + 2621440;
		//Actually doing the work...
		hfsplus_grow(RamdiskPath, rd_size);
		//Extracting asr...
		hfsplus_extract(RamdiskPath, "/usr/sbin/asr", temppath + "\\asr.orig");
		//Patching asr...
		bspatch(temppath + "\\asr.orig", temppath + "\\asr.pwned", temppath + "\\asr.patch");
		//Safely removing asr...
		hfsplus_mv(RamdiskPath, "/usr/sbin/asr", "/usr/sbin/asr_orig");
		//Adding Pwned asr...
		hfsplus_add(RamdiskPath, temppath + "\\asr.pwned", "/usr/sbin/asr");
		//Permissions for asr...
		hfsplus_chmod(RamdiskPath, "/usr/sbin/asr", "100755");
		//options.plist shit...
		ReplateText(temppath + "\\options.plist", temppath + "\\options.pwned", "$ROOT_SIZE$", rootfs_size);
		//Adding to ramdisk...
		hfsplus_add(RamdiskPath, temppath + "\\options.pwned", "/usr/local/share/restore/options.plist");
		if (xml_model == "Apple TV 2") {
		//ignore teh sexiness... :(
		} else {
			//Add sexiness... :)
			hfsplus_rm(RamdiskPath, "/usr/share/progressui/images-1x/applelogo.png");
			hfsplus_rm(RamdiskPath, "/usr/share/progressui/images-2x/applelogo.png");
			//
			hfsplus_add(RamdiskPath, temppath + "\\applelogox1.png", "/usr/share/progressui/images-1x/applelogo.png");
			hfsplus_add(RamdiskPath, temppath + "\\applelogox2.png", "/usr/share/progressui/images-2x/applelogo.png");
		}
		//Rebuild Ramdisk...
		if (string.IsNullOrEmpty(RamdiskIV)) {
			xpwntool_template_no_keys(RamdiskPath, temppath + "\\IPSW\\" + Ramdisk, temppath + "\\IPSW\\" + Ramdisk + ".orig");
		} else {
			xpwntool_template(RamdiskPath, temppath + "\\IPSW\\" + Ramdisk, temppath + "\\IPSW\\" + Ramdisk + ".orig", RamdiskIV, RamdiskKey);
		}
		//Clean up...
		Delete_File(temppath + "\\asr.orig");
		Delete_File(temppath + "\\asr.pwned");
		Delete_File(temppath + "\\asr.patch");
		Delete_File(RamdiskPath);
		Delete_File(temppath + "\\IPSW\\" + Ramdisk + ".orig");
		Delete_File(temppath + "\\applelogox1.png");
		Delete_File(temppath + "\\applelogox2.png");
		Delete_File(temppath + "\\options.plist");
		Delete_File(temppath + "\\options.pwned");
		//Done.
	}
	public void iFaith()
	{
		string flashDIR = temppath + "\\IPSW\\Firmware\\all_flash\\all_flash." + xml_board + ".production\\";
		//
		string logoPath = flashDIR + logoName;
		string chg0Path = flashDIR + chg0Name;
		string chg1Path = flashDIR + chg1Name;
		string batfPath = flashDIR + batfName;
		string low0Path = flashDIR + low0Name;
		string low1Path = flashDIR + low1Name;
		string dtrePath = flashDIR + dtreName;
		string gchgPath = flashDIR + GchgName;
		string gpinPath = flashDIR + GpinName;
		string ibotPath = flashDIR + ibotName;
		string illbPath = flashDIR + illbName;
		string recmPath = flashDIR + recmName;
		//We can't forget our friend needservice ;)
		string nsrvPath = flashDIR + nsrvName;
		string krnlPath = null;
		if (xml_ios.Substring(0, 1) == "3") {
			//Kernels from 3.x have their processor included in the kernel name. (except iPad)
			if (iDevice == "iPhone 3GS") {
				krnlPath = temppath + "\\IPSW\\kernelcache.release.s5l8920x";
				Rename_File(krnlPath, "kernelcache.release.s5l8920x.orig");
			} else if (iDevice == "iPod Touch 3G") {
				krnlPath = temppath + "\\IPSW\\kernelcache.release.s5l8922x";
				Rename_File(krnlPath, "kernelcache.release.s5l8922x.orig");
			} else if (iDevice == "iPad 1G") {
				krnlPath = temppath + "\\IPSW\\kernelcache.release.k48";
				Rename_File(krnlPath, "kernelcache.release.k48.orig");
			}
		} else {
			krnlPath = temppath + "\\IPSW\\kernelcache.release." + xml_board.Substring(0, 3);
			Rename_File(krnlPath, "kernelcache.release." + xml_board.Substring(0, 3) + ".orig");
		}
		//Rename All Files to original...
		Rename_File(logoPath, logoName + ".orig");
		Rename_File(chg0Path, chg0Name + ".orig");
		Rename_File(chg1Path, chg1Name + ".orig");
		Rename_File(batfPath, batfName + ".orig");
		Rename_File(low0Path, low0Name + ".orig");
		Rename_File(low1Path, low1Path + ".orig");
		Rename_File(dtrePath, dtreName + ".orig");
		Rename_File(gchgPath, GchgName + ".orig");
		Rename_File(gpinPath, GpinName + ".orig");
		Rename_File(ibotPath, ibotName + ".orig");
		Rename_File(illbPath, illbName + ".orig");
		if (xml_ios.Substring(0, 1) == "3") {
			Rename_File(nsrvPath, nsrvName + ".orig");
		}
		Rename_File(recmPath, recmName + ".orig");
		//Patch (sign) Files...
		string dir = temppath + "\\";
		bspatch(logoPath + ".orig", logoPath, dir + "logo.patch");
		bspatch(chg0Path + ".orig", chg0Path, dir + "chg0.patch");
		bspatch(chg1Path + ".orig", chg1Path, dir + "chg1.patch");
		bspatch(batfPath + ".orig", batfPath, dir + "batf.patch");
		bspatch(low0Path + ".orig", low0Path, dir + "low0.patch");
		bspatch(low1Path + ".orig", low1Path, dir + "low1.patch");
		bspatch(dtrePath + ".orig", dtrePath, dir + "dtre.patch");
		bspatch(gchgPath + ".orig", gchgPath, dir + "gchg.patch");
		bspatch(gpinPath + ".orig", gpinPath, dir + "gpin.patch");
		bspatch(ibotPath + ".orig", ibotPath, dir + "ibot.patch");
		bspatch(illbPath + ".orig", illbPath, dir + "illb.patch");
		if (xml_ios.Substring(0, 1) == "3") {
			bspatch(nsrvPath + ".orig", nsrvPath, dir + "nsrv.patch");
		}
		bspatch(recmPath + ".orig", recmPath, dir + "recm.patch");
		bspatch(krnlPath + ".orig", krnlPath, dir + "krnl.patch");
		//Delete Old files + patches...
		Delete_File(dir + "logo.patch");
		Delete_File(dir + "chg0.patch");
		Delete_File(dir + "chg1.patch");
		Delete_File(dir + "batf.patch");
		Delete_File(dir + "low0.patch");
		Delete_File(dir + "low1.patch");
		Delete_File(dir + "dtre.patch");
		Delete_File(dir + "gchg.patch");
		Delete_File(dir + "gpin.patch");
		Delete_File(dir + "ibot.patch");
		Delete_File(dir + "illb.patch");
		if (xml_ios.Substring(0, 1) == "3") {
			Delete_File(dir + "nsrv.patch");
		}
		Delete_File(dir + "recm.patch");
		Delete_File(dir + "krnl.patch");
		//
		Delete_File(logoPath + ".orig");
		Delete_File(chg0Path + ".orig");
		Delete_File(chg1Path + ".orig");
		Delete_File(batfPath + ".orig");
		Delete_File(low0Path + ".orig");
		Delete_File(low1Path + ".orig");
		Delete_File(dtrePath + ".orig");
		Delete_File(gchgPath + ".orig");
		Delete_File(gpinPath + ".orig");
		Delete_File(ibotPath + ".orig");
		Delete_File(illbPath + ".orig");
		if (xml_ios.Substring(0, 1) == "3") {
			Delete_File(nsrvPath + ".orig");
		}
		Delete_File(recmPath + ".orig");
		Delete_File(krnlPath + ".orig");
		//
		string SHSH_offset = null;
		byte[] blob = null;
		BinaryWriter bw = null;
		Delay(1.5);
		// <----------------------------------------------------------------->
		// <----------------------------- logo ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(logoPath);

		bw = new BinaryWriter(File.Open(logoPath, FileMode.Open, FileAccess.ReadWrite));

		blob = String_To_Bytes(blob_logo);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- chg0 ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(chg0Path);
		bw = new BinaryWriter(File.Open(chg0Path, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_chg0);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- chg1 ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(chg1Path);
		bw = new BinaryWriter(File.Open(chg1Path, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_chg1);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- batF ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(batfPath);
		bw = new BinaryWriter(File.Open(batfPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_batf);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- low0 ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(low0Path);
		bw = new BinaryWriter(File.Open(low0Path, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_bat0);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- low1 ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(low1Path);
		bw = new BinaryWriter(File.Open(low1Path, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_bat1);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- dtre ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(dtrePath);
		bw = new BinaryWriter(File.Open(dtrePath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_dtre);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- gchg ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(gchgPath);
		bw = new BinaryWriter(File.Open(gchgPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_glyc);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- gpin ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(gpinPath);
		bw = new BinaryWriter(File.Open(gpinPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_glyp);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- ibot ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(ibotPath);
		bw = new BinaryWriter(File.Open(ibotPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_ibot);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- illb ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(illbPath);
		bw = new BinaryWriter(File.Open(illbPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_illb);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		if (xml_ios.Substring(0, 1) == "3") {
			// <----------------------------------------------------------------->
			// <----------------------------- nsrv ------------------------------>
			// <----------------------------------------------------------------->
			SHSH_offset = Find_SHSH_offset(nsrvPath);
			bw = new BinaryWriter(File.Open(nsrvPath, FileMode.Open, FileAccess.ReadWrite));


			blob = String_To_Bytes(blob_nsrv);

			bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
			bw.Write(blob);
			bw.Close();
		}
		// <----------------------------------------------------------------->
		// <----------------------------- recm ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(recmPath);
		bw = new BinaryWriter(File.Open(recmPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_recm);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		// <----------------------------------------------------------------->
		// <----------------------------- krnl ------------------------------>
		// <----------------------------------------------------------------->
		SHSH_offset = Find_SHSH_offset(krnlPath);
		bw = new BinaryWriter(File.Open(krnlPath, FileMode.Open, FileAccess.ReadWrite));


		blob = String_To_Bytes(blob_krnl);

		bw.BaseStream.Seek(SHSH_offset, SeekOrigin.Begin);
		bw.Write(blob);
		bw.Close();

		//I hate buildmanifest... -_-
		Delete_File(temppath + "\\IPSW\\BuildManifest.plist");
	}
	public void PatchiBSS()
	{
		string path = temppath + "\\IPSW\\Firmware\\dfu";
		//
		Rename_File(path + "\\iBSS." + xml_board + ".RELEASE.dfu", "iBSS." + xml_board + ".RELEASE.dfu.orig");
		//
		xpwntool(path + "\\iBSS." + xml_board + ".RELEASE.dfu.orig", path + "\\iBSS.d", iBSSIV, iBSSKey);
		bspatch(path + "\\iBSS.d", path + "\\iBSS.pwned", temppath + "\\iBSS." + xml_board + ".RELEASE.patch");
		xpwntool_template(path + "\\iBSS.pwned", path + "\\iBSS." + xml_board + ".RELEASE.dfu", path + "\\iBSS." + xml_board + ".RELEASE.dfu.orig", iBSSIV, iBSSKey);
		//
		Delete_File(path + "\\iBSS." + xml_board + ".RELEASE.dfu.orig");
		Delete_File(path + "\\iBSS.d");
		Delete_File(path + "\\iBSS.pwned");
		Delete_File(temppath + "\\iBSS." + xml_board + ".RELEASE.patch");
	}
	public void ZipIPSW()
	{
		//get desktop folder
		object obj = null;
		obj = Interaction.CreateObject("WScript.Shell");
		iDevice = iDevice.Replace(" ", "_");

		string FileName = "iFaith_" + iDevice + "-" + xml_ios + "_signed.ipsw";

		//delete ipsw if already exists
		if (File_Exists(obj.SpecialFolders("desktop") + "\\" + FileName) == true) {
			File_Delete(obj.SpecialFolders("desktop") + "\\" + FileName);
		}
		using (Ionic.Zip.ZipFile zip1 = new Ionic.Zip.ZipFile()) {
			zip1.CompressionLevel = Ionic.Zlib.CompressionLevel.LEVEL0_NONE;
			zip1.AddDirectory(temppath + "\\IPSW\\");
			zip1.Save(obj.SpecialFolders("desktop") + "\\" + FileName);
		}
	}
	public void Cleanup()
	{
		//Delete All .patch
		string[] directoryFiles = System.IO.Directory.GetFiles(temppath, "*.patch");
		foreach (string directoryFile in directoryFiles) {
			System.IO.File.Delete(directoryFile);
		}
		//Delete rest of static resources...
		Delete_File(temppath + "\\xpwntool.exe");
		Delete_File(temppath + "\\hfsplus.exe");
		Delete_File(temppath + "\\bspatch.exe");
		//Delete Dll's...
		Delete_File(temppath + "\\bzip2.dll");
		Delete_File(temppath + "\\cygcrypto-0.9.8.dll");
		Delete_File(temppath + "\\cygwin.dll");
		Delete_File(temppath + "\\libeay32.dll");
		Delete_File(temppath + "\\libpng12.dll");
		Delete_File(temppath + "\\libssl32.dll");
		Delete_File(temppath + "\\zlib1.dll");
	}

	private void Button2_Click(System.Object sender, System.EventArgs e)
	{
		LoadiREB();
	}
	public sn0wbreeze()
	{
		Load += Welcome_Load;
	}
}
