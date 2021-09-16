
namespace Cheer_fic_Pears
{
    partial class frmTimeOptions
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad5Min = new System.Windows.Forms.RadioButton();
            this.rad3Min = new System.Windows.Forms.RadioButton();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rad5Min);
            this.groupBox1.Controls.Add(this.rad3Min);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(37, 62);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(239, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Game Length:";
            // 
            // rad5Min
            // 
            this.rad5Min.AutoSize = true;
            this.rad5Min.Location = new System.Drawing.Point(22, 77);
            this.rad5Min.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rad5Min.Name = "rad5Min";
            this.rad5Min.Size = new System.Drawing.Size(103, 24);
            this.rad5Min.TabIndex = 1;
            this.rad5Min.TabStop = true;
            this.rad5Min.Text = "5 minutes";
            this.rad5Min.UseVisualStyleBackColor = true;
            // 
            // rad3Min
            // 
            this.rad3Min.AutoSize = true;
            this.rad3Min.Location = new System.Drawing.Point(22, 42);
            this.rad3Min.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rad3Min.Name = "rad3Min";
            this.rad3Min.Size = new System.Drawing.Size(103, 24);
            this.rad3Min.TabIndex = 0;
            this.rad3Min.TabStop = true;
            this.rad3Min.Text = "3 minutes";
            this.rad3Min.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(103, 194);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(88, 38);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Image = global::Cheer_fic_Pears.Resource.BackArrow;
            this.btnGoBack.Location = new System.Drawing.Point(12, 12);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(37, 33);
            this.btnGoBack.TabIndex = 6;
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // frmTimeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(324, 265);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTimeOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTimeOptions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rad5Min;
        private System.Windows.Forms.RadioButton rad3Min;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnGoBack;
    }
}