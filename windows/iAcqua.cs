using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using MobileDevice;
using System.Management;
public class iAcqua
{
	public string consoletxt = "";
	public bool QuitMOFO = false;
	public bool iLeft = false;
	public bool DoIgiveAshit = false;
	private iDevice withEventsField_iPhoneInterface;
	private iDevice iPhoneInterface {
		get { return withEventsField_iPhoneInterface; }
		set {
			if (withEventsField_iPhoneInterface != null) {
				withEventsField_iPhoneInterface.DfuDisconnect -= DFUleft;
				withEventsField_iPhoneInterface.RecoveryModeLeave -= iBootleft;
				withEventsField_iPhoneInterface.Disconnect -= oniPhoneDisconnect;
				withEventsField_iPhoneInterface.Connect -= oniPhoneConnected;
			}
			withEventsField_iPhoneInterface = value;
			if (withEventsField_iPhoneInterface != null) {
				withEventsField_iPhoneInterface.DfuDisconnect += DFUleft;
				withEventsField_iPhoneInterface.RecoveryModeLeave += iBootleft;
				withEventsField_iPhoneInterface.Disconnect += oniPhoneDisconnect;
				withEventsField_iPhoneInterface.Connect += oniPhoneConnected;
			}
		}
	}
	private event iPhoneConnectedEventHandler iPhoneConnected;
	private delegate void iPhoneConnectedEventHandler();
	private event iPhoneDisconnectedEventHandler iPhoneDisconnected;
	private delegate void iPhoneDisconnectedEventHandler();
	private void iAcqua_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(35, 0);
		iLeft = false;
		Center_Label(Label3);
		DoIgiveAshit = true;
		try {
			iPhoneInterface = new MobileDevice.iDevice();
		} catch (Exception ex) {
			Interaction.MsgBox("iFaith was unable to hook iTunes!" + Strings.Chr(13) + Strings.Chr(13) + "Automatic ECID detection is now only available via Recovery/DFU!", MsgBoxStyle.Critical);
		}
		dfuibootsearcher.RunWorkerAsync();
	}
	private void DFUleft(object sender, DeviceNotificationEventArgs args)
	{
		if (iLeft == true) {
			return;
		}
		try {
			//DFU diconnected.
			MDIMain.Activate();
			this.Invoke((MethodInvoker)Cleanup);
		} catch (Exception ex) {
		}
	}
	private void iBootleft(object sender, DeviceNotificationEventArgs args)
	{
		if (iLeft == true) {
			return;
		}
		try {
			//iBoot disconnected.
			MDIMain.Activate();
			this.Invoke((MethodInvoker)Cleanup);
		} catch (Exception ex) {
		}
	}
	public void Cleanup()
	{
		try {
			Label2.Text = "Plug-in an iDevice or enter an ECID";
			Center_Label(Label2);
			iphone.Visible = true;
			Label3.Visible = true;
			TextBox1.Visible = true;
			spinny.Visible = false;
			manualecidBTN.Enabled = true;
			manualecidBTN.Visible = true;
			//
			Button1.Visible = false;
			dlallBTN.Visible = false;
			dlblobBTN.Visible = false;
			availableshsh.Items.Clear();
			availableshsh.Visible = false;
			DoIgiveAshit = true;
		//Reshow animation
		} catch (Exception ex) {
		}
	}
	private void oniPhoneDisconnect(object Sender, ConnectEventArgs args)
	{
		if (iLeft == true) {
			return;
		}
		try {
			DoIgiveAshit = true;
			MDIMain.Activate();
			this.Invoke((MethodInvoker)Cleanup);
		} catch (Exception ex) {
		}
	}
	private void oniPhoneConnected(object sender, ConnectEventArgs args)
	{
		if (iLeft == true) {
			return;
		}
		try {
			FileSystem.FreeFile();
			if (DoIgiveAshit == true) {
				DoIgiveAshit = false;
				ECID = Conversion.Hex(iPhoneInterface.CopyValue("UniqueChipID"));
				while (!(ECID.Length == 16)) {
					ECID = "0" + ECID;
				}
				MDIMain.Activate();
				this.Invoke((MethodInvoker)PluggedinGrabber);
			}
		} catch (Exception ex) {
		}
	}
	public void PluggedinGrabber()
	{
		try {
			TextBox1.Visible = false;
			manualecidBTN.Visible = false;
			this.Invoke((MethodInvoker)iAcqua);
		} catch (Exception ex) {
		}
	}
	public void Center_Label(object Label)
	{
		Label.Left = (this.Width / 2) - (Label.Width / 2);
	}
	public void iAcqua()
	{
		try {
			Label2.Text = "Communicating with Server...";
			spinny.Visible = true;
			iphone.Visible = false;
			Label3.Visible = false;
			Center_Label(spinny);
			Center_Label(Label2);
			Center_Label(availableshsh);
			Center_Label(dlallBTN);
			Center_Label(dlblobBTN);
			if (DoIgiveAshit == true) {
				return;
			}
			Delay(1);
			File_Delete(temppath + "\\available.xml");
			if (DoIgiveAshit == true) {
				return;
			}
			try {
				string Check = "http://iacqua.ih8sn0w.com/req.php?ecid=" + ECID;
				Uri CheckURI = new Uri(Check);
				dynamic clientCheck = new System.Net.WebClient();
				clientCheck.Headers.Add("user-agent", "iacqua/1.0-452");
				clientCheck.DownloadFileAsync(CheckURI, temppath + "\\available.xml");
				if (DoIgiveAshit == true) {
					return;
				}
				while (!(clientCheck.IsBusy == false)) {
					Delay(0.5);
				}
			} catch (Exception ex) {
				Interaction.MsgBox("Error 3 : We have failed trying to connect to iFaith's SHSH Cache server!", MsgBoxStyle.Critical);
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
				this.Dispose();
				return;
			}
			try {
				while (!(File_Exists(temppath + "\\available.xml") == true)) {
					if (DoIgiveAshit == true) {
						return;
					}
					Delay(0.1);
				}
				Delay(2);
				thatbox.LoadFile(temppath + "\\available.xml", RichTextBoxStreamType.PlainText);
				spinny.Visible = false;
				if (DoIgiveAshit == true) {
					return;
				}
				availableshsh.Items.AddRange(thatbox.Lines);
				if (availableshsh.Items.Count >= 1) {
					availableshsh.Items.Remove(availableshsh.Items.Item(availableshsh.Items.Count - 1));
				}
				if (DoIgiveAshit == true) {
					return;
				}
				Label2.Text = "Available Blobs:";
				Center_Label(Label2);
				Button1.Visible = true;
				//Show drop down menu.
				if (availableshsh.Items.Count == 0) {
					availableshsh.Items.Add("None");
					dlallBTN.Enabled = false;
					dlblobBTN.Enabled = false;
				} else {
					dlallBTN.Enabled = true;
					dlblobBTN.Enabled = true;
				}
				availableshsh.Visible = true;
				availableshsh.SelectedIndex = 0;
				dlallBTN.Visible = true;
				dlblobBTN.Visible = true;
			} catch (Exception ex) {
			}
		} catch (Exception ex) {
		}
	}
	private void goback_Click(System.Object sender, System.EventArgs e)
	{
		iLeft = true;
		QuitMOFO = true;
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
	}
	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
	try {
	bool amIhex = false;
	amIhex = false;
	DoIgiveAshit = false;
	if (TextBox1.Text.Contains("A")) {
		amIhex = true;
	}
	if (TextBox1.Text.Contains("B")) {
		amIhex = true;
	}
	if (TextBox1.Text.Contains("C")) {
		amIhex = true;
	}
	if (TextBox1.Text.Contains("D")) {
		amIhex = true;
	}
	if (TextBox1.Text.Contains("E")) {
		amIhex = true;
	}
	if (TextBox1.Text.Contains("F")) {
		amIhex = true;
	}
	if (amIhex == true & !(TextBox1.Text.Length == 16)) {
		while (!(TextBox1.Text.Length == 16)) {
			TextBox1.Text = "0" + TextBox1.Text;
		}
	}
	if (amIhex == false) {
		TextBox1.Text = Conversion.Hex(TextBox1.Text);
		while (!(TextBox1.Text.Length == 16)) {
			TextBox1.Text = "0" + TextBox1.Text;
		}
	}
	ECID = TextBox1.Text;
	manualecidBTN.Enabled = false;
	Label2.Text = "Waiting on Server...";
	Label2.Left = (this.Width / 2) - (Label2.Width / 2);
	TextBox1.Visible = false;
	TextBox1.Text = "";
	manualecidBTN.Visible = false;
	spinny.Visible = true;
	PluggedinGrabber();
} catch (Exception ex) {
	//Interaction.MsgBox(ex.ToString());
}

	}

	private void dlallBTN_Click(System.Object sender, System.EventArgs e)
	{
		if (availableshsh.Items.Item(0) == "None") {
			return;
		}
		FolderBrowserDialog1.ShowDialog();
		if (string.IsNullOrEmpty(FolderBrowserDialog1.SelectedPath)) {
			return;
		} else {
			Label2.Text = "Downloading Blob(s)...";
			Center_Label(Label2);
			spinny.Visible = true;
			availableshsh.Visible = false;
			dlblobBTN.Visible = false;
			dlallBTN.Visible = false;
			Delay(2);
			//Download ALL
			foreach (object blob_loopVariable in availableshsh.Items) {
				blob = blob_loopVariable;
				string Check = "http://iacqua.ih8sn0w.com/req.php?ecid=" + ECID + "&ios=" + blob.ToString().Replace(" ", "%20");
				Uri CheckURI = new Uri(Check);
				dynamic clientCheck = new System.Net.WebClient();
				//Headers.Add("user-agent", "iacqua/1.0-452")
				clientCheck.Headers.Add("user-agent", "iacqua/1.0-452");
				clientCheck.DownloadFileAsync(CheckURI, FolderBrowserDialog1.SelectedPath + "\\" + ECID + "_" + blob.ToString() + "_cache.ifaith");
				while (!(clientCheck.IsBusy == false)) {
					Delay(0.5);
				}
			}
			spinny.Visible = false;
			Label2.Text = "Available Blobs:";
			Center_Label(Label2);
			availableshsh.Visible = true;
			dlallBTN.Visible = true;
			dlblobBTN.Visible = true;
			Interaction.MsgBox("SHSH Blob(s) downloaded!", MsgBoxStyle.Information);
		}

	}
	public void DownloadingNAO()
	{
		availableshsh.Visible = false;
		dlallBTN.Visible = false;
		dlblobBTN.Visible = false;
	}
	private void dlblobBTN_Click(System.Object sender, System.EventArgs e)
	{
		if (availableshsh.Items.Item(0) == "None") {
			return;
		}
		FolderBrowserDialog1.ShowDialog();
		if (string.IsNullOrEmpty(FolderBrowserDialog1.SelectedPath)) {
			return;
		} else {
			Label2.Text = "Downloading Blob(s)...";
			Center_Label(Label2);
			spinny.Visible = true;
			availableshsh.Visible = false;
			dlblobBTN.Visible = false;
			dlallBTN.Visible = false;
			Delay(2);
			//Download
			string Check = "http://iacqua.ih8sn0w.com/req.php?ecid=" + ECID + "&ios=" + availableshsh.SelectedItem.ToString.Replace(" ", "%20");
			Uri CheckURI = new Uri(Check);
			dynamic clientCheck = new System.Net.WebClient();
			//Headers.Add("user-agent", "iacqua/1.0-452")
			clientCheck.Headers.Add("user-agent", "iacqua/1.0-452");
			clientCheck.DownloadFileAsync(CheckURI, FolderBrowserDialog1.SelectedPath + "\\" + ECID + "_" + availableshsh.SelectedItem.ToString + "_cache.ifaith");
			while (!(clientCheck.IsBusy == false)) {
				Delay(0.5);
			}
			spinny.Visible = false;
			Label2.Text = "Available Blobs:";
			Center_Label(Label2);
			availableshsh.Visible = true;
			dlallBTN.Visible = true;
			dlblobBTN.Visible = true;
			Interaction.MsgBox("SHSH Blob(s) downloaded!", MsgBoxStyle.Information);
		}
	}
	private void DFUiBootECIDboom()
	{
		try {
			DoIgiveAshit = false;
			int icountMatch = 0;
			icountMatch = HighlightWords(Console, "ECID:", System.Drawing.Color.Red);
			PluggedinGrabber();
		} catch (Exception ex) {
		}
	}
	private void ClearConsole()
	{
		if (iLeft == true) {
			return;
		}
		Console.Text = "";
	}
	private void WriteConsole()
	{
		if (iLeft == true) {
			return;
		}
		Console.Text = consoletxt;
	}
	private void dfuibootsearcher_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		try {
			while (!(QuitMOFO == true)) {
				if (iLeft == true) {
					return;
				}
				Delay(0.5);
				if (DoIgiveAshit == true) {
					if (iLeft == true) {
						return;
					}
					this.Invoke((MethodInvoker)ClearConsole);
					//Searching for DFU/iBoot...
					ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (DFU) USB Driver'");
					foreach (ManagementObject queryObj in searcher.Get()) {
						//Jump to DFU
						consoletxt = (queryObj("DeviceID"));
						this.Invoke((MethodInvoker)WriteConsole);
						this.Invoke((MethodInvoker)DFUiBootECIDboom);
					}
					ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (iBoot) USB Driver'");
					foreach (ManagementObject queryObj2 in searcher2.Get()) {
						//Jump to iBoot
						consoletxt = (queryObj2("DeviceID"));
						this.Invoke((MethodInvoker)WriteConsole);
						this.Invoke((MethodInvoker)DFUiBootECIDboom);
					}
				}
			}
			QuitMOFO = false;
			return;
		} catch (Exception ex) {
		}
	}
	public int HighlightWords(RichTextBox rtb, string sFindString, System.Drawing.Color lColor)
	{

		int iFoundPos = 0;
		//Position of first character of match
		int iFindLength = 0;
		//Length of string to find
		int iOriginalSelStart = 0;
		int iOriginalSelLength = 0;
		int iMatchCount = 0;
		//Number of matches


		//Save the insertion points current location and length
		iOriginalSelStart = rtb.SelectionStart;
		iOriginalSelLength = rtb.SelectionLength;

		//Cache the length of the string to find
		iFindLength = Strings.Len(sFindString) + 16;

		//Attempt to find the first match
		iFoundPos = rtb.Find(sFindString, 0, RichTextBoxFinds.NoHighlight);
		while (iFoundPos > 0) {
			iMatchCount = iMatchCount + 1;

			Console.SelectionStart = iFoundPos;
			//The SelLength property is set to 0 as soon as you change SelStart
			Console.SelectionLength = iFindLength;
			//rtb.SelectionBackColor = lColor

			Console.Select(iFoundPos + 5, iFindLength - 5);
			ECID = Console.SelectedText;
			//Attempt to find the next match
			iFoundPos = rtb.Find(sFindString, iFoundPos + iFindLength, RichTextBoxFinds.NoHighlight);
		}

		//Restore the insertion point to its original location and length
		rtb.SelectionStart = iOriginalSelStart;
		rtb.SelectionLength = iOriginalSelLength;

		//Return the number of matches
		return iMatchCount;
	}

	private void PowerRangerStyle(System.Object sender, System.EventArgs e)
	{
		try {
			if (iLeft == true) {
				return;
			}
			DoIgiveAshit = true;
			Button1.Visible = false;
			DoIgiveAshit = true;
			MDIMain.Activate();
			this.Invoke((MethodInvoker)Cleanup);
		} catch (Exception ex) {
		}
	}
	public iAcqua()
	{
		Load += iAcqua_Load;
	}
}
