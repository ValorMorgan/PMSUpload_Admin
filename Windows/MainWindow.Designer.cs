﻿namespace PMSUpload_Admin
{
    partial class MainWindow
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
            this.DataTable = new System.Windows.Forms.DataGridView();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.RoundedBox1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Select_Button = new System.Windows.Forms.Button();
            this.Search_Panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.CurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable)).BeginInit();
            this.Search_Panel.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataTable
            // 
            this.DataTable.AllowUserToAddRows = false;
            this.DataTable.AllowUserToDeleteRows = false;
            this.DataTable.AllowUserToResizeRows = false;
            this.DataTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataTable.CausesValidation = false;
            this.DataTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.DataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataTable.Location = new System.Drawing.Point(12, 52);
            this.DataTable.MultiSelect = false;
            this.DataTable.Name = "DataTable";
            this.DataTable.ReadOnly = true;
            this.DataTable.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataTable.ShowCellToolTips = false;
            this.DataTable.Size = new System.Drawing.Size(760, 453);
            this.DataTable.TabIndex = 0;
            this.DataTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataTable_CellDoubleClick);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.RoundedBox1});
            this.shapeContainer1.Size = new System.Drawing.Size(784, 562);
            this.shapeContainer1.TabIndex = 2;
            this.shapeContainer1.TabStop = false;
            // 
            // RoundedBox1
            // 
            this.RoundedBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundedBox1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.RoundedBox1.CornerRadius = 10;
            this.RoundedBox1.FillColor = System.Drawing.Color.RoyalBlue;
            this.RoundedBox1.FillGradientColor = System.Drawing.Color.White;
            this.RoundedBox1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Vertical;
            this.RoundedBox1.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.RoundedBox1.Location = new System.Drawing.Point(6, 5);
            this.RoundedBox1.Name = "RoundedBox1";
            this.RoundedBox1.Size = new System.Drawing.Size(772, 544);
            // 
            // Select_Button
            // 
            this.Select_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Select_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Select_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Select_Button.Location = new System.Drawing.Point(205, 509);
            this.Select_Button.Name = "Select_Button";
            this.Select_Button.Size = new System.Drawing.Size(388, 26);
            this.Select_Button.TabIndex = 3;
            this.Select_Button.Text = "Edit";
            this.Select_Button.UseVisualStyleBackColor = true;
            this.Select_Button.Click += new System.EventHandler(this.Select_Button_Click);
            // 
            // Search_Panel
            // 
            this.Search_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_Panel.BackColor = System.Drawing.SystemColors.Control;
            this.Search_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Search_Panel.Controls.Add(this.button1);
            this.Search_Panel.Controls.Add(this.searchBox);
            this.Search_Panel.Location = new System.Drawing.Point(12, 12);
            this.Search_Panel.Name = "Search_Panel";
            this.Search_Panel.Size = new System.Drawing.Size(760, 34);
            this.Search_Panel.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.HideSelection = false;
            this.searchBox.Location = new System.Drawing.Point(84, 6);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(671, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.WordWrap = false;
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentUser});
            this.StatusBar.Location = new System.Drawing.Point(0, 540);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(784, 22);
            this.StatusBar.TabIndex = 6;
            this.StatusBar.Text = "StatusBar";
            // 
            // CurrentUser
            // 
            this.CurrentUser.BackColor = System.Drawing.SystemColors.Control;
            this.CurrentUser.Name = "CurrentUser";
            this.CurrentUser.Size = new System.Drawing.Size(80, 17);
            this.CurrentUser.Text = "MFBNTDOM\\";
            // 
            // MainWindow
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.Search_Panel);
            this.Controls.Add(this.Select_Button);
            this.Controls.Add(this.DataTable);
            this.Controls.Add(this.shapeContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PMS Upload - Admin";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable)).EndInit();
            this.Search_Panel.ResumeLayout(false);
            this.Search_Panel.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataTable;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape RoundedBox1;
        private System.Windows.Forms.Button Select_Button;
        private System.Windows.Forms.Panel Search_Panel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel CurrentUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn tcdCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvMPSequenceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mjpAlphaMajorPerilDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rvcReserveCategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lccAlphaCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn payTransactionCategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rcvTransactionCategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvAIA12DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvAIA34DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvAIA56DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvInsuranceLineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvRiskUnitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn polUnitLocationNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvLocationNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rsvInlandMarineUnitDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
    }
}

