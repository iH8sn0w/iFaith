using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Management;
public class dfu
{
	public bool QuitDFU = false;
	private string Results;
	private void DFU_Load(System.Object sender, System.EventArgs e)
	{
		MDIMain.dfuinstructions.Enabled = true;
		MDIMain.dfuinstructions.Checked = true;
		MDIMain.dfuinstructionstxt.ForeColor = Color.White;
		this.Location = new Point(160, 0);
		if (iDevice == "Apple TV 2") {
			animation.Visible = false;
			atv2animation.Visible = true;
			atv2warn.Visible = true;
			Label1.Text = "Plug-in your Apple TV 2 via USB.";
			Center_PowerOFFtxt();
		//Load ATV2 Instructions
		} else {
			animation.Visible = true;
			Label1.Text = "Please power off your iDevice," + Strings.Chr(13) + "then press " + Quotation + "Start" + Quotation + ".";
			Center_PowerOFFtxt();
			//Load Other Instructions
		}
	}
	public void LoadDFU_1()
	{
		Label1.Visible = true;
		Label1.Text = "Please turn off your iDevice.";
		animation.Visible = true;
		Button1.Visible = true;
	}
	public void Search_DFU(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		DFUConnected = false;
		while (!(DFUConnected == true)) {
			if (OhNoesShutDOWN == true) {
				return;
			}
			//Searching for DFU...
			Console.Text = " ";
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description = 'Apple Recovery (DFU) USB Driver'");
			foreach (ManagementObject queryObj in searcher.Get()) {
				if (OhNoesShutDOWN == true) {
					return;
				}
				Console.Text += (queryObj("Description"));
			}
			if (Console.Text.Contains("DFU")) {
				if (OhNoesShutDOWN == true) {
					return;
				}
				//MsgBox("In DFU!", MsgBoxStyle.Information)
				DFUConnected = true;
			}
		}
	}
	public void CleaniREB()
	{
		MDIMain.dfuinstructions.Visible = false;
		MDIMain.dfuinstructionstxt.Visible = false;
		MDIMain.blue.Visible = false;
		MDIMain.Button1.Visible = false;
		BackgroundWorker1.Dispose();
		BackgroundWorker2.Dispose();
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
		this.Dispose();
	}
	public void GoGoGadgetiFaith()
	{
		QuitDFU = true;
		DFUConnected = false;
		ResetDFUInstructions = true;
		iDetector();
		//Shutdown iTunes...
		//Shell("cmd /c taskkill /f /t /im iTunes.exe", AppWinStyle.Hide)
		//Shell("cmd /c taskkill /f /t /im iTunesHelper.exe", AppWinStyle.Hide)
		//Go...
		MDIMain.TopMost = true;
		MDIMain.Activate();
		if (iREB_mode == true) {
			MDIMain.TopMost = true;
			MDIMain.Activate();
			PictureBox2.Visible = true;
			ProgressBar1.Visible = true;
			PictureBox2.BringToFront();
			ProgressBar1.BringToFront();
			BackgroundWorker2.RunWorkerAsync();
			SaveToDisk("s-irecovery.exe", temppath + "\\s-irecovery.exe");
			iRecovery_exploit();
			MDIMain.TopMost = false;
			Interaction.MsgBox("Your device is now in a PWNED DFU state (black screen)." + Strings.Chr(13) + Strings.Chr(13) + "You may now launch iTunes and do SHIFT + Restore" + Strings.Chr(13) + "to the custom *signed* IPSW located on your desktop!", MsgBoxStyle.Information);
			CleaniREB();
		} else {
			Run.MdiParent = MDIMain;
			Run.Show();
			this.Dispose();
		}
	}
	public void Center_PowerOFFtxt()
	{
		Label1.Left = (this.Width / 2) - (Label1.Width / 2);
	}
	public void Center_DFUInstructions()
	{
		dfuinstructions.Left = (this.Width / 2) - (dfuinstructions.Width / 2);
	}
	public void Center_Prepare()
	{
		Prepare.Left = (this.Width / 2) - (Prepare.Width / 2);
	}
	public void DFUInstructions_Normal()
	{
		try {
			if (QuitDFU == true) {
				ResetDFUInstructions = false;
				QuitDFU = false;
				return;
			}
			BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			BackgroundWorker1.WorkerSupportsCancellation = true;
			BackgroundWorker1.RunWorkerAsync();
			PictureBox1.Visible = true;
			PictureBox1.Image = My.Resources.justdevice;
			Prepare.Visible = true;
			Prepare.Text = "Prepare to press && hold Power + Home...";
			Center_Prepare();
			DateAndTime.Timer.Visible = true;
			DateAndTime.Timer.ForeColor = Color.Red;
			DateAndTime.Timer.Text = "5";
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			Delay(1);
			DateAndTime.Timer.Text = "4";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "3";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "2";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "1";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.ForeColor = Color.White;
			Prepare.Visible = false;
			dfuinstructions.Text = "Press && hold Power + Home!";
			dfuinstructions.Visible = true;
			Center_DFUInstructions();
			PictureBox1.Image = My.Resources.powerNhome;
			DateAndTime.Timer.Text = "10";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "9";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "8";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "7";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "6";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			Prepare.Visible = true;
			Prepare.Text = "Prepare to release the Power button (5)";
			Center_Prepare();
			DateAndTime.Timer.ForeColor = Color.Red;
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			DateAndTime.Timer.Text = "5";
			Delay(1);
			Prepare.Text = "Prepare to release the Power button (4)";
			DateAndTime.Timer.Text = "4";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			Prepare.Text = "Prepare to release the Power button (3)";
			DateAndTime.Timer.Text = "3";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			Prepare.Text = "Prepare to release the Power button (2)";
			DateAndTime.Timer.Text = "2";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			Prepare.Text = "Prepare to release the Power button (1)";
			DateAndTime.Timer.Text = "1";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			Prepare.Visible = false;
			PictureBox1.Image = My.Resources.homebutton;
			dfuinstructions.Text = "Release the power button, keep holding home!";
			Center_DFUInstructions();
			DateAndTime.Timer.ForeColor = Color.White;
			DateAndTime.Timer.Text = "30";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "29";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "28";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "27";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "26";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "25";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "24";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "23";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "22";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "21";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "20";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "19";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "18";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "17";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "16";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "15";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "14";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "13";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "12";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "11";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "10";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "9";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "8";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "7";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "6";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "5";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "4";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "3";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "2";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			DateAndTime.Timer.Text = "1";
			if (DFUConnected == true) {
				GoGoGadgetiFaith();
			}
			if (ResetDFUInstructions == true) {
				ResetDFUInstructions = false;
				DFUInstructions_Normal();
				return;
			}
			Delay(1);
			GoGoGadgetCleanUp();
			dfuinstructions.Visible = false;
			MDIMain.TopMost = false;
			Interaction.MsgBox("You failed to Enter DFU. Please Try again.", MsgBoxStyle.Critical);
		} catch (Exception ex) {
		}
	}
	public void GoGoGadgetCleanUp()
	{
		if (iDevice == "Apple TV 2") {
			atv2animation.Visible = true;
			atv2warn.Visible = true;
			atv2.Visible = false;
		} else {
			PictureBox1.Visible = false;
			animation.Visible = true;
		}
		//Shared components
		Label1.Visible = true;
		Button1.Visible = true;
		resetdfubtn.Visible = false;
		DateAndTime.Timer.Visible = false;
		BackgroundWorker1.Dispose();
	}
	public void update_1(object sender, DataReceivedEventArgs e)
	{
		UpdateTextBox(e.Data);
	}
	private void UpdateText()
	{
		idetect.Text = Results;
	}
	private delegate void delUpdate();
	private delUpdate Finished = new delUpdate(UpdateText);
	private delegate void UpdateTextBoxDelegate(string Text);
	private void UpdateTextBox(string Tex)
	{
		if (this.InvokeRequired) {
			UpdateTextBoxDelegate del = new UpdateTextBoxDelegate(UpdateTextBox);
			object[] args = { Tex };
			this.Invoke(del, args);
		} else {
			//tb.Text &= Tex & Environment.NewLine
			idetect.Text += Tex;
			idetect.Text = idetect.Text.Replace(" ", "");
		}
	}
	public void iDetector()
	{
		SaveToDisk("s-irecovery.exe", "s-irecovery.exe");
		//Were going to detect the iDevice now! :)
		try {
			idetect.Text = string.Empty;
			Process p = new Process();
			p.StartInfo.FileName = "s-irecovery.exe";
			p.StartInfo.Arguments = "-detect";
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = true;
			p.StartInfo.RedirectStandardError = true;
			p.OutputDataReceived += update_1;
			p.Start();
			System.IO.StreamWriter SW = p.StandardInput;
			p.BeginOutputReadLine();
			p.Dispose();
			Delay(2);
			if (atv2mode == true & idetect.Text.Contains("k66ap") == true) {
			//No Problem :)
			} else if (atv2mode == false & idetect.Text.Contains("k66ap") == true) {
				Interaction.MsgBox("Apple TV 2 was detected! " + Strings.Chr(13) + Strings.Chr(13) + " Next time, Select Apple TV 2! ABORTING...", MsgBoxStyle.Critical);
				Application.Exit();
			} else if (atv2mode == false) {
			// Do Nothing.
			} else {
				Interaction.MsgBox("A DEVICE OTHER THAN THE Apple TV 2 WAS DETECTED!" + Strings.Chr(13) + Strings.Chr(13) + "ABORTING...", MsgBoxStyle.Critical);
				Application.Exit();
			}
			if (idetect.Text.Contains("n88ap") == true) {
				iDevice = "iPhone 3GS";
				board = "n88ap";
			} else if (idetect.Text.Contains("n72ap") == true) {
				iDevice = "iPod Touch 2G";
				board = "n72ap";
			} else if (idetect.Text.Contains("n18ap") == true) {
				iDevice = "iPod Touch 3G";
				board = "n18ap";
			} else if (idetect.Text.Contains("n81ap") == true) {
				iDevice = "iPod Touch 4";
				board = "n81ap";
			} else if (idetect.Text.Contains("n90ap") == true) {
				iDevice = "iPhone 4";
				board = "n90ap";
			} else if (idetect.Text.Contains("n92ap") == true) {
				iDevice = "iPhone 4";
				board = "n92ap";
			} else if (idetect.Text.Contains("k48ap") == true) {
				iDevice = "iPad 1";
				board = "k48ap";
			} else if (idetect.Text.Contains("k66ap") == true) {
				iDevice = "Apple TV 2";
				board = "k66ap";
			} else {
				Interaction.MsgBox("UNSUPPORTED DEVICE!" + Strings.Chr(13) + Strings.Chr(13) + "ABORTING...", MsgBoxStyle.Critical);
				Application.Exit();
			}
		} catch (ManagementException err) {
		}
	}
	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		Label1.Visible = false;
		Button1.Visible = false;
		OhNoesShutDOWN = false;
		resetdfubtn.Visible = true;
		if (iDevice == "Apple TV 2") {
			//Load ATV2 DFU
			atv2animation.Visible = false;
			PictureBox1.Visible = false;
			atv2.Visible = true;
			DFUInstructions_ATV2();
		} else {
			animation.Visible = false;
			DFUInstructions_Normal();
		}
	}
	public void DFUInstructions_ATV2()
	{
		BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		BackgroundWorker1.WorkerSupportsCancellation = true;
		BackgroundWorker1.RunWorkerAsync();
		if (QuitDFU == true) {
			ResetDFUInstructions = false;
			QuitDFU = false;
			return;
		}
		atv2warn.Visible = false;
		atv2.Image = My.Resources.remote_self;
		Prepare.Visible = true;
		Prepare.Text = "Prepare to press && hold Menu + Play...";
		Center_Prepare();
		DateAndTime.Timer.Visible = true;
		DateAndTime.Timer.ForeColor = Color.Red;
		DateAndTime.Timer.Text = "5";
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		Delay(1);
		DateAndTime.Timer.Text = "4";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "3";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "2";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "1";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.ForeColor = Color.Red;
		Prepare.Text = "Prepare to release in (5).";
		Center_Prepare();
		dfuinstructions.Text = "Point to your Apple TV 2 && hold Menu + Play!!";
		dfuinstructions.Visible = true;
		Center_DFUInstructions();
		atv2.Image = My.Resources.remote_both;
		DateAndTime.Timer.Text = "5";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		Prepare.Text = "Prepare to release in (4).";
		DateAndTime.Timer.Text = "4";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		Prepare.Text = "Prepare to release in (3).";
		DateAndTime.Timer.Text = "3";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		Prepare.Text = "Prepare to release in (2).";
		DateAndTime.Timer.Text = "2";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		Prepare.Text = "Prepare to release in (1).";
		DateAndTime.Timer.Text = "1";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		atv2.Image = My.Resources.remote_self;
		Prepare.Visible = false;
		dfuinstructions.Text = "Release Menu + Play! (Waiting for DFU...)";
		Center_DFUInstructions();
		DateAndTime.Timer.ForeColor = Color.White;
		DateAndTime.Timer.Text = "10";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "9";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "8";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "7";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "6";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "5";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "4";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "3";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "2";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		Delay(1);
		DateAndTime.Timer.Text = "1";
		if (DFUConnected == true) {
			GoGoGadgetiFaith();
		}
		if (ResetDFUInstructions == true) {
			ResetDFUInstructions = false;
			DFUInstructions_ATV2();
			return;
		}
		dfuinstructions.Visible = false;
		MDIMain.TopMost = false;
		GoGoGadgetCleanUp();
		Interaction.MsgBox("You failed to Enter DFU. Please Try again.", MsgBoxStyle.Critical);
	}
	private void resetdfubtn_Click(System.Object sender, System.EventArgs e)
	{
		ResetDFUInstructions = true;
		dfuinstructions.Visible = false;
	}
	public void Increase()
	{
		if (ProgressBar1.Value == 100) {
			return;
		}
		ProgressBar1.Value = ProgressBar1.Value + 3;
	}
	public void one00Percent()
	{
		ProgressBar1.Value = 100;
	}

	public void BackgroundWorker2_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
	{
		try {
			while (!(ProgressBar1.Value == 99)) {
				if (OhNoesShutDOWN == true) {
					return;
				}
				ProgressBar1.Invoke((MethodInvoker)Increase);
				Sleep(45);
			}
			ProgressBar1.Invoke((MethodInvoker)one00Percent);
		} catch (Exception ex) {
		}
	}

	private void Button2_Click(System.Object sender, System.EventArgs e)
	{
		DFUConnected = true;
		GoGoGadgetiFaith();
	}
	public dfu()
	{
		Load += DFU_Load;
	}
}
