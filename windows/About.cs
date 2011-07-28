using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
public class About
{

	private void Button1_Click(System.Object sender, System.EventArgs e)
	{
		Folder_Delete(temppath);
		Create_Directory(temppath);
		Button1.Visible = false;
		Label5.Visible = false;
		whatudo.Visible = true;
		//Just making sure the user doesn't double click.
		PictureBox3.Visible = true;
		whatudo.Text = "Please Wait...";
		whatudo.Left = (this.Width / 2) - (whatudo.Width / 2);
		Delay(1);
		PictureBox5.Visible = true;
		PictureBox4.Visible = true;
		PictureBox3.Visible = false;
		PictureBox2.Visible = true;
		PictureBox1.Visible = true;
		whatudo.Text = "What would you like to do?";
		whatudo.Left = (this.Width / 2) - (whatudo.Width / 2);
		PictureBox1.Enabled = true;
		PictureBox2.Enabled = true;
		PictureBox4.Enabled = true;
	}
	public void Meow4()
	{
		//Load up Server shit...
		iAcqua.MdiParent = MDIMain;
		iAcqua.Show();
		Welcome.Dispose();
		Loaded = true;
		this.Dispose();
	}
	public void Meow3()
	{
		dynamic Answer = null;
		Answer = Interaction.MsgBox("Run iREB for an Apple TV 2?", MsgBoxStyle.YesNoCancel, "iFaith");
		iREB_mode = false;
		if (Answer == Constants.vbYes) {
			iDevice = "Apple TV 2";
			atv2mode = true;
		} else if (Answer == Constants.vbNo) {
			atv2mode = false;
		} else if (Answer == Constants.vbCancel) {
			return;
		}
		iREB_mode = true;
		MDIMain.dfuinstructions.Visible = true;
		MDIMain.dfuinstructionstxt.Visible = true;
		MDIMain.Button1.Visible = true;
		MDIMain.dfuinstructionstxt.Text = "DFU Mode Pwner";
		MDIMain.blue.Visible = true;
		dfu.MdiParent = MDIMain;
		dfu.Show();
		Welcome.Dispose();
		Loaded = true;
		this.Dispose();
	}
	public void Meow2()
	{
		//Good bye Welcome.vb Hello IPSW.vb...
		Welcome_ipsw.MdiParent = MDIMain;
		Welcome_ipsw.Show();
		Welcome.Dispose();
		Loaded = true;
		this.Dispose();
	}
	public void Meow()
	{
		Welcome.goback.Visible = true;
		Welcome.Button1.Visible = true;
		Welcome.Button1.Enabled = true;
		Welcome.goback.Enabled = true;
		Loaded = true;
		this.Dispose();
	}
	private void About_Load(System.Object sender, System.EventArgs e)
	{
		this.Location = new Point(45, 82);
		Welcome.Button1.Visible = false;
		Welcome.goback.Visible = false;
		Loaded = false;
		MDIMain.Activate();
	}

	private void PictureBox1_MouseEnter(System.Object sender, System.EventArgs e)
	{
		PictureBox1.Image = My.Resources.Dump_Pressed;
	}

	private void PictureBox1_MouseLeave(System.Object sender, System.EventArgs e)
	{
		PictureBox1.Image = My.Resources.Dump;
	}

	private void PictureBox1_MouseHover(System.Object sender, System.EventArgs e)
	{
		PictureBox1.Image = My.Resources.Dump_Pressed;
	}

	private void PictureBox2_MouseEnter(System.Object sender, System.EventArgs e)
	{
		PictureBox2.Image = My.Resources.Build_Pressed;
	}

	private void PictureBox2_MouseLeave(System.Object sender, System.EventArgs e)
	{
		PictureBox2.Image = My.Resources.Build;
	}

	private void PictureBox2_MouseHover(System.Object sender, System.EventArgs e)
	{
		PictureBox2.Image = My.Resources.Build_Pressed;
	}
	private void PictureBox4_MouseEnter(System.Object sender, System.EventArgs e)
	{
		PictureBox4.Image = My.Resources.DFUPwner_Pressed;
	}

	private void PictureBox4_MouseLeave(System.Object sender, System.EventArgs e)
	{
		PictureBox4.Image = My.Resources.DFUPwner;
	}

	private void PictureBox4_MouseHover(System.Object sender, System.EventArgs e)
	{
		PictureBox4.Image = My.Resources.DFUPwner_Pressed;
	}
	private void PictureBox5_MouseEnter(System.Object sender, System.EventArgs e)
	{
		PictureBox5.Image = My.Resources.SHSHCache_Pressed;
	}

	private void PictureBox5_MouseLeave(System.Object sender, System.EventArgs e)
	{
		PictureBox5.Image = My.Resources.SHSHCache;
	}

	private void PictureBox5_MouseHover(System.Object sender, System.EventArgs e)
	{
		PictureBox5.Image = My.Resources.SHSHCache_Pressed;
	}
	private void PictureBox1_Click(System.Object sender, System.EventArgs e)
	{
		Meow();
	}

	private void PictureBox2_Click(System.Object sender, System.EventArgs e)
	{
		Meow2();
	}

	private void PictureBox4_Click(System.Object sender, System.EventArgs e)
	{
		Meow3();
	}

	private void PictureBox5_Click(System.Object sender, System.EventArgs e)
	{
		Meow4();
	}
	public About()
	{
		Load += About_Load;
	}

}
