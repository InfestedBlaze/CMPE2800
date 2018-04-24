namespace Chatroom
{
    partial class ServerConnect
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
            this.label1 = new System.Windows.Forms.Label();
            this.UI_numericUpDown_Port = new System.Windows.Forms.NumericUpDown();
            this.UI_button_Ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // UI_numericUpDown_Port
            // 
            this.UI_numericUpDown_Port.Location = new System.Drawing.Point(48, 11);
            this.UI_numericUpDown_Port.Maximum = new decimal(new int[] {
            49151,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Name = "UI_numericUpDown_Port";
            this.UI_numericUpDown_Port.Size = new System.Drawing.Size(224, 20);
            this.UI_numericUpDown_Port.TabIndex = 1;
            this.UI_numericUpDown_Port.Value = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.ValueChanged += new System.EventHandler(this.UI_numericUpDown_Port_ValueChanged);
            // 
            // UI_button_Ok
            // 
            this.UI_button_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UI_button_Ok.Location = new System.Drawing.Point(16, 40);
            this.UI_button_Ok.Name = "UI_button_Ok";
            this.UI_button_Ok.Size = new System.Drawing.Size(256, 23);
            this.UI_button_Ok.TabIndex = 2;
            this.UI_button_Ok.Text = "Ok";
            this.UI_button_Ok.UseVisualStyleBackColor = true;
            // 
            // ServerConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 73);
            this.Controls.Add(this.UI_button_Ok);
            this.Controls.Add(this.UI_numericUpDown_Port);
            this.Controls.Add(this.label1);
            this.Name = "ServerConnect";
            this.Text = "ServerConnect";
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UI_numericUpDown_Port;
        private System.Windows.Forms.Button UI_button_Ok;
    }
}