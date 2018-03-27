namespace Server
{
    partial class Server
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
            this.UI_label_Answer = new System.Windows.Forms.Label();
            this.DiscBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UI_label_Answer
            // 
            this.UI_label_Answer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_label_Answer.Location = new System.Drawing.Point(12, 9);
            this.UI_label_Answer.Name = "UI_label_Answer";
            this.UI_label_Answer.Size = new System.Drawing.Size(160, 144);
            this.UI_label_Answer.TabIndex = 0;
            this.UI_label_Answer.Text = "Number";
            this.UI_label_Answer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DiscBtn
            // 
            this.DiscBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DiscBtn.Location = new System.Drawing.Point(52, 130);
            this.DiscBtn.Name = "DiscBtn";
            this.DiscBtn.Size = new System.Drawing.Size(75, 23);
            this.DiscBtn.TabIndex = 1;
            this.DiscBtn.Text = "Disconnect";
            this.DiscBtn.UseVisualStyleBackColor = true;
            this.DiscBtn.Click += new System.EventHandler(this.DiscBtn_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 162);
            this.Controls.Add(this.DiscBtn);
            this.Controls.Add(this.UI_label_Answer);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label UI_label_Answer;
        private System.Windows.Forms.Button DiscBtn;
    }
}

