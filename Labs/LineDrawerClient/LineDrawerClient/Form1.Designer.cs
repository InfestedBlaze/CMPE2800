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
            this.UI_StatusStrip_Info = new System.Windows.Forms.StatusStrip();
            this.UI_toolStripSplitButton_Connect = new System.Windows.Forms.ToolStripSplitButton();
            this.UI_toolStripStatusLabel_Colour = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_ThicknessStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_FramesRecieved = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_FragmentsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_toolStripStatusLabel_DestackAverage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.UI_StatusStrip_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_StatusStrip_Info
            // 
            this.UI_StatusStrip_Info.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UI_toolStripSplitButton_Connect,
            this.UI_toolStripStatusLabel_Colour,
            this.UI_toolStripStatusLabel_ThicknessStatus,
            this.UI_toolStripStatusLabel_FramesRecieved,
            this.UI_toolStripStatusLabel_FragmentsCount,
            this.UI_toolStripStatusLabel_DestackAverage,
            this.toolStripStatusLabel1});
            this.UI_StatusStrip_Info.Location = new System.Drawing.Point(0, 539);
            this.UI_StatusStrip_Info.Name = "UI_StatusStrip_Info";
            this.UI_StatusStrip_Info.Size = new System.Drawing.Size(584, 22);
            this.UI_StatusStrip_Info.TabIndex = 0;
            this.UI_StatusStrip_Info.Text = "statusStrip1";
            // 
            // UI_toolStripSplitButton_Connect
            // 
            this.UI_toolStripSplitButton_Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UI_toolStripSplitButton_Connect.Image = ((System.Drawing.Image)(resources.GetObject("UI_toolStripSplitButton_Connect.Image")));
            this.UI_toolStripSplitButton_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UI_toolStripSplitButton_Connect.Name = "UI_toolStripSplitButton_Connect";
            this.UI_toolStripSplitButton_Connect.Size = new System.Drawing.Size(77, 20);
            this.UI_toolStripSplitButton_Connect.Text = "Connect...";
            // 
            // UI_toolStripStatusLabel_Colour
            // 
            this.UI_toolStripStatusLabel_Colour.Name = "UI_toolStripStatusLabel_Colour";
            this.UI_toolStripStatusLabel_Colour.Size = new System.Drawing.Size(43, 17);
            this.UI_toolStripStatusLabel_Colour.Text = "Colour";
            // 
            // UI_toolStripStatusLabel_ThicknessStatus
            // 
            this.UI_toolStripStatusLabel_ThicknessStatus.Name = "UI_toolStripStatusLabel_ThicknessStatus";
            this.UI_toolStripStatusLabel_ThicknessStatus.Size = new System.Drawing.Size(70, 17);
            this.UI_toolStripStatusLabel_ThicknessStatus.Text = "Thickness: -";
            // 
            // UI_toolStripStatusLabel_FramesRecieved
            // 
            this.UI_toolStripStatusLabel_FramesRecieved.Name = "UI_toolStripStatusLabel_FramesRecieved";
            this.UI_toolStripStatusLabel_FramesRecieved.Size = new System.Drawing.Size(106, 17);
            this.UI_toolStripStatusLabel_FramesRecieved.Text = "Frames Recieved: -";
            // 
            // UI_toolStripStatusLabel_FragmentsCount
            // 
            this.UI_toolStripStatusLabel_FragmentsCount.Name = "UI_toolStripStatusLabel_FragmentsCount";
            this.UI_toolStripStatusLabel_FragmentsCount.Size = new System.Drawing.Size(74, 17);
            this.UI_toolStripStatusLabel_FragmentsCount.Text = "Fragments: -";
            // 
            // UI_toolStripStatusLabel_DestackAverage
            // 
            this.UI_toolStripStatusLabel_DestackAverage.Name = "UI_toolStripStatusLabel_DestackAverage";
            this.UI_toolStripStatusLabel_DestackAverage.Size = new System.Drawing.Size(105, 17);
            this.UI_toolStripStatusLabel_DestackAverage.Text = "Destack Average: -";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(96, 17);
            this.toolStripStatusLabel1.Text = "Bytes Received: -";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.UI_StatusStrip_Info);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.Text = "Line Drawer Client";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.UI_StatusStrip_Info.ResumeLayout(false);
            this.UI_StatusStrip_Info.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip UI_StatusStrip_Info;
        private System.Windows.Forms.ToolStripSplitButton UI_toolStripSplitButton_Connect;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_Colour;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_ThicknessStatus;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_FramesRecieved;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_FragmentsCount;
        private System.Windows.Forms.ToolStripStatusLabel UI_toolStripStatusLabel_DestackAverage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

