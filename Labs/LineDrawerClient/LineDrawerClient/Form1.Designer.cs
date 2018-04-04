namespace LineDrawerClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.UI_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.UI_toolStripStatusLabel_Colour = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_ThicknessStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_FramesRecieved = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_FragmentsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_DestackAverage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripSplitLabel_Connect = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_StatusStrip
            // 
            this.UI_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UI_toolStripSplitLabel_Connect,
            this.UI_toolStripStatusLabel_Colour,
            this.UI_toolStripStatusLabel_ThicknessStatus,
            this.UI_toolStripStatusLabel_FramesRecieved,
            this.UI_toolStripStatusLabel_FragmentsCount,
            this.UI_toolStripStatusLabel_DestackAverage,
            this.toolStripStatusLabel1});
            this.UI_StatusStrip.Location = new System.Drawing.Point(0, 537);
            this.UI_StatusStrip.Name = "UI_StatusStrip";
            this.UI_StatusStrip.Size = new System.Drawing.Size(634, 24);
            this.UI_StatusStrip.TabIndex = 0;
            this.UI_StatusStrip.Text = "statusStrip1";
            // 
            // UI_toolStripStatusLabel_Colour
            // 
            this.UI_toolStripStatusLabel_Colour.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.UI_toolStripStatusLabel_Colour.ForeColor = System.Drawing.Color.Red;
            this.UI_toolStripStatusLabel_Colour.Name = "UI_toolStripStatusLabel_Colour";
            this.UI_toolStripStatusLabel_Colour.Size = new System.Drawing.Size(47, 19);
            this.UI_toolStripStatusLabel_Colour.Text = "Colour";
            // 
            // UI_toolStripStatusLabel_ThicknessStatus
            // 
            this.UI_toolStripStatusLabel_ThicknessStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.UI_toolStripStatusLabel_ThicknessStatus.Name = "UI_toolStripStatusLabel_ThicknessStatus";
            this.UI_toolStripStatusLabel_ThicknessStatus.Size = new System.Drawing.Size(81, 19);
            this.UI_toolStripStatusLabel_ThicknessStatus.Text = "Thickness: 10";
            // 
            // UI_toolStripStatusLabel_FramesRecieved
            // 
            this.UI_toolStripStatusLabel_FramesRecieved.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.UI_toolStripStatusLabel_FramesRecieved.Name = "UI_toolStripStatusLabel_FramesRecieved";
            this.UI_toolStripStatusLabel_FramesRecieved.Size = new System.Drawing.Size(110, 19);
            this.UI_toolStripStatusLabel_FramesRecieved.Text = "Frames Recieved: -";
            // 
            // UI_toolStripStatusLabel_FragmentsCount
            // 
            this.UI_toolStripStatusLabel_FragmentsCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.UI_toolStripStatusLabel_FragmentsCount.Name = "UI_toolStripStatusLabel_FragmentsCount";
            this.UI_toolStripStatusLabel_FragmentsCount.Size = new System.Drawing.Size(78, 19);
            this.UI_toolStripStatusLabel_FragmentsCount.Text = "Fragments: -";
            // 
            // UI_toolStripStatusLabel_DestackAverage
            // 
            this.UI_toolStripStatusLabel_DestackAverage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.UI_toolStripStatusLabel_DestackAverage.Name = "UI_toolStripStatusLabel_DestackAverage";
            this.UI_toolStripStatusLabel_DestackAverage.Size = new System.Drawing.Size(109, 19);
            this.UI_toolStripStatusLabel_DestackAverage.Text = "Destack Average: -";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(96, 19);
            this.toolStripStatusLabel1.Text = "Bytes Received: -";
            // 
            // UI_toolStripSplitLabel_Connect
            // 
            this.UI_toolStripSplitLabel_Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UI_toolStripSplitLabel_Connect.Image = ((System.Drawing.Image)(resources.GetObject("UI_toolStripSplitLabel_Connect.Image")));
            this.UI_toolStripSplitLabel_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UI_toolStripSplitLabel_Connect.Name = "UI_toolStripSplitLabel_Connect";
            this.UI_toolStripSplitLabel_Connect.Size = new System.Drawing.Size(61, 19);
            this.UI_toolStripSplitLabel_Connect.Text = "Connect...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 561);
            this.Controls.Add(this.UI_StatusStrip);
            this.MinimumSize = new System.Drawing.Size(650, 600);
            this.Name = "Form1";
            this.Text = "Line Drawer Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.UI_StatusStrip.ResumeLayout(false);
            this.UI_StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip UI_StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_Colour;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_ThicknessStatus;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_FramesRecieved;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_FragmentsCount;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_DestackAverage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripSplitLabel_Connect;
    }
}

