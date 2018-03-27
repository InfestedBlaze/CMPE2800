namespace Client
{
    partial class SocketSelect
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
            this.UI_label_Instruction = new System.Windows.Forms.Label();
            this.UI_numericUpDown_Port = new System.Windows.Forms.NumericUpDown();
            this.UI_button_Connect = new System.Windows.Forms.Button();
            this.UI_label_IP = new System.Windows.Forms.Label();
            this.UI_textBox_IPAddress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_label_Instruction
            // 
            this.UI_label_Instruction.AutoSize = true;
            this.UI_label_Instruction.Location = new System.Drawing.Point(13, 13);
            this.UI_label_Instruction.Name = "UI_label_Instruction";
            this.UI_label_Instruction.Size = new System.Drawing.Size(124, 13);
            this.UI_label_Instruction.TabIndex = 0;
            this.UI_label_Instruction.Text = "Select a port to connect:";
            // 
            // UI_numericUpDown_Port
            // 
            this.UI_numericUpDown_Port.Cursor = System.Windows.Forms.Cursors.Default;
            this.UI_numericUpDown_Port.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Location = new System.Drawing.Point(143, 11);
            this.UI_numericUpDown_Port.Maximum = new decimal(new int[] {
            49151,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Name = "UI_numericUpDown_Port";
            this.UI_numericUpDown_Port.ReadOnly = true;
            this.UI_numericUpDown_Port.Size = new System.Drawing.Size(129, 20);
            this.UI_numericUpDown_Port.TabIndex = 1;
            this.UI_numericUpDown_Port.Value = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            // 
            // UI_button_Connect
            // 
            this.UI_button_Connect.Location = new System.Drawing.Point(12, 63);
            this.UI_button_Connect.Name = "UI_button_Connect";
            this.UI_button_Connect.Size = new System.Drawing.Size(259, 23);
            this.UI_button_Connect.TabIndex = 2;
            this.UI_button_Connect.Text = "Connect";
            this.UI_button_Connect.UseVisualStyleBackColor = true;
            this.UI_button_Connect.Click += new System.EventHandler(this.UI_button_Connect_Click);
            // 
            // UI_label_IP
            // 
            this.UI_label_IP.AutoSize = true;
            this.UI_label_IP.Location = new System.Drawing.Point(13, 40);
            this.UI_label_IP.Name = "UI_label_IP";
            this.UI_label_IP.Size = new System.Drawing.Size(64, 13);
            this.UI_label_IP.TabIndex = 3;
            this.UI_label_IP.Text = "IP Address: ";
            // 
            // UI_textBox_IPAddress
            // 
            this.UI_textBox_IPAddress.Location = new System.Drawing.Point(144, 37);
            this.UI_textBox_IPAddress.Name = "UI_textBox_IPAddress";
            this.UI_textBox_IPAddress.Size = new System.Drawing.Size(128, 20);
            this.UI_textBox_IPAddress.TabIndex = 4;
            this.UI_textBox_IPAddress.Text = "localhost";
            // 
            // SocketSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 97);
            this.ControlBox = false;
            this.Controls.Add(this.UI_textBox_IPAddress);
            this.Controls.Add(this.UI_label_IP);
            this.Controls.Add(this.UI_button_Connect);
            this.Controls.Add(this.UI_numericUpDown_Port);
            this.Controls.Add(this.UI_label_Instruction);
            this.MaximumSize = new System.Drawing.Size(300, 136);
            this.MinimumSize = new System.Drawing.Size(300, 136);
            this.Name = "SocketSelect";
            this.Text = "SocketSelect";
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UI_label_Instruction;
        private System.Windows.Forms.NumericUpDown UI_numericUpDown_Port;
        private System.Windows.Forms.Button UI_button_Connect;
        private System.Windows.Forms.Label UI_label_IP;
        private System.Windows.Forms.TextBox UI_textBox_IPAddress;
    }
}