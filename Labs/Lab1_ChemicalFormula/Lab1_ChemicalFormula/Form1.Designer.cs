namespace Lab1_ChemicalFormula
{
    partial class MainForm
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
            this.UI_dataGridView_Chemicals = new System.Windows.Forms.DataGridView();
            this.UI_button_SortName = new System.Windows.Forms.Button();
            this.UI_button_SingleCharacter = new System.Windows.Forms.Button();
            this.UI_button_SortAtomic = new System.Windows.Forms.Button();
            this.UI_label_ChemFormula = new System.Windows.Forms.Label();
            this.UI_textBox_ChemFormula = new System.Windows.Forms.TextBox();
            this.UI_label_MMass = new System.Windows.Forms.Label();
            this.UI_label_MassCalculation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView_Chemicals)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_dataGridView_Chemicals
            // 
            this.UI_dataGridView_Chemicals.AllowUserToOrderColumns = true;
            this.UI_dataGridView_Chemicals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_dataGridView_Chemicals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_dataGridView_Chemicals.Location = new System.Drawing.Point(13, 13);
            this.UI_dataGridView_Chemicals.Name = "UI_dataGridView_Chemicals";
            this.UI_dataGridView_Chemicals.Size = new System.Drawing.Size(480, 355);
            this.UI_dataGridView_Chemicals.TabIndex = 0;
            // 
            // UI_button_SortName
            // 
            this.UI_button_SortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_SortName.Location = new System.Drawing.Point(499, 12);
            this.UI_button_SortName.Name = "UI_button_SortName";
            this.UI_button_SortName.Size = new System.Drawing.Size(123, 23);
            this.UI_button_SortName.TabIndex = 1;
            this.UI_button_SortName.Text = "Sort by Name";
            this.UI_button_SortName.UseVisualStyleBackColor = true;
            this.UI_button_SortName.Click += new System.EventHandler(this.UI_button_SortName_Click);
            // 
            // UI_button_SingleCharacter
            // 
            this.UI_button_SingleCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_SingleCharacter.Location = new System.Drawing.Point(500, 42);
            this.UI_button_SingleCharacter.Name = "UI_button_SingleCharacter";
            this.UI_button_SingleCharacter.Size = new System.Drawing.Size(122, 23);
            this.UI_button_SingleCharacter.TabIndex = 2;
            this.UI_button_SingleCharacter.TabStop = false;
            this.UI_button_SingleCharacter.Text = "Single Character Symbols";
            this.UI_button_SingleCharacter.UseVisualStyleBackColor = true;
            this.UI_button_SingleCharacter.Click += new System.EventHandler(this.UI_button_SingleCharacter_Click);
            // 
            // UI_button_SortAtomic
            // 
            this.UI_button_SortAtomic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_button_SortAtomic.Location = new System.Drawing.Point(500, 72);
            this.UI_button_SortAtomic.Name = "UI_button_SortAtomic";
            this.UI_button_SortAtomic.Size = new System.Drawing.Size(122, 23);
            this.UI_button_SortAtomic.TabIndex = 3;
            this.UI_button_SortAtomic.Text = "Sort by Atomic #";
            this.UI_button_SortAtomic.UseVisualStyleBackColor = true;
            this.UI_button_SortAtomic.Click += new System.EventHandler(this.UI_button_SortAtomic_Click);
            // 
            // UI_label_ChemFormula
            // 
            this.UI_label_ChemFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_label_ChemFormula.AutoSize = true;
            this.UI_label_ChemFormula.Location = new System.Drawing.Point(12, 381);
            this.UI_label_ChemFormula.Name = "UI_label_ChemFormula";
            this.UI_label_ChemFormula.Size = new System.Drawing.Size(90, 13);
            this.UI_label_ChemFormula.TabIndex = 6;
            this.UI_label_ChemFormula.Text = "Chemical Formula";
            // 
            // UI_textBox_ChemFormula
            // 
            this.UI_textBox_ChemFormula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_textBox_ChemFormula.Location = new System.Drawing.Point(108, 379);
            this.UI_textBox_ChemFormula.Name = "UI_textBox_ChemFormula";
            this.UI_textBox_ChemFormula.Size = new System.Drawing.Size(190, 20);
            this.UI_textBox_ChemFormula.TabIndex = 4;
            this.UI_textBox_ChemFormula.TextChanged += new System.EventHandler(this.UI_textBox_ChemFormula_TextChanged);
            // 
            // UI_label_MMass
            // 
            this.UI_label_MMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_label_MMass.AutoSize = true;
            this.UI_label_MMass.Location = new System.Drawing.Point(393, 381);
            this.UI_label_MMass.Name = "UI_label_MMass";
            this.UI_label_MMass.Size = new System.Drawing.Size(100, 13);
            this.UI_label_MMass.TabIndex = 7;
            this.UI_label_MMass.Text = "Approx. Molar Mass";
            // 
            // UI_label_MassCalculation
            // 
            this.UI_label_MassCalculation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_label_MassCalculation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UI_label_MassCalculation.Location = new System.Drawing.Point(499, 376);
            this.UI_label_MassCalculation.Name = "UI_label_MassCalculation";
            this.UI_label_MassCalculation.Size = new System.Drawing.Size(123, 23);
            this.UI_label_MassCalculation.TabIndex = 5;
            this.UI_label_MassCalculation.Text = "0 g/mol";
            this.UI_label_MassCalculation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.UI_label_MassCalculation);
            this.Controls.Add(this.UI_label_MMass);
            this.Controls.Add(this.UI_textBox_ChemFormula);
            this.Controls.Add(this.UI_label_ChemFormula);
            this.Controls.Add(this.UI_button_SortAtomic);
            this.Controls.Add(this.UI_button_SingleCharacter);
            this.Controls.Add(this.UI_button_SortName);
            this.Controls.Add(this.UI_dataGridView_Chemicals);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "MainForm";
            this.Text = "Lab 1 - Chemical Formula";
            ((System.ComponentModel.ISupportInitialize)(this.UI_dataGridView_Chemicals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UI_dataGridView_Chemicals;
        private System.Windows.Forms.Button UI_button_SortName;
        private System.Windows.Forms.Button UI_button_SingleCharacter;
        private System.Windows.Forms.Button UI_button_SortAtomic;
        private System.Windows.Forms.Label UI_label_ChemFormula;
        private System.Windows.Forms.TextBox UI_textBox_ChemFormula;
        private System.Windows.Forms.Label UI_label_MMass;
        private System.Windows.Forms.Label UI_label_MassCalculation;
    }
}

