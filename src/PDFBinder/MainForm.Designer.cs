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
            this.showNameButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.addFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.helpLabel = new System.Windows.Forms.Label();
            this.addFileButton = new System.Windows.Forms.ToolStripButton();
            this.addBookmarkButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.moveUpButton = new System.Windows.Forms.ToolStripButton();
            this.moveDownButton = new System.Windows.Forms.ToolStripButton();
            this.completeButton = new System.Windows.Forms.ToolStripButton();
            this.sortButton = new System.Windows.Forms.ToolStripButton();
            this.PageSizeButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuPageSize_Original = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPageSize_A4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPageSize_B4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPageSize_A5 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileListBox
            // 
            resources.ApplyResources(this.FileListBox, "FileListBox");
            this.FileListBox.AllowDrop = true;
            this.FileListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.FileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FileListBox_DrawItem);
            this.FileListBox.SelectedIndexChanged += new System.EventHandler(this.FileListBox_SelectedIndexChanged);
            this.FileListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileListBox_DragDrop);
            this.FileListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileListBox_DragEnter);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetPageRange,
            this.toolStripMenuItem1,
            this.mnuClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // mnuSetPageRange
            // 
            resources.ApplyResources(this.mnuSetPageRange, "mnuSetPageRange");
            this.mnuSetPageRange.Name = "mnuSetPageRange";
            this.mnuSetPageRange.Click += new System.EventHandler(this.mnuSetPageRange_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // mnuClear
            // 
            resources.ApplyResources(this.mnuClear, "mnuClear");
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileButton,
            this.addBookmarkButton,
            this.removeButton,
            this.toolStripSeparator1,
            this.moveUpButton,
            this.moveDownButton,
            this.toolStripSeparator2,
            this.completeButton,
            this.showNameButton,
            this.sortButton,
            this.PageSizeButton});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // showNameButton
            // 
            resources.ApplyResources(this.showNameButton, "showNameButton");
            this.showNameButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.showNameButton.CheckOnClick = true;
            this.showNameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showNameButton.Name = "showNameButton";
            this.showNameButton.Click += new System.EventHandler(this.showNameButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pdf";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // addFileDialog
            // 
            this.addFileDialog.DefaultExt = "pdf";
            resources.ApplyResources(this.addFileDialog, "addFileDialog");
            this.addFileDialog.Multiselect = true;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // helpLabel
            // 
            resources.ApplyResources(this.helpLabel, "helpLabel");
            this.helpLabel.Name = "helpLabel";
            // 
            // addFileButton
            // 
            resources.ApplyResources(this.addFileButton, "addFileButton");
            this.addFileButton.Image = global::PDFBinder.Properties.Resources.add;
            this.addFileButton.Name = "addFileButton";
            this.addFileButton.Click += new System.EventHandler(this.addFileButton_Click);
            // 
            // addBookmarkButton
            // 
            resources.ApplyResources(this.addBookmarkButton, "addBookmarkButton");
            this.addBookmarkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addBookmarkButton.Image = global::PDFBinder.Properties.Resources.bookmark;
            this.addBookmarkButton.Name = "addBookmarkButton";
            this.addBookmarkButton.Click += new System.EventHandler(this.addBookmarkButton_Click);
            // 
            // removeButton
            // 
            resources.ApplyResources(this.removeButton, "removeButton");
            this.removeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeButton.Image = global::PDFBinder.Properties.Resources.del;
            this.removeButton.Name = "removeButton";
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // moveUpButton
            // 
            resources.ApplyResources(this.moveUpButton, "moveUpButton");
            this.moveUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveUpButton.Image = global::PDFBinder.Properties.Resources.up;
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
            // 
            // moveDownButton
            // 
            resources.ApplyResources(this.moveDownButton, "moveDownButton");
            this.moveDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moveDownButton.Image = global::PDFBinder.Properties.Resources.down;
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // completeButton
            // 
            resources.ApplyResources(this.completeButton, "completeButton");
            this.completeButton.Image = global::PDFBinder.Properties.Resources.export;
            this.completeButton.Name = "completeButton";
            this.completeButton.Click += new System.EventHandler(this.combineButton_Click);
            // 
            // sortButton
            // 
            resources.ApplyResources(this.sortButton, "sortButton");
            this.sortButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sortButton.Image = global::PDFBinder.Properties.Resources.sortNone;
            this.sortButton.Name = "sortButton";
            this.sortButton.Tag = "0";
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // PageSizeButton
            // 
            resources.ApplyResources(this.PageSizeButton, "PageSizeButton");
            this.PageSizeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PageSizeButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPageSize_Original,
            this.mnuPageSize_A4,
            this.mnuPageSize_B4,
            this.mnuPageSize_A5});
            this.PageSizeButton.Name = "PageSizeButton";
            // 
            // mnuPageSize_Original
            // 
            resources.ApplyResources(this.mnuPageSize_Original, "mnuPageSize_Original");
            this.mnuPageSize_Original.Checked = true;
            this.mnuPageSize_Original.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuPageSize_Original.Name = "mnuPageSize_Original";
            this.mnuPageSize_Original.Click += new System.EventHandler(this.OnPageSizeChanged);
            // 
            // mnuPageSize_A4
            // 
            resources.ApplyResources(this.mnuPageSize_A4, "mnuPageSize_A4");
            this.mnuPageSize_A4.Name = "mnuPageSize_A4";
            this.mnuPageSize_A4.Click += new System.EventHandler(this.OnPageSizeChanged);
            // 
            // mnuPageSize_B4
            // 
            resources.ApplyResources(this.mnuPageSize_B4, "mnuPageSize_B4");
            this.mnuPageSize_B4.Name = "mnuPageSize_B4";
            this.mnuPageSize_B4.Click += new System.EventHandler(this.OnPageSizeChanged);
            // 
            // mnuPageSize_A5
            // 
            resources.ApplyResources(this.mnuPageSize_A5, "mnuPageSize_A5");
            this.mnuPageSize_A5.Name = "mnuPageSize_A5";
            this.mnuPageSize_A5.Click += new System.EventHandler(this.OnPageSizeChanged);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.FileListBox);
            this.Name = "MainForm";
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
        private System.Windows.Forms.ToolStripDropDownButton PageSizeButton;
        private System.Windows.Forms.ToolStripMenuItem mnuPageSize_Original;
        private System.Windows.Forms.ToolStripMenuItem mnuPageSize_A4;
        private System.Windows.Forms.ToolStripMenuItem mnuPageSize_B4;
        private System.Windows.Forms.ToolStripMenuItem mnuPageSize_A5;
        private System.Windows.Forms.ToolStripButton addBookmarkButton;
    }
}