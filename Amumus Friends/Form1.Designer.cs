namespace Amumus_Friends
{
    partial class amumus_friends
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(amumus_friends));
            exportBtn = new MaterialSkin.Controls.MaterialButton();
            importBtn = new MaterialSkin.Controls.MaterialButton();
            deleteAllBtn = new MaterialSkin.Controls.MaterialButton();
            statusLabel = new MaterialSkin.Controls.MaterialLabel();
            creditLabel = new MaterialSkin.Controls.MaterialLabel();
            exportsListBox = new ListBox();
            SuspendLayout();
            // 
            // exportBtn
            // 
            exportBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            exportBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            exportBtn.Depth = 0;
            exportBtn.HighEmphasis = true;
            exportBtn.Icon = null;
            exportBtn.Location = new Point(168, 78);
            exportBtn.Margin = new Padding(4, 6, 4, 6);
            exportBtn.MouseState = MaterialSkin.MouseState.HOVER;
            exportBtn.Name = "exportBtn";
            exportBtn.NoAccentTextColor = Color.Empty;
            exportBtn.Size = new Size(169, 36);
            exportBtn.TabIndex = 0;
            exportBtn.Text = "Export friendslist";
            exportBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            exportBtn.UseAccentColor = false;
            exportBtn.UseVisualStyleBackColor = true;
            exportBtn.Click += exportBtn_Click;
            // 
            // importBtn
            // 
            importBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            importBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            importBtn.Depth = 0;
            importBtn.HighEmphasis = true;
            importBtn.Icon = null;
            importBtn.Location = new Point(7, 78);
            importBtn.Margin = new Padding(4, 6, 4, 6);
            importBtn.MouseState = MaterialSkin.MouseState.HOVER;
            importBtn.Name = "importBtn";
            importBtn.NoAccentTextColor = Color.Empty;
            importBtn.Size = new Size(153, 36);
            importBtn.TabIndex = 1;
            importBtn.Text = "Import from file";
            importBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            importBtn.UseAccentColor = false;
            importBtn.UseVisualStyleBackColor = true;
            importBtn.Click += importBtn_Click;
            // 
            // deleteAllBtn
            // 
            deleteAllBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            deleteAllBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            deleteAllBtn.Depth = 0;
            deleteAllBtn.HighEmphasis = true;
            deleteAllBtn.Icon = null;
            deleteAllBtn.Location = new Point(345, 78);
            deleteAllBtn.Margin = new Padding(4, 6, 4, 6);
            deleteAllBtn.MouseState = MaterialSkin.MouseState.HOVER;
            deleteAllBtn.Name = "deleteAllBtn";
            deleteAllBtn.NoAccentTextColor = Color.Empty;
            deleteAllBtn.Size = new Size(166, 36);
            deleteAllBtn.TabIndex = 2;
            deleteAllBtn.Text = "Delete all Friends";
            deleteAllBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            deleteAllBtn.UseAccentColor = false;
            deleteAllBtn.UseVisualStyleBackColor = true;
            deleteAllBtn.Click += deleteAllBtn_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Depth = 0;
            statusLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            statusLabel.Location = new Point(6, 237);
            statusLabel.MouseState = MaterialSkin.MouseState.HOVER;
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(154, 19);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "LCU Status: Unknown";
            // 
            // creditLabel
            // 
            creditLabel.AutoSize = true;
            creditLabel.Depth = 0;
            creditLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            creditLabel.Location = new Point(413, 237);
            creditLabel.MouseState = MaterialSkin.MouseState.HOVER;
            creditLabel.Name = "creditLabel";
            creditLabel.Size = new Size(97, 19);
            creditLabel.TabIndex = 5;
            creditLabel.Text = "Made by Rico";
            // 
            // exportsListBox
            // 
            exportsListBox.FormattingEnabled = true;
            exportsListBox.ItemHeight = 15;
            exportsListBox.Location = new Point(7, 123);
            exportsListBox.Name = "exportsListBox";
            exportsListBox.Size = new Size(153, 94);
            exportsListBox.TabIndex = 6;
            exportsListBox.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // amumus_friends
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(518, 262);
            Controls.Add(exportsListBox);
            Controls.Add(creditLabel);
            Controls.Add(statusLabel);
            Controls.Add(deleteAllBtn);
            Controls.Add(importBtn);
            Controls.Add(exportBtn);
            FormStyle = FormStyles.ActionBar_48;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "amumus_friends";
            Padding = new Padding(3, 72, 3, 3);
            Text = "Amumus Friends";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton exportBtn;
        private MaterialSkin.Controls.MaterialButton importBtn;
        private MaterialSkin.Controls.MaterialButton deleteAllBtn;
        private MaterialSkin.Controls.MaterialLabel statusLabel;
        private MaterialSkin.Controls.MaterialLabel creditLabel;
        private ListBox exportsListBox;
    }
}