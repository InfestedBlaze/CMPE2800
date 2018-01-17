namespace ICA2_NicW
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
            this.UI_button_Read = new System.Windows.Forms.Button();
            this.UI_label = new System.Windows.Forms.Label();
            this.UI_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // UI_button_Read
            // 
            this.UI_button_Read.Location = new System.Drawing.Point(13, 13);
            this.UI_button_Read.Name = "UI_button_Read";
            this.UI_button_Read.Size = new System.Drawing.Size(259, 23);
            this.UI_button_Read.TabIndex = 0;
            this.UI_button_Read.Text = "Read from file";
            this.UI_button_Read.UseVisualStyleBackColor = true;
            this.UI_button_Read.Click += new System.EventHandler(this.UI_button_Read_Click);
            // 
            // UI_label
            // 
            this.UI_label.AutoSize = true;
            this.UI_label.Location = new System.Drawing.Point(13, 43);
            this.UI_label.Name = "UI_label";
            this.UI_label.Size = new System.Drawing.Size(0, 13);
            this.UI_label.TabIndex = 1;
            // 
            // UI_openFileDialog
            // 
            this.UI_openFileDialog.FileName = "openFileDialog1";
            this.UI_openFileDialog.Filter = "Text Files|*.txt|All Files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.UI_label);
            this.Controls.Add(this.UI_button_Read);
            this.Name = "Form1";
            this.Text = "ICA 2: var";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_button_Read;
        private System.Windows.Forms.Label UI_label;
        private System.Windows.Forms.OpenFileDialog UI_openFileDialog;
    }
}

