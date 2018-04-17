namespace LineDrawerClient
{
    partial class AddressSelect
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
            this.UI_textBox_Address = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UI_numericUpDown_Port = new System.Windows.Forms.NumericUpDown();
            this.UI_button_Connect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // UI_textBox_Address
            // 
            this.UI_textBox_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_textBox_Address.Location = new System.Drawing.Point(65, 5);
            this.UI_textBox_Address.Name = "UI_textBox_Address";
            this.UI_textBox_Address.Size = new System.Drawing.Size(132, 20);
            this.UI_textBox_Address.TabIndex = 1;
            this.UI_textBox_Address.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // UI_numericUpDown_Port
            // 
            this.UI_numericUpDown_Port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_numericUpDown_Port.Enabled = false;
            this.UI_numericUpDown_Port.Location = new System.Drawing.Point(65, 37);
            this.UI_numericUpDown_Port.Maximum = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Minimum = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            this.UI_numericUpDown_Port.Name = "UI_numericUpDown_Port";
            this.UI_numericUpDown_Port.Size = new System.Drawing.Size(132, 20);
            this.UI_numericUpDown_Port.TabIndex = 3;
            this.UI_numericUpDown_Port.Value = new decimal(new int[] {
            1666,
            0,
            0,
            0});
            // 
            // UI_button_Connect
            // 
            this.UI_button_Connect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_Connect.Location = new System.Drawing.Point(13, 76);
            this.UI_button_Connect.Name = "UI_button_Connect";
            this.UI_button_Connect.Size = new System.Drawing.Size(184, 23);
            this.UI_button_Connect.TabIndex = 4;
            this.UI_button_Connect.Text = "Connect";
            this.UI_button_Connect.UseVisualStyleBackColor = true;
            this.UI_button_Connect.Click += new System.EventHandler(this.UI_button_Connect_Click);
            // 
            // AddressSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 111);
            this.Controls.Add(this.UI_button_Connect);
            this.Controls.Add(this.UI_numericUpDown_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UI_textBox_Address);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(225, 150);
            this.MinimumSize = new System.Drawing.Size(225, 150);
            this.Name = "AddressSelect";
            this.Text = "AddressSelect";
            ((System.ComponentModel.ISupportInitialize)(this.UI_numericUpDown_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UI_textBox_Address;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown UI_numericUpDown_Port;
        private System.Windows.Forms.Button UI_button_Connect;
    }
}