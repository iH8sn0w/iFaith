using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Text;
public class Welcome_ipsw
{
	public string ipswhash;
	private Utility.Http withEventsField_h = new Utility.Http();
	private Utility.Http h {
		get { return withEventsField_h; }
		set {
			if (withEventsField_h != null) {
				withEventsField_h.DownloadBegin -= h_DownloadBegin;
				withEventsField_h.DownloadProgress -= h_DownloadProgress;
			}
			withEventsField_h = value;
			if (withEventsField_h != null) {
				withEventsField_h.DownloadBegin += h_DownloadBegin;
				withEventsField_h.DownloadProgress += h_DownloadProgress;
			}
		}
	}
	private System.ComponentModel.BackgroundWorker withEventsField_get_MD5;
	private System.ComponentModel.BackgroundWorker get_MD5 {
		get { return withEventsField_get_MD5; }
		set {
			if (withEventsField_get_MD5 != null) {
				withEventsField_get_MD5.DoWork -= get_MD5_hash;
				withEventsField_get_MD5.RunWorkerCompleted -= get_md5_RunWorkerCompleted;
			}
			withEventsField_get_MD5 = value;
			if (withEventsField_get_MD5 != null) {
				withEventsField_get_MD5.DoWork += get_MD5_hash;
				withEventsField_get_MD5.RunWorkerCompleted += get_md5_RunWorkerCompleted;
			}
		}
	}
	public int Stage = 0;
	private void Welcome_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(35, 0);
	}
	public void LoadGUI()
	{
		//
		MDIMain.dfuinstructions.Visible = true;
		MDIMain.LoadiFaith.Visible = true;
		MDIMain.AppleLogo.Visible = true;
		MDIMain.batchrg0.Visible = true;
		MDIMain.batchrg1.Visible = true;
		MDIMain.batfullnsrv.Visible = true;
		MDIMain.batlow0.Visible = true;
		MDIMain.batlow1.Visible = true;
		MDIMain.devicetree.Visible = true;
		MDIMain.gchrg.Visible = true;
		MDIMain.gplugin.Visible = true;
		MDIMain.iboot.Visible = true;
		MDIMain.llb.Visible = true;
		MDIMain.recoverylogo.Visible = true;
		MDIMain.kernel.Visible = true;
		MDIMain.createipsw.Visible = true;
		MDIMain.cleanup.Visible = true;
		MDIMain.done.Visible = true;
		MDIMain.blue.Visible = true;
		//
		MDIMain.dfuinstructionstxt.Visible = true;
		MDIMain.loadifaithtxt.Visible = true;
		MDIMain.applelogotxt.Visible = true;
		MDIMain.batchrg0txt.Visible = true;
		MDIMain.batchrg1txt.Visible = true;
		MDIMain.batfullnsrvtxt.Visible = true;
		MDIMain.batlow0txt.Visible = true;
		MDIMain.batlow1txt.Visible = true;
		MDIMain.devicetreetxt.Visible = true;
		MDIMain.gchrgtxt.Visible = true;
		MDIMain.gplugintxt.Visible = true;
		MDIMain.ibootxt.Visible = true;
		MDIMain.llbtxt.Visible = true;
		MDIMain.recoverylogotxt.Visible = true;
		MDIMain.kerneltxt.Visible = true;
		MDIMain.createipswtxt.Visible = true;
		MDIMain.cleanuptxt.Visible = true;
		MDIMain.donetxt.Visible = true;
		//'
	}
	public void Clicked(System.Object sender, System.EventArgs e)
	{
		if (Loaded == false) {
			About.BringToFront();
		}
	}
	public void Show_Credits()
	{
		description1.Visible = false;
		description2.Visible = false;
		creditstitle.Visible = true;
		aking.Visible = true;
		cdev.Visible = true;
		cpich.Visible = true;
		cj.Visible = true;
		msftguy.Visible = true;
		musclenerd.Visible = true;
		neal.Visible = true;
		planetbeing.Visible = true;
		Surenix.Visible = true;
		posixninja.Visible = true;
		thepirate.Visible = true;
		sbingner.Visible = true;
		semaphore.Visible = true;
		GreySyntax.Visible = true;
		geohot.Visible = true;
	}
	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		if (Stage == 1) {
			Button3.Visible = false;
			Button2.Visible = false;
			Label2.Visible = false;
			verify.Visible = false;
			Show_Credits();
			//Go to Credits then Build
			Button1.Text = "Build IPSW";
			Stage = 2;
			return;
		} else if (Stage == 2) {
			sn0wbreeze.MdiParent = MDIMain;
			sn0wbreeze.Show();
			this.Dispose();
			return;
		}
	}
	//for credits
	private void aking_mouseenter(System.Object sender, System.EventArgs e)
	{
		aking.ForeColor = Color.Cyan;
	}
	private void aking_mouseleave(System.Object sender, System.EventArgs e)
	{
		aking.ForeColor = Color.Blue;
	}
	private void cdev_mouseenter(System.Object sender, System.EventArgs e)
	{
		cdev.ForeColor = Color.Cyan;
	}
	private void cdev_mouseleave(System.Object sender, System.EventArgs e)
	{
		cdev.ForeColor = Color.Blue;
	}
	private void cpich_mouseenter(System.Object sender, System.EventArgs e)
	{
		cpich.ForeColor = Color.Cyan;
	}
	private void cpich_mouseleave(System.Object sender, System.EventArgs e)
	{
		cpich.ForeColor = Color.Blue;
	}
	private void cj_mouseenter(System.Object sender, System.EventArgs e)
	{
		cj.ForeColor = Color.Cyan;
	}
	private void cj_mouseleave(System.Object sender, System.EventArgs e)
	{
		cj.ForeColor = Color.Blue;
	}
	private void msftguy_mouseenter(System.Object sender, System.EventArgs e)
	{
		msftguy.ForeColor = Color.Cyan;
	}
	private void msftguy_mouseleave(System.Object sender, System.EventArgs e)
	{
		msftguy.ForeColor = Color.Blue;
	}
	private void musclenerd_mouseenter(System.Object sender, System.EventArgs e)
	{
		musclenerd.ForeColor = Color.Cyan;
	}
	private void musclenerd_mouseleave(System.Object sender, System.EventArgs e)
	{
		musclenerd.ForeColor = Color.Blue;
	}
	private void neal_mouseenter(System.Object sender, System.EventArgs e)
	{
		neal.ForeColor = Color.Cyan;
	}
	private void neal_mouseleave(System.Object sender, System.EventArgs e)
	{
		neal.ForeColor = Color.Blue;
	}
	private void planetbeing_mouseenter(System.Object sender, System.EventArgs e)
	{
		planetbeing.ForeColor = Color.Cyan;
	}
	private void planetbeing_mouseleave(System.Object sender, System.EventArgs e)
	{
		planetbeing.ForeColor = Color.Blue;
	}
	private void surenix_mouseenter(System.Object sender, System.EventArgs e)
	{
		Surenix.ForeColor = Color.Cyan;
	}
	private void surenix_mouseleave(System.Object sender, System.EventArgs e)
	{
		Surenix.ForeColor = Color.Blue;
	}
	private void posixninja_mouseenter(System.Object sender, System.EventArgs e)
	{
		posixninja.ForeColor = Color.Cyan;
	}
	private void posixninja_mouseleave(System.Object sender, System.EventArgs e)
	{
		posixninja.ForeColor = Color.Blue;
	}
	private void thepirate_mouseenter(System.Object sender, System.EventArgs e)
	{
		thepirate.ForeColor = Color.Cyan;
	}
	private void thepirate_mouseleave(System.Object sender, System.EventArgs e)
	{
		thepirate.ForeColor = Color.Blue;
	}
	private void semaphore_mouseenter(System.Object sender, System.EventArgs e)
	{
		semaphore.ForeColor = Color.Cyan;
	}
	private void semaphore_mouseleave(System.Object sender, System.EventArgs e)
	{
		semaphore.ForeColor = Color.Blue;
	}
	private void sbingner_mouseenter(System.Object sender, System.EventArgs e)
	{
		sbingner.ForeColor = Color.Cyan;
	}
	private void sbingner_mouseleave(System.Object sender, System.EventArgs e)
	{
		sbingner.ForeColor = Color.Blue;
	}
	private void greysyntax_mouseenter(System.Object sender, System.EventArgs e)
	{
		GreySyntax.ForeColor = Color.Cyan;
	}
	private void greysyntax_mouseleave(System.Object sender, System.EventArgs e)
	{
		GreySyntax.ForeColor = Color.Blue;
	}
	//
	private void geohot_mouseenter(System.Object sender, System.EventArgs e)
	{
		geohot.ForeColor = Color.Cyan;
	}
	private void geohot_mouseleave(System.Object sender, System.EventArgs e)
	{
		geohot.ForeColor = Color.Blue;
	}
	private void aking_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/AKi_nG");
	}
	private void cdev_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/chronicdevteam");
	}
	private void cpich_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/cpich3g");
	}
	private void cj_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/iC_J");
	}
	private void msftguy_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/msftguy");
	}
	private void musclenerd_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/MuscleNerd");
	}
	private void neal_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/iNeal11");
	}
	private void planetbeing_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/planetbeing");
	}
	private void Surenix_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/iSurenix");
	}
	private void posixninja_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/p0sixninja");
	}
	private void thepirate_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/ThePiratep");
	}
	private void semaphore_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/notcom");
	}
	private void sbingner_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/sbingner");
	}
	private void GreySyntax_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://twitter.com/GreySyntax");
	}
	private void geohot_Click(System.Object sender, System.EventArgs e)
	{
		Process.Start("http://geohot.com");
	}
	//
	private void Button2_Click(System.Object sender, System.EventArgs e)
	{
		verify.Visible = false;
		Button1.Visible = false;
		OpenFileDialog1.ShowDialog();
		if (string.IsNullOrEmpty(OpenFileDialog1.FileName)) {
			return;
		} else {
			spinny.Visible = true;
			Button2.Visible = false;
			Delay(1.5);
			try {
				//Parsing time baby!!!
				//Load up xml...
				XmlTextReader m_xmlr = null;
				m_xmlr = new XmlTextReader(OpenFileDialog1.FileName);
				m_xmlr.WhitespaceHandling = WhitespaceHandling.None;
				m_xmlr.Read();
				m_xmlr.Read();
				while (!m_xmlr.EOF) {
					m_xmlr.Read();
					if (!m_xmlr.IsStartElement()) {
						break; // TODO: might not be correct. Was : Exit While
					}
					dynamic iFaithAttribute = m_xmlr.GetAttribute("iFaith");
					m_xmlr.Read();
					xml_revision = m_xmlr.ReadElementString("revision");
					xml_ios = m_xmlr.ReadElementString("ios");
					xml_model = m_xmlr.ReadElementString("model");
					xml_board = m_xmlr.ReadElementString("board");
					xml_ecid = m_xmlr.ReadElementString("ecid");
					blob_logo = m_xmlr.ReadElementString("logo");
					blob_chg0 = m_xmlr.ReadElementString("chg0");
					blob_chg1 = m_xmlr.ReadElementString("chg1");
					blob_batf = m_xmlr.ReadElementString("batf");
					blob_bat0 = m_xmlr.ReadElementString("bat0");
					blob_bat1 = m_xmlr.ReadElementString("bat1");
					blob_dtre = m_xmlr.ReadElementString("dtre");
					blob_glyc = m_xmlr.ReadElementString("glyc");
					blob_glyp = m_xmlr.ReadElementString("glyp");
					blob_ibot = m_xmlr.ReadElementString("ibot");
					blob_illb = m_xmlr.ReadElementString("illb");
					if (xml_ios.Substring(0, 1) == "3") {
						blob_nsrv = m_xmlr.ReadElementString("nsrv");
					}
					blob_recm = m_xmlr.ReadElementString("recm");
					blob_krnl = m_xmlr.ReadElementString("krnl");
					xml_md5 = m_xmlr.ReadElementString("md5");
					xml_ipsw_md5 = m_xmlr.ReadElementString("ipsw_md5");
				}
				m_xmlr.Close();
			} catch (Exception Ex) {
				Interaction.MsgBox("Error while processing specified file!", MsgBoxStyle.Critical);
				spinny.Visible = false;
				Button2.Visible = true;
				return;
			}
		}

		//Were going to Check to see if this was made with a newer iFaith revision...
		if (xml_revision > MDIMain.VersionNumber) {
			Interaction.MsgBox("This iFaith SHSH Cache file was made with iFaith v" + xml_revision + Strings.Chr(13) + Strings.Chr(13) + "Please download the latest iFaith revision at http://ih8sn0w.com", MsgBoxStyle.Critical);
			spinny.Visible = false;
			Button2.Visible = true;
			return;
		}

		//Hashing time! :)
		if (MD5CalcString(blob_logo + xml_ecid + xml_revision) == xml_md5) {
			if (xml_board == "n72ap") {
				Interaction.MsgBox("iPod Touch 2G IPSW Creation is still being worked on.", MsgBoxStyle.Exclamation);
				spinny.Visible = false;
				Button2.Visible = true;
				return;
			}
			//Load IPSW Pwner...
			verify.Visible = false;
			//Hide Logo + Welcome txt...
			PictureBox1.Visible = false;
			spinny.Visible = false;
			Label1.Visible = false;
			//
			ORlabel.Visible = true;
			dl4mebtn.Visible = true;
			browse4ios.Visible = true;
			browse4ios.Text = "Browse for the " + xml_ios;
			browse4ios.ForeColor = Color.Cyan;
			browse4ios.Left = (Width / 2) - (browse4ios.Width / 2);
			if (xml_board == "n92ap") {
				Label2.Text = "IPSW for the iPhone 4 (CDMA)";
			} else {
				Label2.Text = "IPSW for the " + xml_model;
			}
			Label2.ForeColor = Color.Cyan;
			Label2.Left = (Width / 2) - (Label2.Width / 2);
			Button3.Text = "Browse for the iOS " + xml_ios + " IPSW";
			Button3.Left = (Width / 2) - (Button3.Width / 2);
			Button2.Visible = false;
			Button3.Visible = true;
			Button1.Visible = false;
			Stage = 1;
		} else {
			Interaction.MsgBox("Invalid iFaith Cache!", MsgBoxStyle.Critical);
			spinny.Visible = false;
			Button2.Visible = true;
			return;

		}


	}

	public void get_MD5_hash(object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		//MD5 hash provider for computing the hash of the file
		MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

		//open the file
		FileStream stream = new FileStream(ipsw, FileMode.Open, FileAccess.Read, FileShare.Read, 8192);

		//calculate the files hash
		md5.ComputeHash(stream);

		//close our stream
		stream.Close();

		//byte array of files hash
		byte[] hash = md5.Hash;

		//string builder to hold the results
		StringBuilder sb = new StringBuilder();

		//loop through each byte in the byte array
		foreach (byte b in hash) {
			//format each byte into the proper value and append
			//current value to return value
			sb.Append(string.Format("{0:X2}", b));
		}

		//return the MD5 hash of the file
		ipswhash = sb.ToString().ToLower();
	}
	private void get_md5_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
	{
		//Alright. Lets check it now...
		if (ipswhash == xml_ipsw_md5) {
			//Full speed ahead!
			spinny.Visible = false;
			verify.Visible = false;
			//
			PictureBox1.Visible = true;
			Label1.Visible = true;
			browse4ios.Visible = false;
			Button3.Visible = false;
			Button1.Visible = true;
			Button2.Visible = false;
			Label2.Visible = false;
			verify.Visible = false;
			Show_Credits();
			//Go to Credits then Build
			Button1.Text = "Build IPSW";
			Stage = 2;
		} else {
			spinny.Visible = false;
			verify.Visible = false;
			dl4mebtn.Visible = true;
			ORlabel.Visible = true;
			Button3.Visible = true;
			ipswhash = "";
			browse4ios.Text = "Browse for the " + xml_ios;
			browse4ios.Left = (Width / 2) - (browse4ios.Width / 2);
			if (xml_board == "n92ap") {
				Label2.Text = "IPSW for the iPhone 4 (CDMA)";
			} else {
				Label2.Text = "IPSW for the " + xml_model;
			}
			Label2.ForeColor = Color.Cyan;
			Label2.Left = (Width / 2) - (Label2.Width / 2);
			Interaction.MsgBox("Invalid " + xml_ios + " IPSW for the " + xml_model + "!", MsgBoxStyle.Critical);
		}
	}
	private void Button3_Click(System.Object sender, System.EventArgs e)
	{
		OpenFileDialog2.ShowDialog();
		if (string.IsNullOrEmpty(OpenFileDialog2.FileName)) {
			//No IPSW Selected... Ignore.
			return;
		} else {
			//md5 the bitch!
			dl4mebtn.Visible = false;
			ORlabel.Visible = false;
			Button3.Visible = false;
			spinny.Visible = true;
			verify.ForeColor = Color.Cyan;
			verify.Text = "Verifying IPSW...";
			verify.Left = 15;
			verify.Visible = true;
			ipsw = OpenFileDialog2.FileName;
			get_MD5 = new System.ComponentModel.BackgroundWorker();
			get_MD5.WorkerReportsProgress = true;
			get_MD5.WorkerSupportsCancellation = true;
			get_MD5.RunWorkerAsync();
			int i = 0;
			while (!(i == 1)) {
				if (string.IsNullOrEmpty(ipswhash)) {
				//Continue
				} else {
					return;
				}
				if (verify.Text.Length == 109) {
					verify.Text = "Verifying IPSW.";
				}
				verify.Text = verify.Text + ".";
				Delay(0.01);
			}
			return;
		}
	}

	private void Button4_Click(System.Object sender, System.EventArgs e)
	{
		// Display a child form.
		Button4.Enabled = false;
		this.Enabled = false;
		CancelDownload = true;
		if (Worker.IsBusy == true) {
			Worker.CancelAsync();
		}
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
		Delay(1);
		this.Dispose();
	}
	public void GrabIPSWURL()
	{
		if (xml_ipsw_md5 == "38638d6056b53f2d87a0f5fcb5584cdd") {
			//iPhone 3GS  3.1/7C144 
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone/061-6609.20090909.mwws4/iPhone2,1_3.1_7C144_Restore.ipsw";
		} else if (xml_ipsw_md5 == "e0c97bdbb9efbf411b22a81327ad48dc") {
			//iPod Touch 2G 3.1.1/7C145
			IPSWurl = "NO-DL";
			// :~(
		} else if (xml_ipsw_md5 == "4ad01a2c6fc82bcac2300253b0368f6e") {
			//iPod Touch 3G 3.1.1/7C145  
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPod/SBML/osx/bundles/061-7163.20090909.NtstR/iPod3,1_3.1.1_7C145_Restore.ipsw";
		} else if (xml_ipsw_md5 == "089769d37b846917394ffe11da9d2e17") {
			//iPhone 3GS 3.1.2/7D11
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone/061-7270.20091008.phn32/iPhone2,1_3.1.2_7D11_Restore.ipsw";
		} else if (xml_ipsw_md5 == "35c66be376201082a005f0a289f26a65") {
			//iPod Touch 2G 3.1.2/7D11
			IPSWurl = "NO-DL";
			// :~(
		} else if (xml_ipsw_md5 == "13938eaca91e12e7cefb47717e7cadc8") {
			//iPod Touch 3G 3.1.2/7D11
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone/061-7271.20091008.Tch23/iPod3,1_3.1.2_7D11_Restore.ipsw";
		} else if (xml_ipsw_md5 == "4117e4b22565e69205a84e9eeef0583e") {
			//iPhone 3GS 3.1.3/7E18
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone/061-7472.20100202.8tugj/iPhone2,1_3.1.3_7E18_Restore.ipsw";
		} else if (xml_ipsw_md5 == "33df8d6ae5d8a695bba267ae89fe37f1") {
			//iPod Touch 2G 3.1.3/7E18
			IPSWurl = "NO-DL";
			// :~(
		} else if (xml_ipsw_md5 == "a73de2cfafef3463e9afa491f20c5213") {
			//iPod Touch 3G 3.1.3/7E18
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone/061-7473.20100202.4i44t/iPod3,1_3.1.3_7E18_Restore.ipsw";
		} else if (xml_ipsw_md5 == "2912cefa0304e5430594c576ad88d398") {
			//iPad 3.2/7B367
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPad/061-7987.20100403.mjiTr/iPad1,1_3.2_7B367_Restore.ipsw";
		} else if (xml_ipsw_md5 == "5ccf846d96a677f42ac183f5a137dc92") {
			//iPad 3.2.1/7B405
			IPSWurl = "http://appldnld.apple.com/iPad/061-8282.20100713.vgtgh/iPad1,1_3.2.1_7B405_Restore.ipsw";
		} else if (xml_ipsw_md5 == "cf6d93fffdc60dcca487a80004d250fa") {
			//iPad 3.2.2/7B500
			IPSWurl = "http://appldnld.apple.com/iPad/061-8801.20100811.CvfR5/iPad1,1_3.2.2_7B500_Restore.ipsw";
		} else if (xml_ipsw_md5 == "f9819ad9a52324ac6f10e4a0ea581cbd") {
			//iPhone 3GS 4.0/8A293
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone4/061-7437.20100621.5urG8/iPhone2,1_4.0_8A293_Restore.ipsw";
		} else if (xml_ipsw_md5 == "8717be79fb38cd83aa5e5956eb0608b7") {
			//iPhone 4 4.0/8A293
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone4/061-7380.20100621,Vfgb5/iPhone3,1_4.0_8A293_Restore.ipsw";
		} else if (xml_ipsw_md5 == "41dd8ab40159a13d7be42cd7e5f3a479") {
			//iPod Touch 2G 4.0/8A293
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone4/061-7435.20100621.tr49t/iPod2,1_4.0_8A293_Restore.ipsw";
		} else if (xml_ipsw_md5 == "6b9d65c9f63792968bad57e44a73434f") {
			//iPod Touch 3G 4.0/8A293
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone4/061-7381.20100621.AzSP9/iPod3,1_4.0_8A293_Restore.ipsw";
		} else if (xml_ipsw_md5 == "a3104ca3b72a91bc7eff037ee320ecc5") {
			//iPhone 3GS 4.0.1/8A306
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8618.20100715.Zapn4/iPhone2,1_4.0.1_8A306_Restore.ipsw";
		} else if (xml_ipsw_md5 == "40ebacb47fb32d7f33ba0fd596e150e9") {
			//iPhone 4 4.0.1/8A306
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8619.20100715.4Pnsx/iPhone3,1_4.0.1_8A306_Restore.ipsw";
		} else if (xml_ipsw_md5 == "9cb5684457fb41886827d727d91313c3") {
			//iPhone 3GS 4.0.2/8A400
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8805.20100811.Dcr4e/iPhone2,1_4.0.2_8A400_Restore.ipsw";
		} else if (xml_ipsw_md5 == "790b24fe7515084f457ce413618b2709") {
			//iPhone 4 4.0.2/8A400
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8807.20100811.3Edre/iPhone3,1_4.0.2_8A400_Restore.ipsw";
		} else if (xml_ipsw_md5 == "e706efcf835de9fcf6f96c7a420a7a22") {
			//iPod Touch 2G 4.0.2/8A400
			IPSWurl = "http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iPhone4/061-7435.20100621.tr49t/iPod2,1_4.0_8A293_Restore.ipsw";
		} else if (xml_ipsw_md5 == "dc7741b9e4353895c3910237a5b10a4d") {
			//iPod Touch 3G 4.0.2/8A400
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8554.20100811.Bgt54/iPod3,1_4.0.2_8A400_Restore.ipsw";
		} else if (xml_ipsw_md5 == "e07bee3c03e7a18e5d75fcaa23db17b5") {
			//iPhone 3GS 4.1/8B117
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-7938.20100908.F3rCk/iPhone2,1_4.1_8B117_Restore.ipsw";
		} else if (xml_ipsw_md5 == "ac3031a7b5c013d6a09952b691985878") {
			//iPhone 4 4.1/8B117
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-7939.20100908.Lcyg3/iPhone3,1_4.1_8B117_Restore.ipsw";
		} else if (xml_ipsw_md5 == "9f8a1978f053ec96926e535bb57ac171") {
			//iPod Touch 2G 4.1/8B117
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-7937.20100908.ghj4f/iPod2,1_4.1_8B117_Restore.ipsw";
		} else if (xml_ipsw_md5 == "f3877c6f309730ee31297a06c7a9e82c") {
			//iPod Touch 3G 4.1/8B117
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-7941.20100908.sV9KE/iPod3,1_4.1_8B117_Restore.ipsw";
		} else if (xml_ipsw_md5 == "2e634d16d0e01ef70070c9a289e488ca") {
			//iPod Touch 4 -- 8B117 4.1/8B117
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8490.20100901.hyjtR/iPod4,1_4.1_8B117_Restore.ipsw";
		} else if (xml_ipsw_md5 == "0564fcd3f53dd6262b9eb636b7fbe540") {
			//iPod Touch 4 -- 8B118 4.1/8B118
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9344.20100922.Urgt43/iPod4,1_4.1_8B118_Restore.ipsw";
		} else if (xml_ipsw_md5 == "35c8ab4b7e70ab0e47e2f5981e52ba55") {
			//Apple TV 2 4.1/8M89
			IPSWurl = "http://appldnld.apple.com/AppleTV/061-8940.20100926.Tvtnz/AppleTV2,1_4.1_8M89_Restore.ipsw";
		} else if (xml_ipsw_md5 == "d688d2d48c8b054367adef8e7ab4f5ea") {
			//iPhone 3GS -- 4.2.1a 4.2.1/8C148a
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9895.20101122.Cdew2/iPhone2,1_4.2.1_8C148a_Restore.ipsw";
		} else if (xml_ipsw_md5 == "93957e7bd21f0549b60a60485c13206a") {
			//iPhone 4 4.2.1/8C148
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9858.20101122.Er456/iPhone3,1_4.2.1_8C148_Restore.ipsw";
		} else if (xml_ipsw_md5 == "0045e3543647e23470b84c2c1de96ab1") {
			//iPod Touch 2G 4.2.1/8C148
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9855.20101122.Lrft6/iPod2,1_4.2.1_8C148_Restore.ipsw";
		} else if (xml_ipsw_md5 == "25dbf5b3e5ca39edd0aab8fcab888503") {
			//iPod Touch 3G 4.2.1/8C148
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9860.20101122.Xsde3/iPod3,1_4.2.1_8C148_Restore.ipsw";
		} else if (xml_ipsw_md5 == "14d1508954532e91172f8704fd941a93") {
			//iPod Touch 4 4.2.1/8C148
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9859.20101122.$erft/iPod4,1_4.2.1_8C148_Restore.ipsw";
		} else if (xml_ipsw_md5 == "9402d5f05348fd68c87f885ff4cb4717") {
			//iPad 4.2.1/8C148
			IPSWurl = "http://appldnld.apple.com/iPad/061-9857.20101122.VGthy/iPad1,1_4.2.1_8C148_Restore.ipsw";
		} else if (xml_ipsw_md5 == "3fe1a01b8f5c8425a074ffd6deea7c86") {
			//Apple TV 2 4.2.1/8C154
			IPSWurl = "http://appldnld.apple.com/AppleTV/061-9978.20101214.gmabr/AppleTV2,1_4.2.1_8C154_Restore.ipsw";
		} else if (xml_ipsw_md5 == "eb3c205debb52c237c37f92335e6369c") {
			//Verizon iPhone 4 4.2.6/8E200
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0177.20110131.Pyvrz/iPhone3,3_4.2.6_8E200_Restore.ipsw";
		} else if (xml_ipsw_md5 == "30fc03783453d23aaa0d13f89fd36c28") {
			//Verizon iPhone 4 4.2.7/8E303
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0614.20110414.B47xa/iPhone3,3_4.2.7_8E303_Restore.ipsw";
		} else if (xml_ipsw_md5 == "0e0e4bf8f0d7c37b9a252fcbed60ac0c") {
			//Verizon iPhone 4 4.2.8/8E401
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1022.20110503.5g8k7/iPhone3,3_4.2.8_8E401_Restore.ipsw";
		} else if (xml_ipsw_md5 == "87ebb9b2c025fb5f87a4cab0631b1547") {
			//iPhone 3GS 4.3/8F190
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0328.20110311.Lkhy6/iPhone2,1_4.3_8F190_Restore.ipsw";
		} else if (xml_ipsw_md5 == "e0a463bded8f5b1e076b466535b18c75") {
			//iPhone 4 4.3/8F190
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0330.20110311.Cswe3/iPhone3,1_4.3_8F190_Restore.ipsw";
		} else if (xml_ipsw_md5 == "43383f2d5cd181f2af1e01ec62a3f1d6") {
			//iPod Touch 3G 4.3/8F190
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-8366.20110311.Fr45t/iPod3,1_4.3_8F190_Restore.ipsw";
		} else if (xml_ipsw_md5 == "0c8cdbbb729508811fa5bd29d8e1143b") {
			//iPod Touch 4 4.3/8F190
			IPSWurl = "http://appldnld.apple.com/iPhone4/061-9588.20110311.GtP7y/iPod4,1_4.3_8F190_Restore.ipsw";
		} else if (xml_ipsw_md5 == "9a889ba48bc2715292f199f50c70ed60") {
			//iPad 4.3/8F190
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0329.20110311.Cvfre/iPad1,1_4.3_8F190_Restore.ipsw";
		} else if (xml_ipsw_md5 == "893cdf844a49ae2f7368e781b1ccf6d1") {
			//Apple TV 2 4.3/8F202
			IPSWurl = "http://appldnld.apple.com/AppleTV/041-0574.20110322.Dcfr5/AppleTV2,1_4.3_8F202_Restore.ipsw";
		} else if (xml_ipsw_md5 == "4726cfb30f322f8cdbb5f20df7ca836f") {
			//Apple TV 2 4.3/8F305
			IPSWurl = "http://appldnld.apple.com/AppleTV/041-0596.20110511.Zz7mC/AppleTV2,1_4.3_8F305_Restore.ipsw";
		} else if (xml_ipsw_md5 == "694c93b5b608513136ba8956dff28ba7") {
			//iPhone 3GS 4.3.1/8G4
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0549.20110325.ZxP8u/iPhone2,1_4.3.1_8G4_Restore.ipsw";
		} else if (xml_ipsw_md5 == "32f9a71430c4dd025adab3b73d4a5242") {
			//iPhone 4 4.3.1/8G4
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0551.20110325.Aw2Dr/iPhone3,1_4.3.1_8G4_Restore.ipsw";
		} else if (xml_ipsw_md5 == "47827ca8d127f28663d5b70b0784236e") {
			//iPod Touch 3G 4.3.1/8G4
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0552.20110325.Yt67u/iPod3,1_4.3.1_8G4_Restore.ipsw";
		} else if (xml_ipsw_md5 == "b0e356267a1407e4d7a7b0f48a07c5c2") {
			//iPod Touch 4 4.3.1/8G4
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0545.20110325.We3Rt/iPod4,1_4.3.1_8G4_Restore.ipsw";
		} else if (xml_ipsw_md5 == "fe4f80f8ff2fa298559b392b64e84bb8") {
			//iPad 4.3.1/8G4
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0550.20110325.Zsw6y/iPad1,1_4.3.1_8G4_Restore.ipsw";
		} else if (xml_ipsw_md5 == "24027c4381a6cdfdd8a03a17177d1d6c") {
			//iPad 4.3.2/8H7
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0661.20110414.W9Q8r/iPad1,1_4.3.2_8H7_Restore.ipsw";
		} else if (xml_ipsw_md5 == "7f831b30d33f80c7f92442cb041227ab") {
			//iPod Touch 3G 4.3.2/8H7
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0667.20110414.95hVL/iPod3,1_4.3.2_8H7_Restore.ipsw";
		} else if (xml_ipsw_md5 == "4a002a4596a681efd9cdbf6f2fd72e74") {
			//iPod Touch 4 4.3.2/8H7
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0666.20110414.3QvM1/iPod4,1_4.3.2_8H7_Restore.ipsw";
		} else if (xml_ipsw_md5 == "7c1c714f24a89c2f2c71e26d37cde3f0") {
			//iPhone 3GS 4.3.2/8H7
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0660.20110414.Gwed5/iPhone2,1_4.3.2_8H7_Restore.ipsw";
		} else if (xml_ipsw_md5 == "8cb3a9964a2a99414030f662d3009deb") {
			//iPhone 4 4.3.2/8H7
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-0662.20110414.byQ84/iPhone3,1_4.3.2_8H7_Restore.ipsw";
		} else if (xml_ipsw_md5 == "d9a02961311ffac8197e8db3b48e449d") {
			//iPhone 3GS 4.3.3/8J2
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1009.20110503.M73Yr/iPhone2,1_4.3.3_8J2_Restore.ipsw";
		} else if (xml_ipsw_md5 == "a0cb7313c5535991d62890c7eef60f9a") {
			//iPhone 4 4.3.3/8J2
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1011.20110503.q7fGc/iPhone3,1_4.3.3_8J2_Restore.ipsw";
		} else if (xml_ipsw_md5 == "7c8d3ccaccd1573dc31d6de555b987f9") {
			//iPod Touch 3G 4.3.3/8J2
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1021.20110503.8Lfs1/iPod3,1_4.3.3_8J2_Restore.ipsw";
		} else if (xml_ipsw_md5 == "dd5003cc00dbaa9fbf0182c5a2e5d6ed") {
			//iPod Touch 4 4.3.3/8J2
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1015.20110503.d7i57/iPod4,1_4.3.3_8J2_Restore.ipsw";
		} else if (xml_ipsw_md5 == "d20493bb1ba0450f2ee01d081ba8eb27") {
			//iPad 4.3.3/8J3
			IPSWurl = "http://appldnld.apple.com/iPhone4/041-1010.20110503.ScEp3/iPad1,1_4.3.3_8J3_Restore.ipsw";
		}
	}
	private void Button5_Click(System.Object sender, System.EventArgs e)
	{
		//Grab URL from MD5...
		GrabIPSWURL();
		if (IPSWurl == "NO-DL") {
			Interaction.MsgBox("There currently is no public download link for this firmware!", MsgBoxStyle.Critical);
			return;
		}
		CancelDownload = false;
		browse4ios.Text = "Downloading IPSW...";
		browse4ios.Left = (Width / 2) - (browse4ios.Width / 2);
		Label2.Text = "(Please Wait)";
		Label2.Left = (Width / 2) - (Label2.Width / 2);
		//Hide others...
		Button3.Visible = false;
		ORlabel.Visible = false;
		dl4mebtn.Visible = false;
		//
		ProgressBar1.Value = 0;
		ProgressBar1.Visible = true;
		//Start the downloader...
		progresstxt.Text = string.Empty;
		progresstxt.Visible = true;
		progresstxt.Text = "Waiting for Server...";
		Delay(1);
		ProgressBar1.Value = 0;
		var _with1 = Worker;
		_with1.WorkerSupportsCancellation = true;
		try {
			if (_with1.IsBusy == false)
				_with1.RunWorkerAsync();
		} catch (Exception ex) {
		}
	}
	private delegate void DownloadDelegate(int Value);
	private void h_DownloadBegin(int Length)
	{
		if (this.InvokeRequired) {
			this.Invoke(new DownloadDelegate(h_DownloadBegin), Length);
		} else {
			ProgressBar1.Maximum = Length;
			progresstxt.Text = "0 / " + Length;
			Label2.Text = "0%";
		}
	}
	private void h_DownloadProgress(int Current)
	{
		if (CancelDownload == true) {
			if (xml_board == "n92ap") {
				Label2.Text = "IPSW for the iPhone 4 (CDMA)";
			} else {
				Label2.Text = "IPSW for the " + xml_model;
			}
			Label2.ForeColor = Color.Cyan;
			Label2.Left = (Width / 2) - (Label2.Width / 2);
			return;
		}
		if (this.InvokeRequired) {
			if (CancelDownload == true) {
				if (xml_board == "n92ap") {
					Label2.Text = "IPSW for the iPhone 4 (CDMA)";
				} else {
					Label2.Text = "IPSW for the " + xml_model;
				}
				Label2.ForeColor = Color.Cyan;
				Label2.Left = (Width / 2) - (Label2.Width / 2);
				return;
			}
			this.Invoke(new DownloadDelegate(h_DownloadProgress), Current);
		} else {
			if (CancelDownload == true) {
				if (xml_board == "n92ap") {
					Label2.Text = "IPSW for the iPhone 4 (CDMA)";
				} else {
					Label2.Text = "IPSW for the " + xml_model;
				}
				Label2.ForeColor = Color.Cyan;
				Label2.Left = (Width / 2) - (Label2.Width / 2);
				return;
			}
			ProgressBar1.Value = Current;
			progresstxt.Text = (Current / 1048576).ToString("N") + " MB of " + (ProgressBar1.Maximum / 1048576).ToString("N") + " MB downloaded";
			Label2.Text = GetPercent(Current, ProgressBar1.Maximum) + "%";
		}
		this.Invoke((MethodInvoker)Center_Label2);
		if (Label2.Text == "100%") {
			browse4ios.Text = "Processing IPSW...";
			browse4ios.Left = (Width / 2) - (browse4ios.Width / 2);
			progresstxt.Text = "Writing IPSW to Hard drive...";
			Label2.Text = "(Please Wait)";
			Center_Label2();
			Application.DoEvents();
		}
	}
	private string GetFileNameFromURL(string URL)
	{
		if (URL.IndexOf('/') == -1)
			return string.Empty;
		return "\\" + URL.Substring(URL.LastIndexOf('/') + 1);
	}
	public void Center_Label2()
	{
		Label2.Left = (Width / 2) - (Label2.Width / 2);
	}
	private void cancelbtn_Click(System.Object sender, System.EventArgs e)
	{
		Worker.CancelAsync();
		CancelDownload = true;

		progresstxt.Visible = false;
		ProgressBar1.Visible = false;

		Button3.Visible = true;
		dl4mebtn.Visible = true;
		ORlabel.Visible = true;

		browse4ios.Text = "Browse for the " + xml_ios;
		browse4ios.Left = (Width / 2) - (browse4ios.Width / 2);
		if (xml_board == "n92ap") {
			Label2.Text = "IPSW for the iPhone 4 (CDMA)";
		} else {
			Label2.Text = "IPSW for the " + xml_model;
		}
		Label2.ForeColor = Color.Cyan;
		Label2.Left = (Width / 2) - (Label2.Width / 2);
	}
	public int GetPercent(int CurrVal, int MaxVal)
	{
		return (CurrVal / MaxVal * 100 + 0.5);
	}
	public void WeGOTit()
	{
		if (CancelDownload == true) {
			return;
		}
		string DownloadedIPSWFileName = IPSWurl.Substring(IPSWurl.LastIndexOf('/') + 1);
		//Wait for File to stop being written...
		while (!(FileInUse(temppath + "\\" + DownloadedIPSWFileName) == false)) {
			Delay(0.5);
		}
		//
		//md5 the bitch!
		progresstxt.Visible = false;
		ProgressBar1.Visible = false;

		dl4mebtn.Visible = false;
		ORlabel.Visible = false;
		Button3.Visible = false;
		spinny.Visible = true;
		verify.Text = "Verifying IPSW...";
		verify.Left = 15;
		verify.Visible = true;
		ipsw = temppath + "\\" + DownloadedIPSWFileName;
		get_MD5 = new System.ComponentModel.BackgroundWorker();
		get_MD5.WorkerReportsProgress = true;
		get_MD5.WorkerSupportsCancellation = true;
		get_MD5.RunWorkerAsync();
		int i = 0;
		while (!(i == 1)) {
			if (string.IsNullOrEmpty(ipswhash)) {
			//Continue
			} else {
				return;
			}
			if (verify.Text.Length == 109) {
				verify.Text = "Verifying IPSW.";
			}
			verify.Text = verify.Text + ".";
			Delay(0.01);
		}
		return;
	}

	private void Worker_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		var _with2 = h;
		object Request = _with2.GetFile(IPSWurl, string.Empty, temppath + "\\" + IPSWurl.Substring(IPSWurl.LastIndexOf('/') + 1));
		if (Request == null) {
		// Downloading
		} else {
			switch (Request.GetType().ToString()) {
				case "System.Net.HttpWebResponse":
					break;
				default:
					switch (Request.GetType().ToString()) {
						case "System.Net.WebException":
							Debug.Print(((System.Net.WebException)Request).Message);
							break;
						case "System.Exception":
							Debug.Print(((System.Exception)Request).Message);
							break;
						default:
							Debug.Print("Else: " + Request.GetType().ToString());
							break;
					}
					return;

					break;
			}
		}
	}

	private void Worker_RunWorkerCompleted(System.Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
	{
		this.Invoke((MethodInvoker)WeGOTit);
	}
	public Welcome_ipsw()
	{
		Click += Clicked;
		Load += Welcome_Load;
	}
}
