namespace KinectCam
{
    partial class AboutForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.firstAuthorLabel = new System.Windows.Forms.Label();
            this.secondAuthorLabel = new System.Windows.Forms.Label();
            this.angleTrackBar = new System.Windows.Forms.TrackBar();
            this.angleGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.angleTrackBar)).BeginInit();
            this.angleGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel.Location = new System.Drawing.Point(12, 6);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(345, 38);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "KinectCam ver. 1.4";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstAuthorLabel
            // 
            this.firstAuthorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstAuthorLabel.AutoSize = true;
            this.firstAuthorLabel.Location = new System.Drawing.Point(19, 168);
            this.firstAuthorLabel.Name = "firstAuthorLabel";
            this.firstAuthorLabel.Size = new System.Drawing.Size(331, 13);
            this.firstAuthorLabel.TabIndex = 2;
            this.firstAuthorLabel.Text = "VirtualCam and BaseClass created by Maxim Kartavenkov aka Sonic";
            this.firstAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondAuthorLabel
            // 
            this.secondAuthorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.secondAuthorLabel.AutoSize = true;
            this.secondAuthorLabel.Location = new System.Drawing.Point(64, 189);
            this.secondAuthorLabel.Name = "secondAuthorLabel";
            this.secondAuthorLabel.Size = new System.Drawing.Size(236, 13);
            this.secondAuthorLabel.TabIndex = 4;
            this.secondAuthorLabel.Text = "Kinect Driver as WebCam created by Piotr Sowa";
            this.secondAuthorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // angleTrackBar
            // 
            this.angleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.angleTrackBar.Location = new System.Drawing.Point(6, 15);
            this.angleTrackBar.Maximum = 27;
            this.angleTrackBar.Minimum = -27;
            this.angleTrackBar.Name = "angleTrackBar";
            this.angleTrackBar.Size = new System.Drawing.Size(322, 45);
            this.angleTrackBar.TabIndex = 5;
            this.angleTrackBar.Scroll += new System.EventHandler(this.angleTrackBar_Scroll);
            // 
            // angleGroupBox
            // 
            this.angleGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.angleGroupBox.Controls.Add(this.angleTrackBar);
            this.angleGroupBox.Location = new System.Drawing.Point(15, 70);
            this.angleGroupBox.Name = "angleGroupBox";
            this.angleGroupBox.Size = new System.Drawing.Size(339, 66);
            this.angleGroupBox.TabIndex = 6;
            this.angleGroupBox.TabStop = false;
            this.angleGroupBox.Text = "Angle";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 215);
            this.Controls.Add(this.angleGroupBox);
            this.Controls.Add(this.secondAuthorLabel);
            this.Controls.Add(this.firstAuthorLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "AboutForm";
            this.Text = "About";
            this.Title = "About";
            ((System.ComponentModel.ISupportInitialize)(this.angleTrackBar)).EndInit();
            this.angleGroupBox.ResumeLayout(false);
            this.angleGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label firstAuthorLabel;
        private System.Windows.Forms.Label secondAuthorLabel;
        private System.Windows.Forms.TrackBar angleTrackBar;
        private System.Windows.Forms.GroupBox angleGroupBox;
    }
}