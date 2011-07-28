using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public class Welcome
{
	public int Stage = 0;
	private void Welcome_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(35, 0);
	}
	public void LoadGUI()
	{
		//
		MDIMain.dfuinstructionstxt.Text = "DFU Instructions";
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
		Label2.Visible = false;
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
		if (Stage == 0) {
			Stage = 1;
			Button1.Text = "Let's Go!";
			//Show Credits
			Show_Credits();
		} else if (Stage == 1) {
			dynamic Answer = null;
			Answer = Interaction.MsgBox("Are you dumping an Apple TV 2?", MsgBoxStyle.YesNo, "iFaith");
			iREB_mode = false;
			if (Answer == Constants.vbYes) {
				iDevice = "Apple TV 2";
				atv2mode = true;
			} else {
				iDevice = "";
				atv2mode = false;
			}
			LoadGUI();
			dfu.MdiParent = MDIMain;
			dfu.Show();
			this.Dispose();
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
	private void surenix_Click(System.Object sender, System.EventArgs e)
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

	private void goback_Click(System.Object sender, System.EventArgs e)
	{
		// Display a child form.
		Form frm = new Form();
		frm.MdiParent = MDIMain;
		frm.Width = this.Width / 2;
		frm.Height = this.Height / 2;
		frm.Show();
		frm.Hide();

		this.Controls.Clear();
		InitializeComponent();
		Show();

		this.Button1.Enabled = false;
		About.MdiParent = MDIMain;
		About.Show();
		About.BringToFront();
	}

	private void Welcome_MouseEnter(System.Object sender, System.EventArgs e)
	{
		if (Loaded == false) {
			About.BringToFront();
		}
	}

	private void Welcome_MouseHover(System.Object sender, System.EventArgs e)
	{
		if (Loaded == false) {
			About.BringToFront();
		}
	}

	private void PictureBox1_Click(System.Object sender, System.Windows.Forms.MouseEventArgs e)
	{
		if (My.Computer.Keyboard.ShiftKeyDown) {
			dynamic DebugQuestion = null;
			DebugQuestion = Interaction.MsgBox("Would you like to debug?", MsgBoxStyle.YesNo, "Enable iFaith Debug Mode?");
			if (DebugQuestion == Constants.vbYes) {
				Debug_Mode = true;
				Interaction.MsgBox("iFaith Debug Mode Enabled!!!", MsgBoxStyle.Information);
			} else {
				Debug_Mode = false;
				Interaction.MsgBox("iFaith Debug mode will NOT be used.", MsgBoxStyle.Information);
			}
		}
	}
	public Welcome()
	{
		MouseHover += Welcome_MouseHover;
		MouseEnter += Welcome_MouseEnter;
		Click += Clicked;
		Load += Welcome_Load;
	}
}
