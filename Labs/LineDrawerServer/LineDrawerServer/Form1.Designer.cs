namespace LineDrawerServer
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.UI_toolStripStatusLabel_TotalBytes = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_TotalFrames = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_dataGridView_Clients = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView_Clients)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UI_toolStripStatusLabel_TotalBytes,
            this.UI_toolStripStatusLabel_TotalFrames});
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(316, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // UI_toolStripStatusLabel_TotalBytes
            // 
            this.UI_toolStripStatusLabel_TotalBytes.Name = "UI_toolStripStatusLabel_TotalBytes";
            this.UI_toolStripStatusLabel_TotalBytes.Size = new System.Drawing.Size(75, 17);
            this.UI_toolStripStatusLabel_TotalBytes.Text = "Total Bytes: -";
            // 
            // UI_toolStripStatusLabel_TotalFrames
            // 
            this.UI_toolStripStatusLabel_TotalFrames.Name = "UI_toolStripStatusLabel_TotalFrames";
            this.UI_toolStripStatusLabel_TotalFrames.Size = new System.Drawing.Size(85, 17);
            this.UI_toolStripStatusLabel_TotalFrames.Text = "Total Frames: -";
            // 
            // UI_dataGridView_Clients
            // 
            this.UI_dataGridView_Clients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_dataGridView_Clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_dataGridView_Clients.Location = new System.Drawing.Point(13, 12);
            this.UI_dataGridView_Clients.Name = "UI_dataGridView_Clients";
            this.UI_dataGridView_Clients.Size = new System.Drawing.Size(291, 375);
            this.UI_dataGridView_Clients.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 412);
            this.Controls.Add(this.UI_dataGridView_Clients);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Line Drawer Server";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView_Clients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_TotalBytes;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_TotalFrames;
        private System.Windows.Forms.DataGridView UI_dataGridView_Clients;
    }
}

