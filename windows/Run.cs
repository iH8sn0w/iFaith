using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Management;
public class Run
{
	public int Stage = 0;
	public bool DumpMode = false;
		// 3GS and up are 0x41000000, but ipt2g and below are 0x90000000.
	public string LDRAddress = "0x41000000";
	//
		// Path to the kernel on the rootfs.
	public string KernelPath = "/System/Library/Caches/com.apple.kernelcaches/kernelcache";
	//If iOS is running 3.1.x -- kernelcache has the processor chip linked to the filename. (s5l8920x/s5l8922x)

	public string Loadillb_nand1 = "go bdev read nand_llb 0x600 0x2C000 " + LDRAddress;
		// 44 49 43 45 [DICE]
	public string Loadillb_nand2 = "go memory search " + LDRAddress + " 0x20000 44494345";
	//
		//logo [AppleLogo]
	public string Loadlogo = "go image load 0x6C6F676F " + LDRAddress;
		//bat0 [BatteryLow0]
	public string Loadbat0 = "go image load 0x62617430 " + LDRAddress;
		//bat1 [BatteryLow1]
	public string Loadbat1 = "go image load 0x62617431 " + LDRAddress;
		//batF [BatteryFull]
	public string LoadbatF = "go image load 0x62617446 " + LDRAddress;
		//chg0 [Charging0]
	public string Loadchg0 = "go image load 0x63686730 " + LDRAddress;
		//chg1 [Charging1]
	public string Loadchg1 = "go image load 0x63686731 " + LDRAddress;
		//dtre [DeviceTree]
	public string Loaddtre = "go image load 0x64747265 " + LDRAddress;
		//glyC [Charging...]
	public string LoadglyC = "go image load 0x676C7943 " + LDRAddress;
		//glyP [Plugin...]
	public string LoadglyP = "go image load 0x676C7950 " + LDRAddress;
		//ibot [iBoot]
	public string Loadibot = "go image load 0x69626F74 " + LDRAddress;
		//illb [LLB] -- Only applies to iPhone 3GS/iPad in our case.
	public string Loadillb = "go image load 0x696C6C62 " + LDRAddress;
		//recm [RecoveryModeLogo]
	public string Loadrecm = "go image load 0x7265636D " + LDRAddress;
	//
	//Discontinued Images (< 3.1.3):
		//nsrv [NeedsService] -- Only applies to 3.1.3 and back.
	public string Loadnsrv = "go image load 0x6E737276 " + LDRAddress;
	//
		//Mount RootFS (also hope that the NAND driver isn't updated :P)
	public string Loadkrnl_1 = "go fs mount nand0a /boot";
		//Load kernel to address.
	public string Loadkrnl_2 = "go fs load /boot" + KernelPath + " " + LDRAddress;
	//
		//44 49 43 45 [DICE] 48 53 48 53 [HSHS]
	public string Search_ECID = "go memory search " + LDRAddress + " 0x9000000 48534853";
		//00 00 47 41 42 4B [GABK]
	public string Search_GABK = "go memory search " + LDRAddress + " 0x9000000 00004741424B";
	//
	public string PieDump = "go pie ";
	public string PieDump2 = "go pie2 ";
	public bool GoSlower = false;
	//
	private string Results;
	private delegate void delUpdate();
	private delUpdate Finished = new delUpdate(UpdateText);
	private delegate void UpdateTextBoxDelegate(string Text);
	//
	private string Results_shsh;
	private delegate void delUpdate_shsh();
	private delUpdate Finished_shsh = new delUpdate(UpdateText_shsh);
	private delegate void UpdateTextBoxDelegate_shsh(string Text);
	public void UpdateAddresses()
	{
		Loadlogo = "go image load 0x6C6F676F " + LDRAddress;
		Loadbat0 = "go image load 0x62617430 " + LDRAddress;
		//bat0 [BatteryLow0]
		Loadbat1 = "go image load 0x62617431 " + LDRAddress;
		//bat1 [BatteryLow1]
		LoadbatF = "go image load 0x62617446 " + LDRAddress;
		//batF [BatteryFull]
		Loadchg0 = "go image load 0x63686730 " + LDRAddress;
		//chg0 [Charging0]
		Loadchg1 = "go image load 0x63686731 " + LDRAddress;
		//chg1 [Charging1]
		Loaddtre = "go image load 0x64747265 " + LDRAddress;
		//dtre [DeviceTree]
		LoadglyC = "go image load 0x676C7943 " + LDRAddress;
		//glyC [Charging...]
		LoadglyP = "go image load 0x676C7950 " + LDRAddress;
		//glyP [Plugin...]
		Loadibot = "go image load 0x69626F74 " + LDRAddress;
		//ibot [iBoot]
		Loadillb = "go image load 0x696C6C62 " + LDRAddress;
		//illb [LLB] -- Only applies to iPhone 3GS/iPad in our case.
		Loadrecm = "go image load 0x7265636D " + LDRAddress;
		//recm [RecoveryModeLogo]
		Loadnsrv = "go image load 0x6E737276 " + LDRAddress;
		Loadkrnl_2 = "go fs load /boot" + KernelPath + " " + LDRAddress;
		if (iDevice == "iPod Touch 2G") {
			Search_GABK = "go memory search " + LDRAddress + " 0x900000 00004741424B";
			Search_ECID = "go memory search " + LDRAddress + " 0x900000 48534853";
		} else {
			Search_GABK = "go memory search " + LDRAddress + " 0x900000 00004741424B";
			Search_ECID = "go memory search " + LDRAddress + " 0x900000 48534853";
		}
	}
	private void UpdateText()
	{
		getenvbox.Text = Results;
	}
	private void UpdateText_shsh()
	{
		shshblob.Text += Results_shsh;
	}
	private void Form1_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(160, 0);
		if (iDevice == "iPod Touch 2G") {
			LDRAddress = "0x09000000";
		} else {
			LDRAddress = "0x41000000";
		}

		UpdateAddresses();

		getenvbox.Visible = Debug_Mode;
		shshblob.Visible = Debug_Mode;

		MDIMain.dfuinstructionstxt.ForeColor = Color.DimGray;
		MDIMain.dfuinstructions.Enabled = false;
		//
		MDIMain.Text = "iFaith v" + MDIMain.VersionNumber + " -- By: iH8sn0w -- [" + iDevice + "]";
		MDIMain.LoadiFaith.Enabled = true;
		MDIMain.LoadiFaith.Checked = true;
		MDIMain.loadifaithtxt.ForeColor = Color.White;
		PictureBox1.Image = My.Resources.Dove;
		Prepare.RunWorkerAsync();
	}
	public void update_1_shsh(object sender, DataReceivedEventArgs e)
	{
		UpdateTextBox_shsh(e.Data);
	}
	private void UpdateTextBox_shsh(string Tex)
	{
		if (this.InvokeRequired) {
			UpdateTextBoxDelegate del_shsh = new UpdateTextBoxDelegate(UpdateTextBox_shsh);
			object[] args = { Tex };
			this.Invoke(del_shsh, args);
		} else {
			shshblob.Text += Tex;
			shshblob.Text = shshblob.Text.Replace(" ", "");
		}
	}
	public void update_1(object sender, DataReceivedEventArgs e)
	{
		UpdateTextBox(e.Data);
	}
	private void UpdateTextBox(string Tex)
	{
		if (this.InvokeRequired) {
			UpdateTextBoxDelegate del = new UpdateTextBoxDelegate(UpdateTextBox);
			object[] args = { Tex };
			this.Invoke(del, args);
		} else {
			//tb.Text &= Tex & Environment.NewLine
			getenvbox.Text += Tex;
			getenvbox.Text = getenvbox.Text.Replace(" ", "");
			CurrentAddr = "";
			CurrentAddr = getenvbox.Text;
			//MsgBox(CurrentAddr)
		}
	}
	public void Center_status()
	{
		Status.Left = (this.Width / 2) - (Status.Width / 2);
	}
	private void Prepare_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		Status.Invoke((MethodInvoker)PrepareDoWork);
	}
	public void PrepareDoWork()
	{
		PictureBox1.Image = My.Resources.Dove;
		Status.Text = "Downloading Essentials...";
		ProgressBar1.Style = ProgressBarStyle.Marquee;
		ProgressBar1.MarqueeAnimationSpeed = 50;
		Status.Invoke((MethodInvoker)Center_status);
		Status.Invoke((MethodInvoker)Downloader);
		Delay(2);
		//Go
		Status.Invoke((MethodInvoker)gogogo);
	}
	public void gogogo()
	{
		//
		Status.Text = "Detecting ECID...";
		Status.Invoke((MethodInvoker)Center_status);

		Console.Text = string.Empty;
		Process p = new Process();
		p.StartInfo.FileName = "s-irecovery.exe";
		p.StartInfo.Arguments = "-ecid";
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.CreateNoWindow = true;
		p.StartInfo.RedirectStandardOutput = true;
		p.StartInfo.RedirectStandardInput = true;
		p.StartInfo.RedirectStandardError = true;
		p.OutputDataReceived += dfu.update_1;
		p.Start();
		System.IO.StreamWriter SW = p.StandardInput;
		p.BeginOutputReadLine();
		p.Dispose();
		Delay(2);
		ECID = dfu.idetect.Text.Substring(5, 16);

		//Change pic to limera1n...
		if (iDevice == "iPod Touch 2G") {
			Status.Text = "Exploiting with steaks4uce...";
			PictureBox1.Image = My.Resources.steaks4uce;
		} else {
			Status.Text = "Exploiting with limera1n...";
			PictureBox1.Image = My.Resources.Ra1ndrop;
		}
		Status.Invoke((MethodInvoker)Center_status);
		//Pwn it...
		iRecovery_exploit();
		Status.Text = "Waiting for " + iDevice + "...";
		Status.Invoke((MethodInvoker)Center_status);
		//Change pic to dove...
		PictureBox1.Image = My.Resources.Dove;
		//USB Reset...
		Delay(3);
		//
		Status.Text = "Uploading iBSS...";
		Status.Invoke((MethodInvoker)Center_status);
		//Upload iBSS...
		Delay(1);
		iRecovery_file("iBSS." + board + ".RELEASE.dfu");

		File_Delete("iBSS." + board + ".RELEASE.dfu");

		Status.Text = "Scanning for iBSS...";
		Status.Invoke((MethodInvoker)Center_status);

		//Go Scan for iBSS...
		Stage = 1;
		Scan_iBoot.RunWorkerAsync();
	}
	private void Scan_iBoot_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		//Searching for Recovery...
		Console.Text = " ";
		iRecoveryConnected = false;
		while (!(iRecoveryConnected == true)) {
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (iBoot) USB Driver'");

			foreach (ManagementObject queryObj in searcher.Get()) {
				Console.Text += (queryObj("Description"));
			}
			if (Console.Text.Contains("iBoot")) {
				iRecoveryConnected = true;
				Status.Invoke((MethodInvoker)upload_payload_ibss);
			}
		}
	}
	private void Scan_iBoot2_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		int i = 0;
		Console2.Text = " ";
		Delay(3);
		while (!(i == 1)) {
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (iBoot) USB Driver'");

			foreach (ManagementObject queryObj in searcher.Get()) {
				Console2.Text += (queryObj("Description"));
			}
			if (Console2.Text.Contains("iBoot")) {
				i = 1;
				Status.Invoke((MethodInvoker)upload_payload_iboot);
			}
		}
	}
	public void upload_payload_iboot()
	{
		Delete_File("iboot.payload");
		SaveToDisk(board + ".iboot.payload", "iboot.payload");

		Delay(1);

		PictureBox1.Image = My.Resources.gp;
		Status.Text = "Uploading iBoot greenpois0n payload...";
		Status.Invoke((MethodInvoker)Center_status);

		iRecovery_file("iboot.payload");

		Status.Text = "Setting up payload...";
		Status.Invoke((MethodInvoker)Center_status);

		iRecovery_cmd("go");

		Delay(1);
		iRecovery_FBwrite("iFaith v" + MDIMain.VersionNumber + " Initalized");
		iRecovery_FBwrite("=========================");
		//Exit Sub
		PictureBox1.Image = My.Resources.Dove;

		File_Delete("iboot.payload");

		ProgressBar1.Style = ProgressBarStyle.Blocks;
		GrabAppleLogo();
	}
	public void upload_payload_ibss()
	{
		Delete_File("ibss.payload");

		SaveToDisk(board + ".ibss.payload", "ibss.payload");
		Delay(1);
		PictureBox1.Image = My.Resources.gp;
		Status.Text = "Uploading iBSS greenpois0n payload...";
		Status.Invoke((MethodInvoker)Center_status);

		iRecovery_file("ibss.payload");

		//Start it up...
		iRecovery_cmd("go");

		File_Delete("ibss.payload");

		Delay(1);
		// give it time to initalize. [especially armv6 devices (ugh)]

		PictureBox1.Image = My.Resources.Dove;
		Status.Text = "Setting up iBSS...";
		Status.Invoke((MethodInvoker)Center_status);

		setenv("auto-boot", "false");

		//Send iBoot...
		//MsgBox("UPLOAD IBOOT?", MsgBoxStyle.Information)
		Status.Text = "Uploading iBoot...";
		Status.Invoke((MethodInvoker)Center_status);
		iRecovery_file("iBoot." + board + ".RELEASE.img3");

		File_Delete("iBoot." + board + ".RELEASE.img3");
		//Decrypt it...
		iRecovery_cmd("go image decrypt " + LDRAddress);
		//Patch it...
		iRecovery_cmd("go memory move " + LDRAddress.Substring(0, 8) + "40 " + LDRAddress + " 0x48000");
		iRecovery_cmd("go patch " + LDRAddress + " 0x48000");
		//Jump to it...
		//MsgBox("GO!!!", MsgBoxStyle.Information)
		iRecovery_cmd("go jump " + LDRAddress);

		Status.Text = "Scanning for iBoot...";
		Status.Invoke((MethodInvoker)Center_status);
		//Scan for iBoot...
		Stage = 2;
		Scan_iBoot2.RunWorkerAsync();
	}
	public void DetectiOS()
	{
		Status.Text = "Detecting iOS... [Please Wait]";
		Center_status();

		ProgressBar1.Style = ProgressBarStyle.Marquee;
		ProgressBar1.MarqueeAnimationSpeed = 50;
		//Mount rootfs...
		iRecovery_cmd(Loadkrnl_1);
		Delay(2);
		iRecovery_cmd(Loadkrnl_2);

		if (iDevice == "iPhone 3GS") {
			iRecovery_cmd("go fs load /boot/System/Library/Caches/com.apple.kernelcaches/kernelcache.s5l8920x 0x41000000");
		} else if (iDevice == "iPod Touch 2G") {
			iRecovery_cmd("go fs load /boot/System/Library/Caches/com.apple.kernelcaches/kernelcache.s5l8720x 0x09000000");
		} else if (iDevice == "iPod Touch 3G") {
			iRecovery_cmd("go fs load /boot/System/Library/Caches/com.apple.kernelcaches/kernelcache.s5l8922x 0x41000000");
		}
		DumpMode = false;
		Delay(1);
		iRecovery_cmd(Search_GABK);
		Delay(1);
		getenv("auto-boot");
		Delay(1);
		iRecovery_SHA1_memory(AddUpHex(CurrentAddr, "0x14"), AddUpHex(CurrentAddr, "0x37"), AddUpHex(CurrentAddr, "0x37"));
		Delay(1);
		iRecovery_pie(AddUpHex(CurrentAddr, "0x14"));
		Delay(1);
		getenv("auto-boot");
		Delay(1);
		if (getenvbox.Text.Length == 110 & getSHA1Hash(getenvbox.Text.Substring(0, 70)) == getenvbox.Text.Substring(70, 40)) {
		//Were Good *CONFIRMED*
		} else {
			Status.Invoke((MethodInvoker)DetectiOS);
			return;
		}
		//Giving it time to write to the buffer...
		Delay(1);
		if (Debug_Mode == true) {
			//Debugger
			Clipboard.Clear();
			Clipboard.SetText(getenvbox.Text);
			Interaction.MsgBox("KBAG copied to clipboard...", MsgBoxStyle.Information);
			iosversion = "DEBUG";
			realiosversion = "DEBUG";
			iNeedServiceBlob = false;
			theIPSWhash = "null/debug";
		} else if (getenvbox.Text.Contains("000071EA4DF0DA219FA192C47B914732707EBD5D666969A35D0617666672A14B89D7A2") == true) {
			//iPhone 3GS
			iosversion = "3.1";
			realiosversion = "3.1 (7C144)";
			iNeedServiceBlob = true;
			theIPSWhash = "38638d6056b53f2d87a0f5fcb5584cdd";
		} else if (getenvbox.Text.Contains("00001341A1282AED72E6CA47DDB71074594749B91CB7F19570972F9467119C13531900") == true) {
			//iPod Touch 2G
			iosversion = "3.1.1";
			realiosversion = "3.1.1 (7C145)";
			iNeedServiceBlob = true;
			theIPSWhash = "e0c97bdbb9efbf411b22a81327ad48dc";
		} else if (getenvbox.Text.Contains("000067E30B8FF3804B7419DA0766FE767F69E1B04A2BCBB0EB44D67F237E0BEBEB4B42") == true) {
			//iPod Touch 3G
			iosversion = "3.1.1";
			realiosversion = "3.1.1 (7C145)";
			iNeedServiceBlob = true;
			theIPSWhash = "4ad01a2c6fc82bcac2300253b0368f6e";
		} else if (getenvbox.Text.Contains("0000324BE66CCC17CDB5C5291C4A71CBEC9BCE783E017C68B5BBEEC3F1DA8C103FAF83") == true) {
			//iPhone 3GS
			iosversion = "3.1.2";
			realiosversion = "3.1.2 (7D11)";
			iNeedServiceBlob = true;
			theIPSWhash = "089769d37b846917394ffe11da9d2e17";
		} else if (getenvbox.Text.Contains("00006159DB0E027FC2731B67DB28F764F4F5B449147F39336455C55BD8FEA6553D1900") == true) {
			//iPod Touch 2G
			iosversion = "3.1.2";
			realiosversion = "3.1.2 (7D11)";
			iNeedServiceBlob = true;
			theIPSWhash = "35c66be376201082a005f0a289f26a65";
		} else if (getenvbox.Text.Contains("0000CA7BC0CF27B4C3E0C10FB1A32C04A2264A9858DD9AD67FA9C08701B6AEA608C9EC") == true) {
			//iPod Touch 3G
			iosversion = "3.1.2";
			realiosversion = "3.1.2 (7D11)";
			iNeedServiceBlob = true;
			theIPSWhash = "13938eaca91e12e7cefb47717e7cadc8";
		} else if (getenvbox.Text.Contains("00004E5FC89B9BD7DD956ACC6C0CCB27B2FD5C6C426172046498D48DCC7D2E1296A2EB") == true) {
			//iPhone 3GS
			iosversion = "3.1.3";
			realiosversion = "3.1.3 (7E18)";
			iNeedServiceBlob = true;
			theIPSWhash = "4117e4b22565e69205a84e9eeef0583e";
		} else if (getenvbox.Text.Contains("0000A3EB0799DC40A5ED60670C5FE69FBCCADC2FB0B1A773ED5550A8395D84979A7B00") == true) {
			//iPod Touch 2G
			iosversion = "3.1.3";
			realiosversion = "3.1.3 (7E18)";
			iNeedServiceBlob = true;
			theIPSWhash = "33df8d6ae5d8a695bba267ae89fe37f1";
		} else if (getenvbox.Text.Contains("00000B36D5B00EFB0D0FC6B42D2A6CDD53B9D8D1787188B2F871FFBE02C29B68BDC164") == true) {
			//iPod Touch 3G
			iosversion = "3.1.3";
			realiosversion = "3.1.3 (7E18)";
			iNeedServiceBlob = true;
			theIPSWhash = "a73de2cfafef3463e9afa491f20c5213";
		} else if (getenvbox.Text.Contains("0000EF56F94205D025CF65CCE9A6660C491CFCB919DAF0CE68A58E8749E3F3A9204604") == true) {
			//iPad
			iosversion = "3.2";
			realiosversion = "3.2 (7B367)";
			iNeedServiceBlob = true;
			theIPSWhash = "2912cefa0304e5430594c576ad88d398";
		} else if (getenvbox.Text.Contains("0000B46D7E31DCFB8171D2FB380D43569C431A90FBC8C59910B0B32894AA540C61C4BC") == true) {
			//iPad
			iosversion = "3.2.1";
			realiosversion = "3.2.1 (7B405)";
			iNeedServiceBlob = true;
			theIPSWhash = "5ccf846d96a677f42ac183f5a137dc92";
		} else if (getenvbox.Text.Contains("00005D504B7691EFD9A9F850C532D318721764AC0AE6E360D8278BE593219FC1BD0AAE") == true) {
			//iPad
			iosversion = "3.2.2";
			realiosversion = "3.2.2 (7B500)";
			iNeedServiceBlob = true;
			theIPSWhash = "cf6d93fffdc60dcca487a80004d250fa";
		} else if (getenvbox.Text.Contains("0000734E244E700C46C4D3EA5902540DBAEE000E42FB50AB31FE4B4DFF965BCA7ECDDB") == true) {
			//iPhone 3GS
			iosversion = "4.0";
			realiosversion = "4.0 (8A293)";
			iNeedServiceBlob = false;
			theIPSWhash = "f9819ad9a52324ac6f10e4a0ea581cbd";
		} else if (getenvbox.Text.Contains("0000343F8D2CB553F6DF9855F980439E4E831AE76955DE510C9BF987F7E26D5FE22713") == true) {
			//iPhone 4
			iosversion = "4.0";
			realiosversion = "4.0 (8A293)";
			iNeedServiceBlob = false;
			theIPSWhash = "8717be79fb38cd83aa5e5956eb0608b7";
		} else if (getenvbox.Text.Contains("0000501FDDC5BD3E000BAB2F6502932F97FD87CD4A3DD95E7B1BC71DF86E510E8A7B00") == true) {
			//iPod Touch 2G
			iosversion = "4.0";
			realiosversion = "4.0 (8A293)";
			iNeedServiceBlob = false;
			theIPSWhash = "41dd8ab40159a13d7be42cd7e5f3a479";
		} else if (getenvbox.Text.Contains("0000641477FDFA42765C350069F4A902D9E1D38B3C07E8CE18AA469AB3F0CE9D6C912B") == true) {
			//iPod Touch 3G
			iosversion = "4.0";
			realiosversion = "4.0 (8A293)";
			iNeedServiceBlob = false;
			theIPSWhash = "6b9d65c9f63792968bad57e44a73434f";
		} else if (getenvbox.Text.Contains("00008AB48BEF8AF1D026FCE3C202E3285C6EC6F08B6B77A1A52F39E738199C7E572FAE") == true) {
			//iPhone 3GS
			iosversion = "4.0.1";
			realiosversion = "4.0.1 (8A306)";
			iNeedServiceBlob = false;
			theIPSWhash = "a3104ca3b72a91bc7eff037ee320ecc5";
		} else if (getenvbox.Text.Contains("000088922BD75937880ED4BF841FD0A6CA4020E3F6E7888CDC059E60B91057552BAC9C") == true) {
			//iPhone 4
			iosversion = "4.0.1";
			realiosversion = "4.0.1 (8A306)";
			iNeedServiceBlob = false;
			theIPSWhash = "40ebacb47fb32d7f33ba0fd596e150e9";
		} else if (getenvbox.Text.Contains("0000A6F641E7F6F3E7C906D3E054322AC486ED8EF8D22D513020D8808C8E8DB9507B65") == true) {
			//iPhone 3GS
			iosversion = "4.0.2";
			realiosversion = "4.0.2 (8A400)";
			iNeedServiceBlob = false;
			theIPSWhash = "9cb5684457fb41886827d727d91313c3";
		} else if (getenvbox.Text.Contains("0000F645794A94C9D4000F78D7415937E994EDA941D97FB4CD12F1B7A95725B50A5406") == true) {
			//iPhone 4
			iosversion = "4.0.2";
			realiosversion = "4.0.2 (8A400)";
			iNeedServiceBlob = false;
			theIPSWhash = "790b24fe7515084f457ce413618b2709";
		} else if (getenvbox.Text.Contains("0000CFA00C90B562C2481D3052303CF0B5EBF85852DCA9D9F50715714D738C00133F00") == true) {
			//iPod Touch 2G
			iosversion = "4.0.2";
			realiosversion = "4.0.2 (8A400)";
			iNeedServiceBlob = false;
			theIPSWhash = "e706efcf835de9fcf6f96c7a420a7a22";
		} else if (getenvbox.Text.Contains("00009012F7EE55ABC5DDE4B3F93908191C2F55249FAA64875502B2B5C880173FF6A40B") == true) {
			//iPod Touch 3G
			iosversion = "4.0.2";
			realiosversion = "4.0.2 (8A400)";
			iNeedServiceBlob = false;
			theIPSWhash = "dc7741b9e4353895c3910237a5b10a4d";
		} else if (getenvbox.Text.Contains("00004CFE1EB382DD7FC3505381B88BD624B2739289AF041E24F770B53C554B57D67A9B") == true) {
			//iPhone 3GS
			iosversion = "4.1";
			realiosversion = "4.1 (8B117)";
			iNeedServiceBlob = false;
			theIPSWhash = "e07bee3c03e7a18e5d75fcaa23db17b5";
		} else if (getenvbox.Text.Contains("00000ECD8D1131AECA71297FA794F495973FA857AE08343992B7200C94C5E4857D907A") == true) {
			//iPhone 4
			iosversion = "4.1";
			realiosversion = "4.1 (8B117)";
			iNeedServiceBlob = false;
			theIPSWhash = "ac3031a7b5c013d6a09952b691985878";
		} else if (getenvbox.Text.Contains("00001C5E07CFBBC2915C1A5E06E8FE197DF4FB033AEBFB299D088676776BEE4F1E9600") == true) {
			//iPod Touch 2G
			iosversion = "4.1";
			realiosversion = "4.1 (8B117)";
			iNeedServiceBlob = false;
			theIPSWhash = "9f8a1978f053ec96926e535bb57ac171";
		} else if (getenvbox.Text.Contains("000064E9A662CBFC940ABDFF0CB70ECA0E51FD57C9D546BC993B8195EB5199ED6B02F0") == true) {
			//iPod Touch 3G
			iosversion = "4.1";
			realiosversion = "4.1 (8B117)";
			iNeedServiceBlob = false;
			theIPSWhash = "f3877c6f309730ee31297a06c7a9e82c";
		} else if (getenvbox.Text.Contains("00008FA8E1A9FDC18FEE29DB840B3AE5214D92E17586C6CF14AB2E6B057301BA7CADEA") == true) {
			//iPod Touch 4 -- 8B117
			iosversion = "4.1";
			realiosversion = "4.1 (8B117)";
			iNeedServiceBlob = false;
			theIPSWhash = "2e634d16d0e01ef70070c9a289e488ca";
		} else if (getenvbox.Text.Contains("000000F0D8BE4BF316731E5D2286D88FA2C1B1307354696800385679294C437C454C60") == true) {
			//iPod Touch 4 -- 8B118
			iosversion = "4.1";
			realiosversion = "4.1 (8B118)";
			iNeedServiceBlob = false;
			theIPSWhash = "0564fcd3f53dd6262b9eb636b7fbe540";
		} else if (getenvbox.Text.Contains("0000304C52297B296D27351B6D1B4532D9E68A4CF94BEAB92C70745FABFBD3BFE60E47") == true) {
			//Apple TV 2
			iosversion = "4.1";
			//Technically, this would be 4.0.
			realiosversion = "4.1 (8M89)";
			iNeedServiceBlob = false;
			theIPSWhash = "35c8ab4b7e70ab0e47e2f5981e52ba55";
		} else if (getenvbox.Text.Contains("0000FCD547AC6414D15A6C7946FBC73E11951F54CB5B35EDF599BD64AEABC25CDA1C0D") == true) {
			//iPhone 3GS -- 4.2.1a
			iosversion = "4.2.1a";
			realiosversion = "4.2.1 (8C148a)";
			iNeedServiceBlob = false;
			theIPSWhash = "d688d2d48c8b054367adef8e7ab4f5ea";
		} else if (getenvbox.Text.Contains("000082D8156AEB2B048EB71A401F229B05D144D39069A124250BCF0C0551D526495ACB") == true) {
			//iPhone 4
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C148)";
			iNeedServiceBlob = false;
			theIPSWhash = "93957e7bd21f0549b60a60485c13206a";
		} else if (getenvbox.Text.Contains("000046EDB0CE803EB07B6007EEB410B30C8FA1DFC6322B18A23AE682DA0648B2A93E00") == true) {
			//iPod Touch 2G
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C148)";
			iNeedServiceBlob = false;
			theIPSWhash = "0045e3543647e23470b84c2c1de96ab1";
		} else if (getenvbox.Text.Contains("00006A5244825CE79733C37FAD042A6C15559C8496D0450ECD78F541EC09E9BC445C5A") == true) {
			//iPod Touch 3G
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C148)";
			iNeedServiceBlob = false;
			theIPSWhash = "25dbf5b3e5ca39edd0aab8fcab888503";
		} else if (getenvbox.Text.Contains("0000C9A2256217454D08E6B9B8567B28C0D6626039323E34579D3B4D1E081ECBC88179") == true) {
			//iPod Touch 4
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C148)";
			iNeedServiceBlob = false;
			theIPSWhash = "14d1508954532e91172f8704fd941a93";
		} else if (getenvbox.Text.Contains("0000597A5B66E6F88D5FDE8AA67F17C5520D9F5CFD25A7D86AD425F74840CA1B6E9D3F") == true) {
			//iPad
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C148)";
			iNeedServiceBlob = false;
			theIPSWhash = "9402d5f05348fd68c87f885ff4cb4717";
		} else if (getenvbox.Text.Contains("0000AE6310D1B0CC480B1CE6F1011356C79DC94FFF0507D688AD5573763BE34A501B62") == true) {
			//Apple TV 2
			iosversion = "4.2.1";
			realiosversion = "4.2.1 (8C154)";
			iNeedServiceBlob = false;
			theIPSWhash = "3fe1a01b8f5c8425a074ffd6deea7c86";
		} else if (getenvbox.Text.Contains("000083924CF182CD2A8811F0AEAF3AE47DBB817737F16967A79CBCB53D9352544ED8C0") == true) {
			//Verizon iPhone 4
			iosversion = "4.2.6";
			realiosversion = "4.2.6 (8E200)";
			iNeedServiceBlob = false;
			theIPSWhash = "eb3c205debb52c237c37f92335e6369c";
		} else if (getenvbox.Text.Contains("0000B69DAD6C9861A0760656E6197FFA0981590BF27980D6027D6D4F78131B1024852D") == true) {
			//Verizon iPhone 4
			iosversion = "4.2.7";
			realiosversion = "4.2.7 (8E303)";
			iNeedServiceBlob = false;
			theIPSWhash = "30fc03783453d23aaa0d13f89fd36c28";
		} else if (getenvbox.Text.Contains("0000926F12B8454D69C8F9340ABD7519A57418D74822297FD8CE72B371C8FFFBCA9680") == true) {
			//Verizon iPhone 4
			iosversion = "4.2.8";
			realiosversion = "4.2.8 (8E401)";
			iNeedServiceBlob = false;
			theIPSWhash = "0e0e4bf8f0d7c37b9a252fcbed60ac0c";
		} else if (getenvbox.Text.Contains("000031BCA86650ED2800BAF5251FED4DAF9E9C2AAA95C47314D3C6D9B13005CA0BF5C2") == true) {
			//iPhone 3GS
			iosversion = "4.3";
			realiosversion = "4.3 (8F190)";
			iNeedServiceBlob = false;
			theIPSWhash = "87ebb9b2c025fb5f87a4cab0631b1547";
		} else if (getenvbox.Text.Contains("00003714EFBA777D5BEFA537BDE0C010CE67CEB0A452D4945557F356B705742E387B58") == true) {
			//iPhone 4
			iosversion = "4.3";
			realiosversion = "4.3 (8F190)";
			iNeedServiceBlob = false;
			theIPSWhash = "e0a463bded8f5b1e076b466535b18c75";
		} else if (getenvbox.Text.Contains("00002284CDC328910C3A5221E784152CDCCE55F72C78E299A7F9FAF94DC090FC363EC0") == true) {
			//iPod Touch 3G
			iosversion = "4.3";
			realiosversion = "4.3 (8F190)";
			iNeedServiceBlob = false;
			theIPSWhash = "43383f2d5cd181f2af1e01ec62a3f1d6";
		} else if (getenvbox.Text.Contains("00004A280D8554B23A5E74069F1CB0657F248CCD2E75446BAB4A5BD49E54C99FF1D08F") == true) {
			//iPod Touch 4
			iosversion = "4.3";
			realiosversion = "4.3 (8F190)";
			iNeedServiceBlob = false;
			theIPSWhash = "0c8cdbbb729508811fa5bd29d8e1143b";
		} else if (getenvbox.Text.Contains("00003B4769D35A092E757F971A9CBC300F418BC6E815B0935E4567D82FB24A9BDCBBA0") == true) {
			//iPad
			iosversion = "4.3";
			realiosversion = "4.3 (8F190)";
			iNeedServiceBlob = false;
			theIPSWhash = "9a889ba48bc2715292f199f50c70ed60";
		} else if (getenvbox.Text.Contains("00007F8651BF1E81548A719A94BFF92C9A01980A3F3EDF0BED5D3F70D5BF266C92F37A") == true) {
			//Apple TV 2
			iosversion = "4.3";
			//Technically this would be 4.2.1
			realiosversion = "4.3 (8F202)";
			iNeedServiceBlob = false;
			theIPSWhash = "893cdf844a49ae2f7368e781b1ccf6d1";
		} else if (getenvbox.Text.Contains("0000DF773610A21A3EF6825ACD570E043F7932A81A5F120E651B75C943ADD6F2859686") == true) {
			//Apple TV 2
			iosversion = "4.3";
			//Technically this would be 4.2.2
			realiosversion = "4.3 (8F305)";
			iNeedServiceBlob = false;
			theIPSWhash = "4726cfb30f322f8cdbb5f20df7ca836f";
		} else if (getenvbox.Text.Contains("0000D35051F9BF04923F05AB7022A189FA4D28FDACD30F5050084C3347C65794640B5F") == true) {
			//iPhone 3GS
			iosversion = "4.3.1";
			realiosversion = "4.3.1 (8G4)";
			iNeedServiceBlob = false;
			theIPSWhash = "694c93b5b608513136ba8956dff28ba7";
		} else if (getenvbox.Text.Contains("0000145EF04D36FC276F224B118B2257679B9888D7D594A6D3C435EBEAED3A6CE13220") == true) {
			//iPhone 4
			iosversion = "4.3.1";
			realiosversion = "4.3.1 (8G4)";
			iNeedServiceBlob = false;
			theIPSWhash = "32f9a71430c4dd025adab3b73d4a5242";
		} else if (getenvbox.Text.Contains("0000499B9C19D4833289A8821682BE4DFBEC72ECAB18142B36BC8814D4172263D9496E") == true) {
			//iPod Touch 3G
			iosversion = "4.3.1";
			realiosversion = "4.3.1 (8G4)";
			iNeedServiceBlob = false;
			theIPSWhash = "47827ca8d127f28663d5b70b0784236e";
		} else if (getenvbox.Text.Contains("0000603856089DAF09532A757B068A88B1B6A6D7B3679033E40C641E4A304D31E43D44") == true) {
			//iPod Touch 4
			iosversion = "4.3.1";
			realiosversion = "4.3.1 (8G4)";
			iNeedServiceBlob = false;
			theIPSWhash = "b0e356267a1407e4d7a7b0f48a07c5c2";
		} else if (getenvbox.Text.Contains("0000AB9E3B42CB392AC0CB89FADE0C4FFEC91A66B0C0FB1A0520ECC65505D256DC2E7B") == true) {
			//iPad
			iosversion = "4.3.1";
			realiosversion = "4.3.1 (8G4)";
			iNeedServiceBlob = false;
			theIPSWhash = "fe4f80f8ff2fa298559b392b64e84bb8";
		} else if (getenvbox.Text.Contains("0000E95C7184AEECE8EECC243E1B08F181CC24F2FDE18ADA41FA4B540648714A3D985A") == true) {
			//iPad
			iosversion = "4.3.2";
			realiosversion = "4.3.2 (8H7)";
			iNeedServiceBlob = false;
			theIPSWhash = "24027c4381a6cdfdd8a03a17177d1d6c";
		} else if (getenvbox.Text.Contains("0000DB9C119159434E627A8376727C355E18A5344D6BF43ECE23206C96BBDA8F8FA88E") == true) {
			//iPod Touch 3G
			iosversion = "4.3.2";
			realiosversion = "4.3.2 (8H7)";
			iNeedServiceBlob = false;
			theIPSWhash = "7f831b30d33f80c7f92442cb041227ab";
		} else if (getenvbox.Text.Contains("00007FF8907A7BE17543F16FE1FA30CFADB940049C84676F6DCFAACF31E2A730C0049C") == true) {
			//iPod Touch 4
			iosversion = "4.3.2";
			realiosversion = "4.3.2 (8H7)";
			iNeedServiceBlob = false;
			theIPSWhash = "4a002a4596a681efd9cdbf6f2fd72e74";
		} else if (getenvbox.Text.Contains("0000D0594DA29F273E2D93718DE836C3E734705343A72A5AF64E17A3BDBB3F5365A3A8") == true) {
			//iPhone 3GS
			iosversion = "4.3.2";
			realiosversion = "4.3.2 (8H7)";
			iNeedServiceBlob = false;
			theIPSWhash = "7c1c714f24a89c2f2c71e26d37cde3f0";
		} else if (getenvbox.Text.Contains("0000E5757E577564C4C0BBFD712E238BA30276F4A6188E4BB83CCA55A8B3C62216AC88") == true) {
			//iPhone 4
			iosversion = "4.3.2";
			realiosversion = "4.3.2 (8H7)";
			iNeedServiceBlob = false;
			theIPSWhash = "8cb3a9964a2a99414030f662d3009deb";
		} else if (getenvbox.Text.Contains("00000FEEB603E31058A575A1EDA70D6FA5400F24290C1AD0CF0FE24F4431EE74265267") == true) {
			//iPhone 3GS
			iosversion = "4.3.3";
			realiosversion = "4.3.3 (8J2)";
			iNeedServiceBlob = false;
			theIPSWhash = "d9a02961311ffac8197e8db3b48e449d";
		} else if (getenvbox.Text.Contains("00003863BF0CF9C1835AE8B3D5F9253AEE81C47547CA1417BA061AD207407CE5F24F69") == true) {
			//iPhone 4
			iosversion = "4.3.3";
			realiosversion = "4.3.3 (8J2)";
			iNeedServiceBlob = false;
			theIPSWhash = "a0cb7313c5535991d62890c7eef60f9a";
		} else if (getenvbox.Text.Contains("0000C45424B0FE669DFE9E7841A7C523ED0D9575BA6E13EF1F3C07E24BB48D93F2958E") == true) {
			//iPod Touch 3G
			iosversion = "4.3.3";
			realiosversion = "4.3.3 (8J2)";
			iNeedServiceBlob = false;
			theIPSWhash = "7c8d3ccaccd1573dc31d6de555b987f9";
		} else if (getenvbox.Text.Contains("00006D7FCE831028AF1163D961173172FBEB5230F6D79060B85F913D5F31457EB6764B") == true) {
			//iPod Touch 4
			iosversion = "4.3.3";
			realiosversion = "4.3.3 (8J2)";
			iNeedServiceBlob = false;
			theIPSWhash = "dd5003cc00dbaa9fbf0182c5a2e5d6ed";
		} else if (getenvbox.Text.Contains("0000B598073066216EAB13ECFB242706BC916C440DF6347F801333E3B7851FAF20C330") == true) {
			//iPad
			iosversion = "4.3.3";
			realiosversion = "4.3.3 (8J3)";
			iNeedServiceBlob = false;
			theIPSWhash = "d20493bb1ba0450f2ee01d081ba8eb27";
		} else {
			setenv("auto-boot", "true");
			iRecovery_cmd("reset");
			Interaction.MsgBox("Unknown iOS Detected on this device! Aborting...", MsgBoxStyle.Critical);
			File_Delete("s-irecovery.exe");
			Application.Exit();
		}
		MDIMain.Text = "iFaith v" + MDIMain.VersionNumber + " -- By: iH8sn0w -- [" + iDevice + " - Running iOS: " + realiosversion + "]";
	}
	public void GoGoGadgetBlobifier()
	{
		getenvbox.Text = SubUpHex(getenvbox.Text, "0x40");
		CurrentAddr = getenvbox.Text;
		GoGoGadgetBlobifier2();
	}
	public void GoGoGadgetBlobifier2()
	{
		GoSlower = true;
		//Generates SHA1 for SHSH blob...
		iRecovery_SHA1_memory(getenvbox.Text, iFaith.AddUpHex(getenvbox.Text, "0xD0"), iFaith.AddUpHex(getenvbox.Text, "0xD0"));
		//iRecovery_FBwrite("SHA1 hash written to " & iFaith.AddUpHex(getenvbox.Text, "0xD0") & " successfully!")
		//
		Delay(1);
		while (!(i == 4)) {
			iRecovery_pie(CurrentAddr);
			if (GoSlower == true) {
				Delay(0.7);
			}
			ProgressBar1.Value = ProgressBar1.Value + 20;
			getenv("auto-boot");
			AddHex(CurrentAddr, "0x37");
			i = i + 1;
			pass.Text = "Pass # " + i + "/5";
			if (GoSlower == true) {
				Delay(0.7);
			}
		}
		//Dump the last 0x08
		iRecovery_pie2(CurrentAddr);
		if (GoSlower == true) {
			Delay(0.7);
		}
		ProgressBar1.Value = ProgressBar1.Value + 20;
		getenv("auto-boot");
		i = i + 1;
		pass.Text = "Pass # " + i + "/5";
		Delay(1);
		if (shshblob.TextLength == 456) {
		//good
		} else {
			Getrdy();
			GoSlower = true;
			GoGoGadgetBlobifier2();
			return;
		}
		if (shshblob.Text.Substring(128, 8) == "48534853" & shshblob.Text.Substring(408, 8) == "54524543") {
		//good
		} else {
			Getrdy();
			GoSlower = true;
			GoGoGadgetBlobifier2();
			return;
		}
		if (getSHA1Hash(shshblob.Text.Substring(0, 416)) == shshblob.Text.Substring(416, 40)) {
		//Were good.
		} else {
			Getrdy();
			GoSlower = true;
			GoGoGadgetBlobifier2();
			return;
		}
		GoSlower = false;
	}
	public void GrabAppleLogo()
	{
		MDIMain.TopMost = false;
		//We'll detect the version of iOS this is running...
		DetectiOS();
		if (board == "n92ap") {
			iRecovery_FBwrite("iDevice: " + iDevice + " [CDMA]");
		} else {
			iRecovery_FBwrite("iDevice: " + iDevice);
		}
		iRecovery_FBwrite("iOS: " + realiosversion);
		iRecovery_FBwrite("ECID: " + ECID);
		iRecovery_FBwrite("=========================");
		ProgressBar1.Style = ProgressBarStyle.Blocks;

		MDIMain.loadifaithtxt.ForeColor = Color.DimGray;
		MDIMain.LoadiFaith.Enabled = false;
		//
		MDIMain.AppleLogo.Enabled = true;
		MDIMain.AppleLogo.Checked = true;
		MDIMain.applelogotxt.ForeColor = Color.White;
		//
		Getrdy();


		Status.Text = "Dumping... [Please Wait]";
		Center_status();
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadlogo);
		//
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabAppleLogo();
			return;
		}

		GoGoGadgetBlobifier();

		DumpMode = false;
		AppleLogoBLOB = shshblob.Text.Substring(0, 416);

		Delay(1);

		//MsgBox("ECID: " & ECID, MsgBoxStyle.Information)

		iRecovery_FBwrite("Dumped: AppleLogo SHSH");

		Delay(1);

		Getrdy();
		sofarshsh.Text = "1/13 SHSH Blobs Captured";
		GrabBatChrg0();
	}
	public void Getrdy()
	{
		pass.Text = "Pass # 0/5";
		shshblob.Text = "";
		//getenvbox.Text = ""
		CurrentAddr = "";
		ProgressBar1.Value = 0;
		i = 0;
	}
	public void GrabBatChrg0()
	{
		MDIMain.applelogotxt.ForeColor = Color.DimGray;
		MDIMain.AppleLogo.Enabled = false;
		//
		MDIMain.batchrg0.Enabled = true;
		MDIMain.batchrg0.Checked = true;
		MDIMain.batchrg0txt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");

		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadchg0);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabBatChrg0();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Battery Charging 0 SHSH");

		DumpMode = false;

		BatCharg0BLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "2/13 SHSH Blobs Captured";
		GrabBatChrg1();
	}
	public void GrabBatChrg1()
	{
		MDIMain.batchrg0txt.ForeColor = Color.DimGray;
		MDIMain.batchrg0.Enabled = false;
		//
		MDIMain.batchrg1.Enabled = true;
		MDIMain.batchrg1.Checked = true;
		MDIMain.batchrg1txt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadchg1);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabBatChrg1();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Battery Charging 1 SHSH");

		DumpMode = false;

		BatCharg1BLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "3/13 SHSH Blobs Captured";
		GrabBatFull();
	}
	public void GrabBatFull()
	{
		MDIMain.batchrg1txt.ForeColor = Color.DimGray;
		MDIMain.batchrg1.Enabled = false;
		//
		MDIMain.batfullnsrv.Enabled = true;
		MDIMain.batfullnsrv.Checked = true;
		MDIMain.batfullnsrvtxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(LoadbatF);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabBatFull();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Battery Full SHSH");
		DumpMode = false;

		BatFullBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "4/13 SHSH Blobs Captured";
		if (iNeedServiceBlob == true) {
			Grabnsrv();
		} else {
			GrabBatLow0();
		}
	}
	public void Grabnsrv()
	{
		MDIMain.batchrg1txt.ForeColor = Color.DimGray;
		MDIMain.batchrg1.Enabled = false;
		//
		MDIMain.batfullnsrv.Enabled = true;
		MDIMain.batfullnsrv.Checked = true;
		MDIMain.batfullnsrvtxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadnsrv);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			Grabnsrv();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Need Service SHSH");
		DumpMode = false;

		NeedServiceBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "4/13 SHSH Blobs Captured";
		GrabBatLow0();
	}
	public void GrabBatLow0()
	{
		MDIMain.batfullnsrvtxt.ForeColor = Color.DimGray;
		MDIMain.batfullnsrv.Enabled = false;
		//
		MDIMain.batlow0.Enabled = true;
		MDIMain.batlow0.Checked = true;
		MDIMain.batlow0txt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadbat0);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabBatLow0();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Battery Low 0 SHSH");
		DumpMode = false;

		BatLow0BLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "5/13 SHSH Blobs Captured";
		GrabBatLow1();
	}
	public void GrabBatLow1()
	{
		MDIMain.batlow0txt.ForeColor = Color.DimGray;
		MDIMain.batlow0.Enabled = false;
		//
		MDIMain.batlow1.Enabled = true;
		MDIMain.batlow1.Checked = true;
		MDIMain.batlow1txt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadbat1);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabBatLow1();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Battery Low 1 SHSH");
		DumpMode = false;

		BatLow1BLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "6/13 SHSH Blobs Captured";
		GrabDeviceTree();
	}
	public void GrabDeviceTree()
	{
		MDIMain.batlow1txt.ForeColor = Color.DimGray;
		MDIMain.batlow1.Enabled = false;
		//
		MDIMain.devicetree.Enabled = true;
		MDIMain.devicetree.Checked = true;
		MDIMain.devicetreetxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//

		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loaddtre);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabDeviceTree();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: DeviceTree SHSH");
		DumpMode = false;

		DeviceTreeBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "7/13 SHSH Blobs Captured";
		GrabGlyphCharging();
	}
	public void GrabGlyphCharging()
	{
		MDIMain.devicetreetxt.ForeColor = Color.DimGray;
		MDIMain.devicetree.Enabled = false;
		//
		MDIMain.gchrg.Enabled = true;
		MDIMain.gchrg.Checked = true;
		MDIMain.gchrgtxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//

		//Load img3 to LDRADDRESS
		iRecovery_cmd(LoadglyC);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabGlyphCharging();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Glyph Charging SHSH");
		DumpMode = false;

		GlyphChrgBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "8/13 SHSH Blobs Captured";
		GrabGlyphPlugin();
	}
	public void GrabGlyphPlugin()
	{
		MDIMain.gchrgtxt.ForeColor = Color.DimGray;
		MDIMain.gchrg.Enabled = false;
		//
		MDIMain.gplugin.Enabled = true;
		MDIMain.gplugin.Checked = true;
		MDIMain.gplugintxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(LoadglyP);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabGlyphPlugin();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Glyph Plugin SHSH");
		DumpMode = false;

		GlyphPluginBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "9/13 SHSH Blobs Captured";
		GrabiBoot();
	}
	public void GrabiBoot()
	{
		MDIMain.gplugintxt.ForeColor = Color.DimGray;
		MDIMain.gplugin.Enabled = false;
		//
		MDIMain.iboot.Enabled = true;
		MDIMain.iboot.Checked = true;
		MDIMain.ibootxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadibot);
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabiBoot();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: iBoot SHSH");
		DumpMode = false;

		iBootBLOB = shshblob.Text.Substring(0, 416);
		Getrdy();
		sofarshsh.Text = "10/13 SHSH Blobs Captured";
		GrabLLB();
	}
	public void GrabLLB()
	{
		MDIMain.ibootxt.ForeColor = Color.DimGray;
		MDIMain.iboot.Enabled = false;
		//
		MDIMain.llb.Enabled = true;
		MDIMain.llb.Checked = true;
		MDIMain.llbtxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		if (iDevice == "iPhone 3GS" | iDevice == "iPad 1" | iDevice == "iPod Touch 2G") {
			iRecovery_cmd(Loadillb);
		} else {
			iRecovery_cmd(Loadillb_nand1);
			Delay(1);
			iRecovery_cmd(Loadillb_nand2);
		}
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabLLB();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: LLB SHSH");
		DumpMode = false;

		LLBBlob = shshblob.Text.Substring(0, 416);

		Getrdy();
		sofarshsh.Text = "11/13 SHSH Blobs Captured";
		GrabRecoveryLogo();
	}
	public void GrabRecoveryLogo()
	{
		MDIMain.llbtxt.ForeColor = Color.DimGray;
		MDIMain.llb.Enabled = false;
		//
		MDIMain.recoverylogo.Enabled = true;
		MDIMain.recoverylogo.Checked = true;
		MDIMain.recoverylogotxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS
		iRecovery_cmd(Loadrecm);

		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabRecoveryLogo();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: Recovery Logo SHSH");
		DumpMode = false;

		RecLogoBLOB = shshblob.Text.Substring(0, 416);

		Getrdy();
		sofarshsh.Text = "12/13 SHSH Blobs Captured";

		GrabKernel();
	}
	public void GrabKernel()
	{
		MDIMain.recoverylogotxt.ForeColor = Color.DimGray;
		MDIMain.recoverylogo.Enabled = false;
		//
		MDIMain.kernel.Enabled = true;
		MDIMain.kernel.Checked = true;
		MDIMain.kerneltxt.ForeColor = Color.White;
		//
		setenv("auto-boot", "true");
		//
		//Load img3 to LDRADDRESS

		//rootfs still mounted...

		//iRecovery_cmd(Loadkrnl_1)
		//Delay(2)
		iRecovery_cmd(Loadkrnl_2);
		//**********************************************************************************************************************************
		//Basically, if 3.1.x is running on these devices, then when mapping 3.2.x+ kernel path, it will simply ignore the request.
		//So, we'll pass the request to map the old kernel path aswell which will not cause any trouble for 3.2.x+ devices since it ignores
		//the request. :)
		//**********************************************************************************************************************************
		if (iDevice == "iPhone 3GS") {
			iRecovery_cmd("go fs load /boot/System/Library/Caches/com.apple.kernelcaches/kernelcache.s5l8920x 0x41000000");
		} else if (iDevice == "iPod Touch 3G") {
			iRecovery_cmd("go fs load /boot/System/Library/Caches/com.apple.kernelcaches/kernelcache.s5l8922x 0x41000000");
		}
		//
		Delay(1);
		//Search for DICE...
		iRecovery_cmd(Search_ECID);
		//
		DumpMode = false;
		//Grab Returned value...
		getenv("auto-boot");
		DumpMode = true;
		//
		Delay(1);
		//
		if (getenvbox.Text.Contains("0x") == true) {
		//Were Good
		} else {
			GrabKernel();
			return;
		}

		GoGoGadgetBlobifier();
		iRecovery_FBwrite("Dumped: rootfs Kernel SHSH");

		DumpMode = false;


		KernelBLOB = shshblob.Text.Substring(0, 416);

		Getrdy();
		sofarshsh.Text = "13/13 SHSH Blobs Captured";
		//GTFO iDevice... We dunt need u anymoar...
		iRecovery_cmd("reset");

		MDIMain.kernel.Checked = false;
		MDIMain.kernel.Enabled = false;
		MDIMain.kerneltxt.ForeColor = Color.DimGray;

		GoGoGadgetsn0wbreeze();
	}
	public void Hide_Stages()
	{
		//
		MDIMain.dfuinstructions.Visible = false;
		MDIMain.LoadiFaith.Visible = false;
		MDIMain.AppleLogo.Visible = false;
		MDIMain.batchrg0.Visible = false;
		MDIMain.batchrg1.Visible = false;
		MDIMain.batfullnsrv.Visible = false;
		MDIMain.batlow0.Visible = false;
		MDIMain.batlow1.Visible = false;
		MDIMain.devicetree.Visible = false;
		MDIMain.gchrg.Visible = false;
		MDIMain.gplugin.Visible = false;
		MDIMain.iboot.Visible = false;
		MDIMain.llb.Visible = false;
		MDIMain.recoverylogo.Visible = false;
		MDIMain.kernel.Visible = false;
		MDIMain.createipsw.Visible = false;
		MDIMain.cleanup.Visible = false;
		MDIMain.done.Visible = false;
		MDIMain.blue.Visible = false;
		//
		MDIMain.dfuinstructionstxt.Visible = false;
		MDIMain.loadifaithtxt.Visible = false;
		MDIMain.applelogotxt.Visible = false;
		MDIMain.batchrg0txt.Visible = false;
		MDIMain.batchrg1txt.Visible = false;
		MDIMain.batfullnsrvtxt.Visible = false;
		MDIMain.batlow0txt.Visible = false;
		MDIMain.batlow1txt.Visible = false;
		MDIMain.devicetreetxt.Visible = false;
		MDIMain.gchrgtxt.Visible = false;
		MDIMain.gplugintxt.Visible = false;
		MDIMain.ibootxt.Visible = false;
		MDIMain.llbtxt.Visible = false;
		MDIMain.recoverylogotxt.Visible = false;
		MDIMain.kerneltxt.Visible = false;
		MDIMain.createipswtxt.Visible = false;
		MDIMain.cleanuptxt.Visible = false;
		MDIMain.donetxt.Visible = false;
		//
	}
	public void GoGoGadgetsn0wbreeze()
	{
		MDIMain.createipsw.Enabled = true;
		MDIMain.createipsw.Checked = true;
		MDIMain.createipswtxt.ForeColor = Color.White;
		MDIMain.Activate();
		MDIMain.TopMost = false;
		//
		ProgressBar1.Style = ProgressBarStyle.Marquee;
		Status.Text = "Choose a saving Location in [3]";
		Center_status();
		Delay(1);
		Status.Text = "Choose a saving Location in [2]";
		Delay(1);
		Status.Text = "Choose a saving Location in [1]";
		Delay(1);
		Status.Text = "Choose a saving Location";
		Center_status();

		SaveBlobs.FileName = iDevice.Replace(" ", "_") + "-" + realiosversion.Replace(" ", "_") + "-blobs";
		SaveBlobs.ShowDialog();

		if (string.IsNullOrEmpty(SaveBlobs.FileName)) {
			Interaction.MsgBox("No save path was specified!" + Strings.Chr(13) + Strings.Chr(13) + "Aborting...", MsgBoxStyle.Critical);
			Application.Exit();
		} else {
			Status.Text = "Saving cache (locally)...";
			Center_status();
			//Put the xml together...
			tehfile.Text += "<?xml version=" + "\"" + "1.0" + "\"" + " encoding=" + "\"" + "utf-8" + "\"" + "?>" + Strings.Chr(13);
			tehfile.Text += "<iFaith>" + Strings.Chr(13);
			tehfile.Text += "<name start=" + "\"here\"" + ">" + Strings.Chr(13);
			tehfile.Text += "<revision>" + MDIMain.VersionNumber + "</revision>" + Strings.Chr(13);
			tehfile.Text += "<ios>" + realiosversion + "</ios>" + Strings.Chr(13);
			tehfile.Text += "<model>" + iDevice + "</model>" + Strings.Chr(13);
			tehfile.Text += "<board>" + board + "</board>" + Strings.Chr(13);
			tehfile.Text += "<ecid>" + ECID + "</ecid>" + Strings.Chr(13);
			tehfile.Text += "<logo>" + AppleLogoBLOB + "</logo>" + Strings.Chr(13);
			tehfile.Text += "<chg0>" + BatCharg0BLOB + "</chg0>" + Strings.Chr(13);
			tehfile.Text += "<chg1>" + BatCharg1BLOB + "</chg1>" + Strings.Chr(13);
			tehfile.Text += "<batf>" + BatFullBLOB + "</batf>" + Strings.Chr(13);
			tehfile.Text += "<bat0>" + BatLow0BLOB + "</bat0>" + Strings.Chr(13);
			tehfile.Text += "<bat1>" + BatLow1BLOB + "</bat1>" + Strings.Chr(13);
			tehfile.Text += "<dtre>" + DeviceTreeBLOB + "</dtre>" + Strings.Chr(13);
			tehfile.Text += "<glyc>" + GlyphChrgBLOB + "</glyc>" + Strings.Chr(13);
			tehfile.Text += "<glyp>" + GlyphPluginBLOB + "</glyp>" + Strings.Chr(13);
			tehfile.Text += "<ibot>" + iBootBLOB + "</ibot>" + Strings.Chr(13);
			tehfile.Text += "<illb>" + LLBBlob + "</illb>" + Strings.Chr(13);
			if (iosversion.Substring(0, 1) == "3") {
				tehfile.Text += "<nsrv>" + NeedServiceBLOB + "</nsrv>" + Strings.Chr(13);
			}
			tehfile.Text += "<recm>" + RecLogoBLOB + "</recm>" + Strings.Chr(13);
			tehfile.Text += "<krnl>" + KernelBLOB + "</krnl>" + Strings.Chr(13);
			tehfile.Text += "<md5>" + MD5CalcString(AppleLogoBLOB + ECID + MDIMain.VersionNumber) + "</md5>" + Strings.Chr(13);
			tehfile.Text += "<ipsw_md5>" + theIPSWhash + "</ipsw_md5>" + Strings.Chr(13);
			tehfile.Text += "</name>" + Strings.Chr(13);
			tehfile.Text += "</iFaith>";
			//
			//Write to buffer...

			tehfile.SaveFile(SaveBlobs.FileName, RichTextBoxStreamType.PlainText);
			Delay(2);
			tehfile.SaveFile(SaveBlobs.FileName, RichTextBoxStreamType.PlainText);

			//Save to remove server! :)
			if (Debug_Mode == true) {
				Interaction.MsgBox("DEBUG MODE ENABLED!" + Strings.Chr(13) + Strings.Chr(13) + "iFaith will not send this cache to the server...", MsgBoxStyle.Exclamation);
			}
			Status.Text = "Saving cache to server...";
			Center_status();


			System.Net.WebClient CacheSaver = new System.Net.WebClient();
			CacheSaver.Headers.Add("user-agent", "iacqua/1.0-452");
			//CacheSaver.BaseAddres()
			string UploadBlobRequest = null;
			UploadBlobRequest = "http://iacqua.ih8sn0w.com/req.php?";
			//Attaching Revision of iFaith...
			UploadBlobRequest = UploadBlobRequest + "revision=" + MDIMain.VersionNumber + "&";
			//Attaching iOS build...
			UploadBlobRequest = UploadBlobRequest + "ios=" + realiosversion.Replace(" ", "%20") + "&";
			//Attaching iDevice Model...
			UploadBlobRequest = UploadBlobRequest + "model=" + iDevice.Replace(" ", "%20") + "&";
			//Attaching Board Config...
			UploadBlobRequest = UploadBlobRequest + "board=" + board + "&";
			//Attaching ECID...
			UploadBlobRequest = UploadBlobRequest + "ecid=" + ECID + "&";
			//Attaching Apple Logo Blob...
			UploadBlobRequest = UploadBlobRequest + "logo=" + AppleLogoBLOB + "&";
			//Attaching Battery Charging 0 Blob...
			UploadBlobRequest = UploadBlobRequest + "chg0=" + BatCharg0BLOB + "&";
			//Attaching Battery Charging 1 Blob...
			UploadBlobRequest = UploadBlobRequest + "chg1=" + BatCharg1BLOB + "&";
			//Attaching Battery Full Blob...
			UploadBlobRequest = UploadBlobRequest + "batf=" + BatFullBLOB + "&";
			//Attaching Battery Low 0 Blob...
			UploadBlobRequest = UploadBlobRequest + "bat0=" + BatLow0BLOB + "&";
			//Attaching Battery Low 1 Blob...
			UploadBlobRequest = UploadBlobRequest + "bat1=" + BatLow1BLOB + "&";
			//Attaching DeviceTree Blob...
			UploadBlobRequest = UploadBlobRequest + "dtre=" + DeviceTreeBLOB + "&";
			//Attaching Glyph Charging Blob...
			UploadBlobRequest = UploadBlobRequest + "glyc=" + GlyphChrgBLOB + "&";
			//Attaching Glyph Plugin Blob...
			UploadBlobRequest = UploadBlobRequest + "glyp=" + GlyphPluginBLOB + "&";
			//Attaching iBoot Blob...
			UploadBlobRequest = UploadBlobRequest + "ibot=" + iBootBLOB + "&";
			//Attaching LLB Blob...
			UploadBlobRequest = UploadBlobRequest + "illb=" + LLBBlob + "&";
			//Attaching Recovery Logo Blob...
			UploadBlobRequest = UploadBlobRequest + "recm=" + RecLogoBLOB + "&";
			//Attaching Kernel Blob...
			UploadBlobRequest = UploadBlobRequest + "krnl=" + KernelBLOB + "&";
			//Attaching iFaith md5...
			UploadBlobRequest = UploadBlobRequest + "md5=" + MD5CalcString(AppleLogoBLOB + ECID + MDIMain.VersionNumber) + "&";
			//Attaching IPSW md5...
			UploadBlobRequest = UploadBlobRequest + "ipsw_md5=" + theIPSWhash + "&";
			if (iosversion.Substring(0, 1) == "3") {
				UploadBlobRequest = UploadBlobRequest + "nsrv=" + NeedServiceBLOB + "&";
			}
			Delay(1.5);
			try {
				if (Debug_Mode == false) {
					Uri UploadBlobRequestURI = new Uri(UploadBlobRequest);
					CacheSaver.DownloadStringAsync(UploadBlobRequestURI);
					while (!(CacheSaver.IsBusy == false)) {
						Delay(0.5);
					}
				}
			} catch (Exception ex) {
				Interaction.MsgBox("Unable to send request to server.", MsgBoxStyle.Critical);
			}
			//Were Done!
			MDIMain.createipsw.Enabled = false;
			MDIMain.createipsw.Checked = false;
			MDIMain.createipswtxt.ForeColor = Color.DimGray;

			Status.Text = "Done!";
			Center_status();

			MDIMain.done.Enabled = true;
			MDIMain.done.Checked = true;
			MDIMain.donetxt.ForeColor = Color.White;

			MDIMain.Text = "iFaith v" + MDIMain.VersionNumber + " -- By: iH8sn0w";


			Interaction.MsgBox("iFaith has finished saving your SHSH Blobs!" + Strings.Chr(13) + Strings.Chr(13) + "Keep them in a VERY safe spot!" + Strings.Chr(13) + "Even email them to yourself!" + Strings.Chr(13) + Strings.Chr(13) + "You will now be returned to the main menu" + Strings.Chr(13) + "where you can create a signed IPSW with your blobs." + Strings.Chr(13) + Strings.Chr(13), MsgBoxStyle.Information);


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

			MDIMain.done.Enabled = false;
			MDIMain.done.Checked = false;
			MDIMain.donetxt.ForeColor = Color.DimGray;

			Hide_Stages();
			this.Dispose();
		}
	}
	public Run()
	{
		Load += Form1_Load;
	}
}
