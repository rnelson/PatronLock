namespace PatronLock
{
	/// <summary>
	/// Design component of the window
	/// </summary>
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.itemPanel = new System.Windows.Forms.Panel();
			this.exitButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.statusTextLabel = new System.Windows.Forms.Label();
			this.nuidLabel = new System.Windows.Forms.Label();
			this.loginButton = new System.Windows.Forms.Button();
			this.nuidTextBox = new System.Windows.Forms.TextBox();
			this.passwordPanel = new System.Windows.Forms.Panel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.logoffImageList = new System.Windows.Forms.ImageList(this.components);
			this.logoffButton = new System.Windows.Forms.Button();
			this.itemPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.passwordPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// itemPanel
			// 
			this.itemPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.itemPanel.Controls.Add(this.exitButton);
			this.itemPanel.Controls.Add(this.pictureBox1);
			this.itemPanel.Controls.Add(this.statusTextLabel);
			this.itemPanel.Controls.Add(this.nuidLabel);
			this.itemPanel.Controls.Add(this.loginButton);
			this.itemPanel.Controls.Add(this.nuidTextBox);
			this.itemPanel.Location = new System.Drawing.Point(84, 129);
			this.itemPanel.Name = "itemPanel";
			this.itemPanel.Size = new System.Drawing.Size(436, 125);
			this.itemPanel.TabIndex = 7;
			// 
			// exitButton
			// 
			this.exitButton.Enabled = false;
			this.exitButton.Location = new System.Drawing.Point(352, 92);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 13;
			this.exitButton.Text = "E&xit";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Visible = false;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::PatronLock.Properties.Resources.wdn_logo;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(101, 95);
			this.pictureBox1.TabIndex = 12;
			this.pictureBox1.TabStop = false;
			// 
			// statusTextLabel
			// 
			this.statusTextLabel.Location = new System.Drawing.Point(113, 70);
			this.statusTextLabel.Name = "statusTextLabel";
			this.statusTextLabel.Size = new System.Drawing.Size(314, 21);
			this.statusTextLabel.TabIndex = 10;
			this.statusTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nuidLabel
			// 
			this.nuidLabel.AutoSize = true;
			this.nuidLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nuidLabel.Location = new System.Drawing.Point(108, 39);
			this.nuidLabel.Name = "nuidLabel";
			this.nuidLabel.Size = new System.Drawing.Size(68, 25);
			this.nuidLabel.TabIndex = 9;
			this.nuidLabel.Text = "NUID:";
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(352, 39);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(75, 23);
			this.loginButton.TabIndex = 8;
			this.loginButton.Text = "&Login";
			this.loginButton.UseVisualStyleBackColor = true;
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// nuidTextBox
			// 
			this.nuidTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nuidTextBox.Location = new System.Drawing.Point(182, 36);
			this.nuidTextBox.Name = "nuidTextBox";
			this.nuidTextBox.Size = new System.Drawing.Size(164, 31);
			this.nuidTextBox.TabIndex = 7;
			// 
			// passwordPanel
			// 
			this.passwordPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.passwordPanel.Controls.Add(this.cancelButton);
			this.passwordPanel.Controls.Add(this.okButton);
			this.passwordPanel.Controls.Add(this.passwordTextBox);
			this.passwordPanel.Controls.Add(this.passwordLabel);
			this.passwordPanel.Enabled = false;
			this.passwordPanel.Location = new System.Drawing.Point(84, 260);
			this.passwordPanel.Name = "passwordPanel";
			this.passwordPanel.Size = new System.Drawing.Size(436, 89);
			this.passwordPanel.TabIndex = 8;
			this.passwordPanel.Visible = false;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(271, 55);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(352, 55);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.passwordTextBox.Location = new System.Drawing.Point(134, 18);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(293, 31);
			this.passwordTextBox.TabIndex = 1;
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.passwordLabel.Location = new System.Drawing.Point(16, 21);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(112, 25);
			this.passwordLabel.TabIndex = 0;
			this.passwordLabel.Text = "Password:";
			// 
			// logoffImageList
			// 
			this.logoffImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("logoffImageList.ImageStream")));
			this.logoffImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.logoffImageList.Images.SetKeyName(0, "glyfx-undo_square_48.png");
			this.logoffImageList.Images.SetKeyName(1, "glyfx-undo_square_48_h.png");
			// 
			// logoffButton
			// 
			this.logoffButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.logoffButton.ImageIndex = 0;
			this.logoffButton.ImageList = this.logoffImageList;
			this.logoffButton.Location = new System.Drawing.Point(523, 353);
			this.logoffButton.Name = "logoffButton";
			this.logoffButton.Size = new System.Drawing.Size(48, 48);
			this.logoffButton.TabIndex = 300;
			this.logoffButton.UseVisualStyleBackColor = true;
			this.logoffButton.Click += new System.EventHandler(this.logoffButton_Click);
			this.logoffButton.MouseEnter += new System.EventHandler(this.logoffButton_MouseEnter);
			this.logoffButton.MouseLeave += new System.EventHandler(this.logoffButton_MouseLeave);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(583, 413);
			this.Controls.Add(this.logoffButton);
			this.Controls.Add(this.passwordPanel);
			this.Controls.Add(this.itemPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Patron Login";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordTextBox_KeyPress);
			this.itemPanel.ResumeLayout(false);
			this.itemPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.passwordPanel.ResumeLayout(false);
			this.passwordPanel.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel itemPanel;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label statusTextLabel;
		private System.Windows.Forms.Label nuidLabel;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.TextBox nuidTextBox;
		private System.Windows.Forms.Panel passwordPanel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.ImageList logoffImageList;
		private System.Windows.Forms.Button logoffButton;

	}
}

