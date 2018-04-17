namespace LineDrawerClient
{
    partial class ColourThickness
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
            this.UI_colorDialog = new System.Windows.Forms.ColorDialog();
            this.UI_label_Thickness = new System.Windows.Forms.Label();
            this.UI_trackBar_Thickness = new System.Windows.Forms.TrackBar();
            this.UI_label_ThickNumber = new System.Windows.Forms.Label();
            this.UI_button_ColourSelect = new System.Windows.Forms.Button();
            this.UI_label_OpacityText = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.UI_label_OpacityValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UI_trackBar_Thickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_label_Thickness
            // 
            this.UI_label_Thickness.AutoSize = true;
            this.UI_label_Thickness.Location = new System.Drawing.Point(14, 13);
            this.UI_label_Thickness.Name = "UI_label_Thickness";
            this.UI_label_Thickness.Size = new System.Drawing.Size(56, 13);
            this.UI_label_Thickness.TabIndex = 0;
            this.UI_label_Thickness.Text = "Thickness";
            // 
            // UI_trackBar_Thickness
            // 
            this.UI_trackBar_Thickness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_trackBar_Thickness.Location = new System.Drawing.Point(76, 13);
            this.UI_trackBar_Thickness.Maximum = 100;
            this.UI_trackBar_Thickness.Minimum = 1;
            this.UI_trackBar_Thickness.Name = "UI_trackBar_Thickness";
            this.UI_trackBar_Thickness.Size = new System.Drawing.Size(196, 45);
            this.UI_trackBar_Thickness.TabIndex = 1;
            this.UI_trackBar_Thickness.TickFrequency = 10;
            this.UI_trackBar_Thickness.Value = 10;
            this.UI_trackBar_Thickness.Scroll += new System.EventHandler(this.UI_trackBar_Thickness_Scroll);
            // 
            // UI_label_ThickNumber
            // 
            this.UI_label_ThickNumber.AutoSize = true;
            this.UI_label_ThickNumber.Location = new System.Drawing.Point(17, 30);
            this.UI_label_ThickNumber.Name = "UI_label_ThickNumber";
            this.UI_label_ThickNumber.Size = new System.Drawing.Size(19, 13);
            this.UI_label_ThickNumber.TabIndex = 2;
            this.UI_label_ThickNumber.Text = "10";
            // 
            // UI_button_ColourSelect
            // 
            this.UI_button_ColourSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_ColourSelect.Location = new System.Drawing.Point(12, 114);
            this.UI_button_ColourSelect.Name = "UI_button_ColourSelect";
            this.UI_button_ColourSelect.Size = new System.Drawing.Size(259, 23);
            this.UI_button_ColourSelect.TabIndex = 3;
            this.UI_button_ColourSelect.Text = "Change Colour";
            this.UI_button_ColourSelect.UseVisualStyleBackColor = true;
            this.UI_button_ColourSelect.Click += new System.EventHandler(this.UI_button_ColourSelect_Click);
            // 
            // UI_label_OpacityText
            // 
            this.UI_label_OpacityText.AutoSize = true;
            this.UI_label_OpacityText.Location = new System.Drawing.Point(20, 62);
            this.UI_label_OpacityText.Name = "UI_label_OpacityText";
            this.UI_label_OpacityText.Size = new System.Drawing.Size(43, 13);
            this.UI_label_OpacityText.TabIndex = 4;
            this.UI_label_OpacityText.Text = "Opacity";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(76, 62);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(195, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 255;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // UI_label_OpacityValue
            // 
            this.UI_label_OpacityValue.AutoSize = true;
            this.UI_label_OpacityValue.Location = new System.Drawing.Point(20, 79);
            this.UI_label_OpacityValue.Name = "UI_label_OpacityValue";
            this.UI_label_OpacityValue.Size = new System.Drawing.Size(25, 13);
            this.UI_label_OpacityValue.TabIndex = 6;
            this.UI_label_OpacityValue.Text = "255";
            // 
            // ColourThickness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 149);
            this.ControlBox = false;
            this.Controls.Add(this.UI_label_OpacityValue);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.UI_label_OpacityText);
            this.Controls.Add(this.UI_button_ColourSelect);
            this.Controls.Add(this.UI_label_ThickNumber);
            this.Controls.Add(this.UI_trackBar_Thickness);
            this.Controls.Add(this.UI_label_Thickness);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(300, 138);
            this.Name = "ColourThickness";
            this.Text = "ColourThickness";
            ((System.ComponentModel.ISupportInitialize)(this.UI_trackBar_Thickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog UI_colorDialog;
        private System.Windows.Forms.Label UI_label_Thickness;
        private System.Windows.Forms.TrackBar UI_trackBar_Thickness;
        private System.Windows.Forms.Label UI_label_ThickNumber;
        private System.Windows.Forms.Button UI_button_ColourSelect;
        private System.Windows.Forms.Label UI_label_OpacityText;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label UI_label_OpacityValue;
    }
}