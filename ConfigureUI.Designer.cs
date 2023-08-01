namespace SheetList
{
    partial class ConfigureUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureUI));
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.ckb_ShowTitle = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txb_SheetNameColName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txb_SheetNoColName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rad_ColHeadingHide = new System.Windows.Forms.RadioButton();
            this.rad_ColHeadingBtm = new System.Windows.Forms.RadioButton();
            this.rad_ColHeadingTop = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rad_DirectionTop = new System.Windows.Forms.RadioButton();
            this.rad_DirectionBtm = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Title = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnl_AutoWrap = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rad_NumberOfSections = new System.Windows.Forms.RadioButton();
            this.rad_MaxRows = new System.Windows.Forms.RadioButton();
            this.txb_SectionNumber = new System.Windows.Forms.TextBox();
            this.txb_MaxRows = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rad_WrapDirectionRight = new System.Windows.Forms.RadioButton();
            this.rad_WrapDirectionLeft = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ckb_EnableAutoWrap = new System.Windows.Forms.CheckBox();
            this.ckb_UpdateBeforeSave = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnl_AutoWrap.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Location = new System.Drawing.Point(4, 4);
            this.btn_SaveSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(141, 28);
            this.btn_SaveSettings.TabIndex = 0;
            this.btn_SaveSettings.Text = "Save Settings";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.btn_SaveSettings_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(153, 4);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(141, 28);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // ckb_ShowTitle
            // 
            this.ckb_ShowTitle.AutoSize = true;
            this.ckb_ShowTitle.Checked = true;
            this.ckb_ShowTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_ShowTitle.Location = new System.Drawing.Point(21, 38);
            this.ckb_ShowTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckb_ShowTitle.Name = "ckb_ShowTitle";
            this.ckb_ShowTitle.Size = new System.Drawing.Size(95, 21);
            this.ckb_ShowTitle.TabIndex = 2;
            this.ckb_ShowTitle.Text = "Show Title";
            this.ckb_ShowTitle.UseVisualStyleBackColor = true;
            this.ckb_ShowTitle.CheckedChanged += new System.EventHandler(this.ckb_ShowTitle_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txb_SheetNameColName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txb_SheetNoColName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txb_Title);
            this.groupBox1.Controls.Add(this.ckb_ShowTitle);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(267, 447);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Headings and Sheet List Settings";
            // 
            // txb_SheetNameColName
            // 
            this.txb_SheetNameColName.Location = new System.Drawing.Point(21, 396);
            this.txb_SheetNameColName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb_SheetNameColName.Name = "txb_SheetNameColName";
            this.txb_SheetNameColName.Size = new System.Drawing.Size(187, 22);
            this.txb_SheetNameColName.TabIndex = 11;
            this.txb_SheetNameColName.Text = "SHEET NAME";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 377);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sheet Name Column Name";
            // 
            // txb_SheetNoColName
            // 
            this.txb_SheetNoColName.Location = new System.Drawing.Point(21, 338);
            this.txb_SheetNoColName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb_SheetNoColName.Name = "txb_SheetNoColName";
            this.txb_SheetNoColName.Size = new System.Drawing.Size(187, 22);
            this.txb_SheetNoColName.TabIndex = 9;
            this.txb_SheetNoColName.Text = "SHEET NO.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 319);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sheet Number Column Name";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rad_ColHeadingHide);
            this.panel4.Controls.Add(this.rad_ColHeadingBtm);
            this.panel4.Controls.Add(this.rad_ColHeadingTop);
            this.panel4.Location = new System.Drawing.Point(21, 217);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 92);
            this.panel4.TabIndex = 7;
            // 
            // rad_ColHeadingHide
            // 
            this.rad_ColHeadingHide.AutoSize = true;
            this.rad_ColHeadingHide.Location = new System.Drawing.Point(4, 60);
            this.rad_ColHeadingHide.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_ColHeadingHide.Name = "rad_ColHeadingHide";
            this.rad_ColHeadingHide.Size = new System.Drawing.Size(58, 21);
            this.rad_ColHeadingHide.TabIndex = 8;
            this.rad_ColHeadingHide.TabStop = true;
            this.rad_ColHeadingHide.Text = "Hide";
            this.rad_ColHeadingHide.UseVisualStyleBackColor = true;
            // 
            // rad_ColHeadingBtm
            // 
            this.rad_ColHeadingBtm.AutoSize = true;
            this.rad_ColHeadingBtm.Location = new System.Drawing.Point(4, 32);
            this.rad_ColHeadingBtm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_ColHeadingBtm.Name = "rad_ColHeadingBtm";
            this.rad_ColHeadingBtm.Size = new System.Drawing.Size(73, 21);
            this.rad_ColHeadingBtm.TabIndex = 1;
            this.rad_ColHeadingBtm.TabStop = true;
            this.rad_ColHeadingBtm.Text = "Bottom";
            this.rad_ColHeadingBtm.UseVisualStyleBackColor = true;
            // 
            // rad_ColHeadingTop
            // 
            this.rad_ColHeadingTop.AutoSize = true;
            this.rad_ColHeadingTop.Checked = true;
            this.rad_ColHeadingTop.Location = new System.Drawing.Point(4, 4);
            this.rad_ColHeadingTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_ColHeadingTop.Name = "rad_ColHeadingTop";
            this.rad_ColHeadingTop.Size = new System.Drawing.Size(54, 21);
            this.rad_ColHeadingTop.TabIndex = 0;
            this.rad_ColHeadingTop.TabStop = true;
            this.rad_ColHeadingTop.Text = "Top";
            this.rad_ColHeadingTop.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 197);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Column Heading Placement";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rad_DirectionTop);
            this.panel1.Controls.Add(this.rad_DirectionBtm);
            this.panel1.Location = new System.Drawing.Point(21, 124);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 59);
            this.panel1.TabIndex = 5;
            // 
            // rad_DirectionTop
            // 
            this.rad_DirectionTop.AutoSize = true;
            this.rad_DirectionTop.Location = new System.Drawing.Point(4, 32);
            this.rad_DirectionTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_DirectionTop.Name = "rad_DirectionTop";
            this.rad_DirectionTop.Size = new System.Drawing.Size(142, 21);
            this.rad_DirectionTop.TabIndex = 1;
            this.rad_DirectionTop.TabStop = true;
            this.rad_DirectionTop.Text = "Add Rows To Top";
            this.rad_DirectionTop.UseVisualStyleBackColor = true;
            // 
            // rad_DirectionBtm
            // 
            this.rad_DirectionBtm.AutoSize = true;
            this.rad_DirectionBtm.Checked = true;
            this.rad_DirectionBtm.Location = new System.Drawing.Point(4, 4);
            this.rad_DirectionBtm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_DirectionBtm.Name = "rad_DirectionBtm";
            this.rad_DirectionBtm.Size = new System.Drawing.Size(156, 21);
            this.rad_DirectionBtm.TabIndex = 0;
            this.rad_DirectionBtm.TabStop = true;
            this.rad_DirectionBtm.Text = "Add Rows to Bottom";
            this.rad_DirectionBtm.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Direction";
            // 
            // txb_Title
            // 
            this.txb_Title.Location = new System.Drawing.Point(21, 66);
            this.txb_Title.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb_Title.Name = "txb_Title";
            this.txb_Title.Size = new System.Drawing.Size(164, 22);
            this.txb_Title.TabIndex = 3;
            this.txb_Title.Text = "SHEET LIST";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btn_SaveSettings);
            this.panel3.Controls.Add(this.btn_Cancel);
            this.panel3.Location = new System.Drawing.Point(268, 479);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 34);
            this.panel3.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnl_AutoWrap);
            this.groupBox2.Controls.Add(this.ckb_EnableAutoWrap);
            this.groupBox2.Location = new System.Drawing.Point(291, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(284, 213);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Table Wrapping";
            // 
            // pnl_AutoWrap
            // 
            this.pnl_AutoWrap.Controls.Add(this.panel6);
            this.pnl_AutoWrap.Controls.Add(this.panel5);
            this.pnl_AutoWrap.Controls.Add(this.label3);
            this.pnl_AutoWrap.Enabled = false;
            this.pnl_AutoWrap.Location = new System.Drawing.Point(8, 64);
            this.pnl_AutoWrap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_AutoWrap.Name = "pnl_AutoWrap";
            this.pnl_AutoWrap.Size = new System.Drawing.Size(269, 142);
            this.pnl_AutoWrap.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rad_NumberOfSections);
            this.panel6.Controls.Add(this.rad_MaxRows);
            this.panel6.Controls.Add(this.txb_SectionNumber);
            this.panel6.Controls.Add(this.txb_MaxRows);
            this.panel6.Location = new System.Drawing.Point(4, 64);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(259, 70);
            this.panel6.TabIndex = 2;
            // 
            // rad_NumberOfSections
            // 
            this.rad_NumberOfSections.AutoSize = true;
            this.rad_NumberOfSections.Checked = true;
            this.rad_NumberOfSections.Location = new System.Drawing.Point(9, 37);
            this.rad_NumberOfSections.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_NumberOfSections.Name = "rad_NumberOfSections";
            this.rad_NumberOfSections.Size = new System.Drawing.Size(156, 21);
            this.rad_NumberOfSections.TabIndex = 7;
            this.rad_NumberOfSections.TabStop = true;
            this.rad_NumberOfSections.Text = "Number Of Sections";
            this.rad_NumberOfSections.UseVisualStyleBackColor = true;
            this.rad_NumberOfSections.CheckedChanged += new System.EventHandler(this.rad_NumberOfSections_CheckedChanged);
            // 
            // rad_MaxRows
            // 
            this.rad_MaxRows.AutoSize = true;
            this.rad_MaxRows.Location = new System.Drawing.Point(9, 5);
            this.rad_MaxRows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_MaxRows.Name = "rad_MaxRows";
            this.rad_MaxRows.Size = new System.Drawing.Size(125, 21);
            this.rad_MaxRows.TabIndex = 6;
            this.rad_MaxRows.Text = "Maximum Rows";
            this.rad_MaxRows.UseVisualStyleBackColor = true;
            this.rad_MaxRows.CheckedChanged += new System.EventHandler(this.rad_MaxRows_CheckedChanged);
            // 
            // txb_SectionNumber
            // 
            this.txb_SectionNumber.Location = new System.Drawing.Point(176, 36);
            this.txb_SectionNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb_SectionNumber.Name = "txb_SectionNumber";
            this.txb_SectionNumber.Size = new System.Drawing.Size(69, 22);
            this.txb_SectionNumber.TabIndex = 7;
            this.txb_SectionNumber.Text = "1";
            // 
            // txb_MaxRows
            // 
            this.txb_MaxRows.Enabled = false;
            this.txb_MaxRows.Location = new System.Drawing.Point(176, 4);
            this.txb_MaxRows.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txb_MaxRows.Name = "txb_MaxRows";
            this.txb_MaxRows.Size = new System.Drawing.Size(69, 22);
            this.txb_MaxRows.TabIndex = 6;
            this.txb_MaxRows.Text = "10";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rad_WrapDirectionRight);
            this.panel5.Controls.Add(this.rad_WrapDirectionLeft);
            this.panel5.Location = new System.Drawing.Point(4, 32);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(189, 27);
            this.panel5.TabIndex = 1;
            // 
            // rad_WrapDirectionRight
            // 
            this.rad_WrapDirectionRight.AutoSize = true;
            this.rad_WrapDirectionRight.Checked = true;
            this.rad_WrapDirectionRight.Location = new System.Drawing.Point(89, 4);
            this.rad_WrapDirectionRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_WrapDirectionRight.Name = "rad_WrapDirectionRight";
            this.rad_WrapDirectionRight.Size = new System.Drawing.Size(62, 21);
            this.rad_WrapDirectionRight.TabIndex = 1;
            this.rad_WrapDirectionRight.TabStop = true;
            this.rad_WrapDirectionRight.Text = "Right";
            this.rad_WrapDirectionRight.UseVisualStyleBackColor = true;
            // 
            // rad_WrapDirectionLeft
            // 
            this.rad_WrapDirectionLeft.AutoSize = true;
            this.rad_WrapDirectionLeft.Location = new System.Drawing.Point(9, 4);
            this.rad_WrapDirectionLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rad_WrapDirectionLeft.Name = "rad_WrapDirectionLeft";
            this.rad_WrapDirectionLeft.Size = new System.Drawing.Size(53, 21);
            this.rad_WrapDirectionLeft.TabIndex = 0;
            this.rad_WrapDirectionLeft.Text = "Left";
            this.rad_WrapDirectionLeft.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Direction to Wrap Table";
            // 
            // ckb_EnableAutoWrap
            // 
            this.ckb_EnableAutoWrap.AutoSize = true;
            this.ckb_EnableAutoWrap.Location = new System.Drawing.Point(21, 36);
            this.ckb_EnableAutoWrap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckb_EnableAutoWrap.Name = "ckb_EnableAutoWrap";
            this.ckb_EnableAutoWrap.Size = new System.Drawing.Size(178, 21);
            this.ckb_EnableAutoWrap.TabIndex = 0;
            this.ckb_EnableAutoWrap.Text = "Enable Automatic Wrap";
            this.ckb_EnableAutoWrap.UseVisualStyleBackColor = true;
            this.ckb_EnableAutoWrap.CheckedChanged += new System.EventHandler(this.ckb_EnableAutoWrap_CheckedChanged);
            // 
            // ckb_UpdateBeforeSave
            // 
            this.ckb_UpdateBeforeSave.AutoSize = true;
            this.ckb_UpdateBeforeSave.Location = new System.Drawing.Point(16, 488);
            this.ckb_UpdateBeforeSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckb_UpdateBeforeSave.Name = "ckb_UpdateBeforeSave";
            this.ckb_UpdateBeforeSave.Size = new System.Drawing.Size(225, 21);
            this.ckb_UpdateBeforeSave.TabIndex = 6;
            this.ckb_UpdateBeforeSave.Text = "Update Sheet List Before Save";
            this.ckb_UpdateBeforeSave.UseVisualStyleBackColor = true;
            // 
            // ConfigureUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 528);
            this.Controls.Add(this.ckb_UpdateBeforeSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ConfigureUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sheet List - Configure";
            this.Load += new System.EventHandler(this.ConfigureUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnl_AutoWrap.ResumeLayout(false);
            this.pnl_AutoWrap.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SaveSettings;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox ckb_ShowTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rad_DirectionTop;
        private System.Windows.Forms.RadioButton rad_DirectionBtm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Title;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rad_ColHeadingHide;
        private System.Windows.Forms.RadioButton rad_ColHeadingBtm;
        private System.Windows.Forms.RadioButton rad_ColHeadingTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckb_EnableAutoWrap;
        private System.Windows.Forms.Panel pnl_AutoWrap;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txb_SectionNumber;
        private System.Windows.Forms.TextBox txb_MaxRows;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rad_WrapDirectionRight;
        private System.Windows.Forms.RadioButton rad_WrapDirectionLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_SheetNameColName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_SheetNoColName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rad_NumberOfSections;
        private System.Windows.Forms.RadioButton rad_MaxRows;
		private System.Windows.Forms.CheckBox ckb_UpdateBeforeSave;
	}
}