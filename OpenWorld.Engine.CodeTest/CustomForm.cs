using OpenWorld.Engine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Engine.CodeTest
{
	class CustomForm : Form
	{
		Button okButton;
		Button cancelButton;
		Button spawnWindowButton;

		public CustomForm()
		{
			this.InitializeComponents();
		}

		private void InitializeComponents()
		{
			this.Text = "Custom Form";

			this.okButton = new Button();
			this.okButton.Text = "OK";
			this.okButton.Left = new Scalar(1.0f, -this.okButton.Width.Absolute - 30);
			this.okButton.Top = new Scalar(1.0f, -this.okButton.Height.Absolute - 10);
			this.okButton.Click += okButton_Click;
			this.okButton.Parent = this;

			this.cancelButton = new Button();
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Left = new Scalar(0.0f, 10);
			this.cancelButton.Top = new Scalar(1.0f, -this.okButton.Height.Absolute - 10);
			this.cancelButton.Click += cancelButton_Click;
			this.cancelButton.Parent = this;

			this.spawnWindowButton = new Button();
			this.spawnWindowButton.Text = "Spawn Window";
			this.spawnWindowButton.Left = new Scalar(0.0f, 10);
			this.spawnWindowButton.Top = new Scalar(0.0f, 10);
			this.spawnWindowButton.Width = new Scalar(0.0f, 120.0f);
			this.spawnWindowButton.Click += spawnWindowButton_Click;
			this.spawnWindowButton.Parent = this;

			Label infoLabel = new Label();
			infoLabel.ForeColor = Color.Red;
			infoLabel.Font = new Font("andyb.ttf", 25);
			infoLabel.Text = "Hello Custom-Font";
			infoLabel.Left = new Scalar(0.0f, 10.0f);
			infoLabel.Top = new Scalar(0.0f, 40.0f);
			infoLabel.Width = new Scalar(0.0f, 200.0f);
			infoLabel.Height = new Scalar(0.0f, 25.0f);
			infoLabel.Parent = this;


			VScrollBar scrollBar = new VScrollBar();
			scrollBar.Left = new Scalar(1.0f, -20.0f);
			scrollBar.Parent = this;
		}

		void okButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		void spawnWindowButton_Click(object sender, EventArgs e)
		{
			CustomForm form2 = new CustomForm();
			form2.Left = new Scalar(0.0f, 20.0f);
			form2.Top = new Scalar(0.0f, 20.0f);
			form2.Parent = this.Parent;
		}
	}
}
