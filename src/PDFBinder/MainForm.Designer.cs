namespace PDFBinder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetPageRange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.addFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.helpLabel = new System.Windows.Forms.Label();
            this.addFileButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.moveUpButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownButton = new System.Windows.Forms.ToolStripButton();
            this.completeButton = new System.Windows.Forms.ToolStripButton();
            this.showNameButton = new System.Windows.Forms.ToolStripButton();
            this.sortButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileListBox
            // 
            this.FileListBox.AllowDrop = true;
            this.FileListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.FileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.IntegralHeight = false;
            this.FileListBox.ItemHeight = 24;
            this.FileListBox.Location = new System.Drawing.Point(4, 28);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileListBox.Size = new System.Drawing.Size(623, 248);
            this.FileListBox.TabIndex = 0;
            this.FileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FileListBox_DrawItem);
            this.FileListBox.SelectedIndexChanged += new System.EventHandler(this.FileListBox_SelectedIndexChanged);
            this.FileListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileListBox_DragDrop);
            this.FileListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileListBox_DragEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetPageRange,
            this.toolStripMenuItem1,
            this.mnuClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 54);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mnuSetPageRange
            // 
            this.mnuSetPageRange.Name = "mnuSetPageRange";
            this.mnuSetPageRange.Size = new System.Drawing.Size(164, 22);
            this.mnuSetPageRange.Text = "设置页码范围(&P)";
            this.mnuSetPageRange.ToolTipText = "设置页码范围";
            this.mnuSetPageRange.Click += new System.EventHandler(this.mnuSetPageRange_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuClear
            // 
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(164, 22);
            this.mnuClear.Text = "移除所有文件(&R)";
            this.mnuClear.ToolTipText = "移除所有文件(&R)";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileButton,
            this.removeButton,
            this.toolStripSeparator1,
            this.moveUpButton,
            this.moveDownButton,
            this.toolStripSeparator2,
            this.completeButton,
            this.showNameButton,
            this.sortButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(630, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pdf";
            this.saveFileDialog.Filter = "PDF documents|*.pdf|All files|*.*";
            // 
            // addFileDialog
            // 
            this.addFileDialog.DefaultExt = "pdf";
            this.addFileDialog.Filter = "PDF documents|*.pdf";
            this.addFileDialog.Multiselect = true;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(4, 284);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(623, 15);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // helpLabel
            // 
            this.helpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpLabel.AutoSize = true;
            this.helpLabel.Location = new System.Drawing.Point(4, 284);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(0, 12);
            this.helpLabel.TabIndex = 3;
            // 
            // addFileButton
            // 
            this.addFileButton.Image = global::PDFBinder.Properties.Resources.add;
            this.addFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Size = new System.Drawing.Size(85, 22);
            this.addFileButton.Text = "添加文档...";
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeButton.Enabled = false;
            this.removeButton.Image = global::PDFBinder.Properties.Resources.del;
            this.removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(23, 22);
            this.removeButton.Text = "移除选择";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpButton.Enabled = false;
            this.moveUpButton.Image = global::PDFBinder.Properties.Resources.up;
            this.moveUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(23, 22);
            this.moveUpButton.Text = "上移";
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownButton.Enabled = false;
            this.moveDownButton.Image = global::PDFBinder.Properties.Resources.down;
            this.moveDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(23, 22);
            this.moveDownButton.Text = "下移";
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // completeButton
            // 
            this.completeButton.Enabled = false;
            this.completeButton.Image = ((System.Drawing.Image)(resources.GetObject("completeButton.Image")));
            this.completeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.completeButton.Name = "completeButton";
            this.completeButton.Size = new System.Drawing.Size(76, 22);
            this.completeButton.Text = "合并文档";
            this.completeButton.Click += new System.EventHandler(this.combineButton_Click);
            // 
            // showNameButton
            // 
            this.showNameButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showNameButton.CheckOnClick = true;
            this.showNameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showNameButton.Image = ((System.Drawing.Image)(resources.GetObject("showNameButton.Image")));
            this.showNameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showNameButton.Name = "showNameButton";
            this.showNameButton.Size = new System.Drawing.Size(24, 22);
            this.showNameButton.Text = "简";
            this.showNameButton.Click += new System.EventHandler(this.showNameButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sortButton.Image = global::PDFBinder.Properties.Resources.sortNone;
            this.sortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(23, 22);
            this.sortButton.Tag = "0";
            this.sortButton.Text = "排序";
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 301);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.FileListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(437, 214);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF合并器v2.0";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FileListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addFileButton;
        private System.Windows.Forms.ToolStripButton moveUpButton;
        private System.Windows.Forms.ToolStripButton moveDownButton;
        private System.Windows.Forms.ToolStripButton removeButton;
        private System.Windows.Forms.ToolStripButton completeButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog addFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.ToolStripButton showNameButton;
        private System.Windows.Forms.ToolStripButton sortButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSetPageRange;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
    }
}