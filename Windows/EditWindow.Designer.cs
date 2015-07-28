namespace PMSUpload_Admin
{
    partial class EditWindow
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
            this.RoundedBox1 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ClaimForm = new System.Windows.Forms.TableLayoutPanel();
            this.SaveClaim_Button = new System.Windows.Forms.Button();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.RoundedBox1.Size = new System.Drawing.Size(572, 473);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.RoundedBox1});
            this.shapeContainer1.Size = new System.Drawing.Size(584, 462);
            this.shapeContainer1.TabIndex = 0;
            this.shapeContainer1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ClaimForm);
            this.panel1.Location = new System.Drawing.Point(12, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 396);
            this.panel1.TabIndex = 1;
            // 
            // ClaimForm
            // 
            this.ClaimForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClaimForm.ColumnCount = 3;
            this.ClaimForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ClaimForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ClaimForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ClaimForm.Location = new System.Drawing.Point(3, 3);
            this.ClaimForm.Name = "ClaimForm";
            this.ClaimForm.RowCount = 1;
            this.ClaimForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 386F));
            this.ClaimForm.Size = new System.Drawing.Size(530, 386);
            this.ClaimForm.TabIndex = 0;
            // 
            // SaveClaim_Button
            // 
            this.SaveClaim_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveClaim_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SaveClaim_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveClaim_Button.Location = new System.Drawing.Point(478, 432);
            this.SaveClaim_Button.Name = "SaveClaim_Button";
            this.SaveClaim_Button.Size = new System.Drawing.Size(94, 26);
            this.SaveClaim_Button.TabIndex = 2;
            this.SaveClaim_Button.Text = "Save";
            this.SaveClaim_Button.UseVisualStyleBackColor = true;
            this.SaveClaim_Button.Click += new System.EventHandler(this.SaveClaim_Button_Click);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_Button.Location = new System.Drawing.Point(12, 432);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(94, 26);
            this.Cancel_Button.TabIndex = 3;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // EditWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.SaveClaim_Button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.shapeContainer1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "EditWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Claim - ";
            this.Load += new System.EventHandler(this.EditWindow_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.RectangleShape RoundedBox1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SaveClaim_Button;
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.TableLayoutPanel ClaimForm;
    }
}