namespace Client
{
    partial class Client
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
            this.UI_label_Status = new System.Windows.Forms.Label();
            this.UI_trackBar_Guess = new System.Windows.Forms.TrackBar();
            this.UI_label_Min = new System.Windows.Forms.Label();
            this.UI_label_Max = new System.Windows.Forms.Label();
            this.UI_label_Current = new System.Windows.Forms.Label();
            this.UI_button_SendGuess = new System.Windows.Forms.Button();
            this.UI_button_Disconnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_trackBar_Guess)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_label_Status
            // 
            this.UI_label_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_label_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UI_label_Status.Location = new System.Drawing.Point(13, 13);
            this.UI_label_Status.Name = "UI_label_Status";
            this.UI_label_Status.Size = new System.Drawing.Size(259, 23);
            this.UI_label_Status.TabIndex = 0;
            this.UI_label_Status.Text = "Status Bar";
            this.UI_label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UI_trackBar_Guess
            // 
            this.UI_trackBar_Guess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_trackBar_Guess.LargeChange = 10;
            this.UI_trackBar_Guess.Location = new System.Drawing.Point(13, 40);
            this.UI_trackBar_Guess.Maximum = 1000;
            this.UI_trackBar_Guess.Minimum = 1;
            this.UI_trackBar_Guess.Name = "UI_trackBar_Guess";
            this.UI_trackBar_Guess.Size = new System.Drawing.Size(259, 45);
            this.UI_trackBar_Guess.TabIndex = 1;
            this.UI_trackBar_Guess.TickFrequency = 50;
            this.UI_trackBar_Guess.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.UI_trackBar_Guess.Value = 500;
            this.UI_trackBar_Guess.Scroll += new System.EventHandler(this.UI_trackBar_Guess_Scroll);
            // 
            // UI_label_Min
            // 
            this.UI_label_Min.AutoSize = true;
            this.UI_label_Min.Location = new System.Drawing.Point(13, 92);
            this.UI_label_Min.Name = "UI_label_Min";
            this.UI_label_Min.Size = new System.Drawing.Size(13, 13);
            this.UI_label_Min.TabIndex = 2;
            this.UI_label_Min.Text = "1";
            this.UI_label_Min.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UI_label_Max
            // 
            this.UI_label_Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_label_Max.AutoSize = true;
            this.UI_label_Max.Location = new System.Drawing.Point(236, 92);
            this.UI_label_Max.Name = "UI_label_Max";
            this.UI_label_Max.Size = new System.Drawing.Size(31, 13);
            this.UI_label_Max.TabIndex = 3;
            this.UI_label_Max.Text = "1000";
            this.UI_label_Max.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UI_label_Current
            // 
            this.UI_label_Current.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.UI_label_Current.AutoSize = true;
            this.UI_label_Current.Location = new System.Drawing.Point(128, 92);
            this.UI_label_Current.Name = "UI_label_Current";
            this.UI_label_Current.Size = new System.Drawing.Size(25, 13);
            this.UI_label_Current.TabIndex = 4;
            this.UI_label_Current.Text = "500";
            this.UI_label_Current.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UI_button_SendGuess
            // 
            this.UI_button_SendGuess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_SendGuess.Location = new System.Drawing.Point(12, 108);
            this.UI_button_SendGuess.Name = "UI_button_SendGuess";
            this.UI_button_SendGuess.Size = new System.Drawing.Size(260, 23);
            this.UI_button_SendGuess.TabIndex = 5;
            this.UI_button_SendGuess.Text = "Guess!";
            this.UI_button_SendGuess.UseVisualStyleBackColor = true;
            this.UI_button_SendGuess.Click += new System.EventHandler(this.UI_button_SendGuess_Click);
            // 
            // UI_button_Disconnect
            // 
            this.UI_button_Disconnect.Location = new System.Drawing.Point(13, 137);
            this.UI_button_Disconnect.Name = "UI_button_Disconnect";
            this.UI_button_Disconnect.Size = new System.Drawing.Size(259, 23);
            this.UI_button_Disconnect.TabIndex = 6;
            this.UI_button_Disconnect.Text = "Disconnect";
            this.UI_button_Disconnect.UseVisualStyleBackColor = true;
            this.UI_button_Disconnect.Click += new System.EventHandler(this.UI_button_Disconnect_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 166);
            this.Controls.Add(this.UI_button_Disconnect);
            this.Controls.Add(this.UI_button_SendGuess);
            this.Controls.Add(this.UI_label_Current);
            this.Controls.Add(this.UI_label_Max);
            this.Controls.Add(this.UI_label_Min);
            this.Controls.Add(this.UI_trackBar_Guess);
            this.Controls.Add(this.UI_label_Status);
            this.MaximumSize = new System.Drawing.Size(300, 205);
            this.MinimumSize = new System.Drawing.Size(300, 205);
            this.Name = "Client";
            this.Text = "Client";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.UI_trackBar_Guess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_label_Status;
        private System.Windows.Forms.TrackBar UI_trackBar_Guess;
        private System.Windows.Forms.Label UI_label_Min;
        private System.Windows.Forms.Label UI_label_Max;
        private System.Windows.Forms.Label UI_label_Current;
        private System.Windows.Forms.Button UI_button_SendGuess;
        private System.Windows.Forms.Button UI_button_Disconnect;
    }
}

