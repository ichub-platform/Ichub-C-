namespace IchubApp
{
    partial class Form1
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
            this.btnLed1 = new System.Windows.Forms.Button();
            this.trbdimer = new System.Windows.Forms.TrackBar();
            this.lbss = new System.Windows.Forms.Label();
            this.lbuint = new System.Windows.Forms.Label();
            this.lbdimername = new System.Windows.Forms.Label();
            this.lbledname = new System.Windows.Forms.Label();
            this.lbssname = new System.Windows.Forms.Label();
            this.lbdimerdata = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trbdimer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLed1
            // 
            this.btnLed1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLed1.Location = new System.Drawing.Point(193, 383);
            this.btnLed1.Name = "btnLed1";
            this.btnLed1.Size = new System.Drawing.Size(123, 77);
            this.btnLed1.TabIndex = 0;
            this.btnLed1.UseVisualStyleBackColor = true;
            this.btnLed1.Click += new System.EventHandler(this.btnLed1_Click);
            // 
            // trbdimer
            // 
            this.trbdimer.Location = new System.Drawing.Point(159, 223);
            this.trbdimer.Maximum = 100;
            this.trbdimer.Name = "trbdimer";
            this.trbdimer.Size = new System.Drawing.Size(320, 56);
            this.trbdimer.TabIndex = 1;
            this.trbdimer.MouseCaptureChanged += new System.EventHandler(this.trbdimer_MouseCaptureChanged);
            // 
            // lbss
            // 
            this.lbss.AutoSize = true;
            this.lbss.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbss.Location = new System.Drawing.Point(160, 46);
            this.lbss.Name = "lbss";
            this.lbss.Size = new System.Drawing.Size(131, 58);
            this.lbss.TabIndex = 2;
            this.lbss.Text = "Data";
            this.lbss.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbuint
            // 
            this.lbuint.AutoSize = true;
            this.lbuint.Location = new System.Drawing.Point(343, 87);
            this.lbuint.Name = "lbuint";
            this.lbuint.Size = new System.Drawing.Size(31, 17);
            this.lbuint.TabIndex = 3;
            this.lbuint.Text = "unit";
            // 
            // lbdimername
            // 
            this.lbdimername.AutoSize = true;
            this.lbdimername.Location = new System.Drawing.Point(23, 223);
            this.lbdimername.Name = "lbdimername";
            this.lbdimername.Size = new System.Drawing.Size(43, 17);
            this.lbdimername.TabIndex = 4;
            this.lbdimername.Text = "name";
            // 
            // lbledname
            // 
            this.lbledname.AutoSize = true;
            this.lbledname.Location = new System.Drawing.Point(220, 484);
            this.lbledname.Name = "lbledname";
            this.lbledname.Size = new System.Drawing.Size(43, 17);
            this.lbledname.TabIndex = 5;
            this.lbledname.Text = "name";
            // 
            // lbssname
            // 
            this.lbssname.AutoSize = true;
            this.lbssname.Location = new System.Drawing.Point(57, 69);
            this.lbssname.Name = "lbssname";
            this.lbssname.Size = new System.Drawing.Size(43, 17);
            this.lbssname.TabIndex = 6;
            this.lbssname.Text = "name";
            // 
            // lbdimerdata
            // 
            this.lbdimerdata.AutoSize = true;
            this.lbdimerdata.Location = new System.Drawing.Point(286, 191);
            this.lbdimerdata.Name = "lbdimerdata";
            this.lbdimerdata.Size = new System.Drawing.Size(36, 17);
            this.lbdimerdata.TabIndex = 7;
            this.lbdimerdata.Text = "data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 581);
            this.Controls.Add(this.lbdimerdata);
            this.Controls.Add(this.lbssname);
            this.Controls.Add(this.lbledname);
            this.Controls.Add(this.lbdimername);
            this.Controls.Add(this.lbuint);
            this.Controls.Add(this.lbss);
            this.Controls.Add(this.trbdimer);
            this.Controls.Add(this.btnLed1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbdimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLed1;
        private System.Windows.Forms.TrackBar trbdimer;
        private System.Windows.Forms.Label lbss;
        private System.Windows.Forms.Label lbuint;
        private System.Windows.Forms.Label lbdimername;
        private System.Windows.Forms.Label lbledname;
        private System.Windows.Forms.Label lbssname;
        private System.Windows.Forms.Label lbdimerdata;
    }
}

