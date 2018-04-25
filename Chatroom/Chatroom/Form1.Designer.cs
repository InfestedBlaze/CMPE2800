namespace Chatroom
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
            this.UI_textBox_Input = new System.Windows.Forms.TextBox();
            this.UI_button_Send = new System.Windows.Forms.Button();
            this.UI_richTextBox_Display = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.UI_toolStripMenuItem_Host = new System.Windows.Forms.ToolStripMenuItem();
            this.UI_ToolStripMenuItem_Join = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_textBox_Input
            // 
            this.UI_textBox_Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_textBox_Input.Location = new System.Drawing.Point(13, 479);
            this.UI_textBox_Input.Name = "UI_textBox_Input";
            this.UI_textBox_Input.Size = new System.Drawing.Size(453, 20);
            this.UI_textBox_Input.TabIndex = 0;
            // 
            // UI_button_Send
            // 
            this.UI_button_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_Send.Enabled = false;
            this.UI_button_Send.Location = new System.Drawing.Point(472, 479);
            this.UI_button_Send.Name = "UI_button_Send";
            this.UI_button_Send.Size = new System.Drawing.Size(75, 23);
            this.UI_button_Send.TabIndex = 1;
            this.UI_button_Send.Text = "Send";
            this.UI_button_Send.UseVisualStyleBackColor = true;
            this.UI_button_Send.Click += new System.EventHandler(this.UI_button_Send_Click);
            // 
            // UI_richTextBox_Display
            // 
            this.UI_richTextBox_Display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_richTextBox_Display.Location = new System.Drawing.Point(13, 27);
            this.UI_richTextBox_Display.Name = "UI_richTextBox_Display";
            this.UI_richTextBox_Display.ReadOnly = true;
            this.UI_richTextBox_Display.Size = new System.Drawing.Size(534, 446);
            this.UI_richTextBox_Display.TabIndex = 2;
            this.UI_richTextBox_Display.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UI_toolStripMenuItem_Host,
            this.UI_ToolStripMenuItem_Join});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // UI_toolStripMenuItem_Host
            // 
            this.UI_toolStripMenuItem_Host.Name = "UI_toolStripMenuItem_Host";
            this.UI_toolStripMenuItem_Host.Size = new System.Drawing.Size(44, 20);
            this.UI_toolStripMenuItem_Host.Text = "Host";
            this.UI_toolStripMenuItem_Host.Click += new System.EventHandler(this.UI_toolStripMenuItem_Host_Click);
            // 
            // UI_ToolStripMenuItem_Join
            // 
            this.UI_ToolStripMenuItem_Join.Name = "UI_ToolStripMenuItem_Join";
            this.UI_ToolStripMenuItem_Join.Size = new System.Drawing.Size(40, 20);
            this.UI_ToolStripMenuItem_Join.Text = "Join";
            this.UI_ToolStripMenuItem_Join.Click += new System.EventHandler(this.UI_toolStripMenuItem_Join_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 511);
            this.Controls.Add(this.UI_richTextBox_Display);
            this.Controls.Add(this.UI_button_Send);
            this.Controls.Add(this.UI_textBox_Input);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Chatroom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UI_textBox_Input;
        private System.Windows.Forms.Button UI_button_Send;
        private System.Windows.Forms.RichTextBox UI_richTextBox_Display;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UI_toolStripMenuItem_Host;
        private System.Windows.Forms.ToolStripMenuItem UI_ToolStripMenuItem_Join;
    }
}

