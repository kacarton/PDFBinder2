using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BookmarkName = System.String;

namespace PDFBinder
{
    public partial class MainForm : Form
    {
        class PdfInfo
        {
            public string Fullname, //path\filename
                Filename,           //filename
                Ranges;             //page ranges, ex: 1-5,8,10-12
            public int TotalPages;  //pages of pdf file.

        }
        private int BookmarkCounter = 0;

        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
            PageSizeButton.Tag = (int)PDFBinder.PageSize.Original;
            mnuPageSize_Original.Tag = (int)PDFBinder.PageSize.Original;
            mnuPageSize_A4.Tag = (int)PDFBinder.PageSize.A4;
            mnuPageSize_A5.Tag = (int)PDFBinder.PageSize.A5;
            mnuPageSize_B4.Tag = (int)PDFBinder.PageSize.B4;
            PageSizeButton.Text = resources.GetString("PageSizeButton.Text");
            FileListBox.Font = toolStrip1.Font;
            FileListBox.ItemHeight = FileListBox.Font.Height * 12 / 7;
        }

        public void AddInputFile(string file)
        {
            int Pages = 0;
            switch (Combiner.TestSourceFile(file, out Pages))
            {
                case Combiner.SourceTestResult.Unreadable:
                    MessageBox.Show(string.Format(resources.GetString("Error.Unreadable.Text"), file), resources.GetString("Error.Unreadable.Title"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case Combiner.SourceTestResult.Protected:
                    MessageBox.Show(string.Format(resources.GetString("Error.Protected.Text"), file), resources.GetString("Error.Protected.Title"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    break;
                case Combiner.SourceTestResult.Ok:
                    //FileListBox.Items.Add(file + "\n\n" + Pages.ToString());
                    FileListBox.Items.Add(new PdfInfo() { Fullname = file, Filename = Path.GetFileName(file), Ranges = "", TotalPages = Pages });
                    break;
            }
        }

        public void UpdateUI()
        {
            //未添加文件禁用按钮
            if (FileListBox.Items.Count < 1)
            {
                sortButton.Enabled = completeButton.Enabled = false;
                helpLabel.Text = resources.GetString("HelpLabel.Empty");
            }
            //只添加一个项目
            //else if (FileListBox.Items.Count == 1 && ((string)FileListBox.Items[0]).Split('\n')[1]!="")
            else if (FileListBox.Items.Count == 1)
            {
                sortButton.Enabled = false;
                if (FileListBox.Items[0] is BookmarkName)
                {
                    completeButton.Enabled = false;
                }
                else if (((PdfInfo)FileListBox.Items[0]).Ranges != "")
                {
                    completeButton.Enabled = true;
                    completeButton.Text = resources.GetString("Split.Text");
                    helpLabel.Text = resources.GetString("HelpLabel.Split");
                }
                else
                {
                    completeButton.Enabled = false;
                    completeButton.Text = resources.GetString("completeButton.Text");
                    helpLabel.Text = resources.GetString("HelpLabel.Empty");
                }
            }
            else if (FileListBox.Items.Count > 1)
            {
                sortButton.Enabled = true;
                int pdfCount = 0;
                for(int i=0; i< FileListBox.Items.Count; i++)
                    if (FileListBox.Items[i] is PdfInfo)
                        pdfCount++;
                completeButton.Enabled = pdfCount > 0;
                completeButton.Text = resources.GetString("completeButton.Text");
                helpLabel.Text = resources.GetString("HelpLabel.Bind");
            }
            else
            {
                sortButton.Enabled = completeButton.Enabled = false;
                helpLabel.Text = resources.GetString("HelpLabel.Empty");
            }

            addBookmarkButton.Enabled = FileListBox.SelectedItems.Count > 0;
            if (FileListBox.SelectedIndex < 0)
            {
                addBookmarkButton.Enabled = removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = false;
            }
            else if (FileListBox.SelectedItems.Count == 1)
            {
                removeButton.Enabled = true;
                moveUpButton.Enabled = (FileListBox.SelectedIndex > 0);
                moveDownButton.Enabled = (FileListBox.SelectedIndex < FileListBox.Items.Count - 1);
            }
            else
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = true;
            }
        }

        private void FileListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void FileListBox_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            Array.Sort(fileNames);

            foreach (var file in fileNames)
            {
                AddInputFile(file);
            }

            UpdateUI();
        }

        private void combineButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var combiner = new Combiner(saveFileDialog.FileName, (PDFBinder.PageSize)PageSizeButton.Tag))
                {
                    progressBar.Visible = true;
                    this.Enabled = false;

                    for (int i = 0; i < FileListBox.Items.Count; i++)
                    {
                        if (FileListBox.Items[i] is BookmarkName)
                            combiner.AddBookmark((string)FileListBox.Items[i]);
                        else
                            //combiner.AddFile(((string)FileListBox.Items[i]).Split('\n')[0], ((string)FileListBox.Items[i]).Split('\n')[1]);
                            combiner.AddFile(((PdfInfo)FileListBox.Items[i]).Fullname, ((PdfInfo)FileListBox.Items[i]).Ranges);
                        progressBar.Value = (int)(((i + 1) / (double)FileListBox.Items.Count) * 100);
                    }

                    this.Enabled = true;
                    progressBar.Visible = false;
                }

                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            if (addFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in addFileDialog.FileNames)
                {
                    AddInputFile(file);
                }

                UpdateUI();
            }
        }

        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            for (int i = FileListBox.SelectedItems.Count - 1; i >= 0; i--)
            {
                if (FileListBox.SelectedItems[i] is BookmarkName)
                    BookmarkCounter--;
                FileListBox.Items.Remove(FileListBox.SelectedItems[i]);
            }
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (FileListBox.SelectedItems.Count <= 0) return;
            for (int i = FileListBox.SelectedItems.Count - 1; i >= 0; i--)
            {
                object dataItem = FileListBox.SelectedItems[i];
                int index = FileListBox.Items.IndexOf(dataItem);

                if (index >= FileListBox.Items.Count - (FileListBox.SelectedItems.Count - i)) continue;

                FileListBox.Items.Remove(dataItem);
                FileListBox.Items.Insert(++index, dataItem);

                FileListBox.SelectedItems.Add(dataItem);
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (FileListBox.SelectedItems.Count <= 0) return;
            object[] collect = new object[FileListBox.SelectedItems.Count];
            for (int i = 0; i < FileListBox.SelectedItems.Count; i++)
                collect[i] = FileListBox.SelectedItems[i];

            for (int i = 0; i < collect.Length; i++)
            {
                object dataItem = collect[i];
                int index = FileListBox.Items.IndexOf(dataItem);

                if (index <= i) continue;

                FileListBox.Items.Remove(dataItem);
                FileListBox.Items.Insert(--index, dataItem);
            }
            FileListBox.SelectedItems.Clear();
            for (int i = 0; i < collect.Length; i++)
                FileListBox.SelectedItems.Add(collect[i]);
        }

        private Color ColorAlt = Color.FromArgb(240, 240, 240);//交替色 
        private Color ColorSelected = Color.FromArgb(150, 200, 250);//选择项目颜色 

        private void FileListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            const int RIGHT_MARGIN = 130;
            if (e.Index < 0) return;

            Brush myBrush = Brushes.Black;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myBrush = new SolidBrush(ColorSelected);
            }
            else if (e.Index % 2 == 1)
            {
                myBrush = new SolidBrush(ColorAlt);
            }
            else
            {
                myBrush = new SolidBrush(Color.White);
            }
            e.Graphics.FillRectangle(myBrush, e.Bounds);
            e.DrawFocusRectangle();//焦点框 

            StringFormat Formater = new StringFormat();
            Formater.Alignment = StringAlignment.Near;
            Formater.LineAlignment = StringAlignment.Center;
            Formater.Trimming = StringTrimming.EllipsisPath;
            Formater.FormatFlags = StringFormatFlags.NoWrap;

            //绘制书签名
            if (FileListBox.Items[e.Index] is BookmarkName)
            {
                //绘书签图标
                e.Graphics.DrawImage(addBookmarkButton.Image, e.Bounds.X, e.Bounds.Y + ((e.Bounds.Height - addBookmarkButton.Image.Height) /2));
                //绘书签名
                e.Graphics.DrawString((BookmarkName)FileListBox.Items[e.Index], e.Font, Brushes.Black
                    , new Rectangle(e.Bounds.X + addBookmarkButton.Image.Width, e.Bounds.Y, e.Bounds.Width - RIGHT_MARGIN, e.Bounds.Height), Formater);
                return;
            }

            //绘制PDF文件名

            //文本 
            /*string[] text = FileListBox.Items[e.Index].ToString().Split('\n');
            if (showNameButton.Checked)
                e.Graphics.DrawString(text[0], e.Font, Brushes.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height), Formater);
            else
                e.Graphics.DrawString(Path.GetFileName(text[0]), e.Font, Brushes.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height), Formater);

            Formater.Alignment = StringAlignment.Far;
            e.Graphics.DrawString((text[1] == "" ? "" : text[1] + " | ") + "共 " + text[2] + " 页", e.Font, Brushes.Gray, e.Bounds, Formater);
            */
            PdfInfo item = (PdfInfo)FileListBox.Items[e.Index];
            e.Graphics.DrawString(showNameButton.Checked ? item.Fullname : item.Filename, e.Font, Brushes.Black
                , new Rectangle(e.Bounds.X + (BookmarkCounter > 0 ? (int)(addBookmarkButton.Image.Width * 1.5) : 0), e.Bounds.Y, e.Bounds.Width - RIGHT_MARGIN, e.Bounds.Height), Formater);

            Formater.Alignment = StringAlignment.Far;
            e.Graphics.DrawString((item.Ranges == "" ? "" : item.Ranges + " | ") 
                + string.Format(item.TotalPages>1 ? resources.GetString("Pages"): resources.GetString("Page"), item.TotalPages)
                , e.Font, Brushes.Gray, e.Bounds, Formater);
        }

        private void showNameButton_Click(object sender, EventArgs e)
        {
            showNameButton.Text = showNameButton.Checked ? resources.GetString("showNameButton.Full") : resources.GetString("showNameButton.Name");
            FileListBox.Invalidate();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            sortButton.Tag = (string)sortButton.Tag == "Asc" ? "Desc" : "Asc";
            if ((string)sortButton.Tag == "Asc")
            {
                sortButton.Image = PDFBinder.Properties.Resources.sortAsc;
            }
            else
                sortButton.Image = PDFBinder.Properties.Resources.sortDesc;

            List<PdfInfo> list = new List<PdfInfo>();
            foreach (var item in FileListBox.Items)
            {
                if (item is PdfInfo)
                    list.Add((PdfInfo)item);
            }
            list.Sort((a, b) =>
            {
                if ((string)sortButton.Tag == "Asc")
                {
                    if (showNameButton.Checked)
                        return a.Fullname.CompareTo(b.Fullname);
                    else
                        return a.Filename.CompareTo(b.Filename);
                }
                else
                {
                    if (showNameButton.Checked)
                        return b.Fullname.CompareTo(a.Fullname);
                    else
                        return b.Filename.CompareTo(a.Filename);
                }
            });

            if (BookmarkCounter > 0)
            { 
                for(int i = FileListBox.Items.Count - 1; i>=0; i--)
                    if (FileListBox.Items[i] is PdfInfo)
                        FileListBox.Items.RemoveAt(i);
            }
            else
                FileListBox.Items.Clear();
            foreach (PdfInfo item in list)
            {
                FileListBox.Items.Add(item);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            FileListBox.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mnuClear.Enabled = FileListBox.Items.Count > 0;
            mnuSetPageRange.Enabled = FileListBox.SelectedItems.Count == 1;
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            FileListBox.Items.Clear();
            BookmarkCounter = 0;
        }

        private void mnuSetPageRange_Click(object sender, EventArgs e)
        {
            PdfInfo item = ((PdfInfo)FileListBox.SelectedItem);
            string range = Interaction.InputBox(resources.GetString("SetPageRange.Prompt"), resources.GetString("SetPageRange.Title"), item.Ranges);
            if (range != item.Ranges)
            {
                if (range == "")
                {
                    ((PdfInfo)FileListBox.Items[FileListBox.SelectedIndex]).Ranges = "";
                    return;
                }

                string[] arr = range.Replace("，", ",").Replace(" ", "").Split(',');
                range = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    if ("" == arr[i]) continue;
                    if (Regex.IsMatch(arr[i], @"^\d+$") || Regex.IsMatch(arr[i], @"^\d+-\d+$"))
                        range += ("" == range ? "" : ",") + arr[i];
                    else
                    {
                        MessageBox.Show(resources.GetString("Error.RangeValid")); 
                        return;
                    }
                }
                ((PdfInfo)FileListBox.Items[FileListBox.SelectedIndex]).Ranges = range;
                UpdateUI();
            }
        }

        private void OnPageSizeChanged(object sender, EventArgs e)
        {
            PageSizeButton.Tag = ((ToolStripMenuItem)sender).Tag;
            mnuPageSize_Original.Checked = sender == mnuPageSize_Original;
            mnuPageSize_A4.Checked = sender == mnuPageSize_A4;
            mnuPageSize_A5.Checked = sender == mnuPageSize_A5;
            mnuPageSize_B4.Checked = sender == mnuPageSize_B4;
            if (mnuPageSize_Original.Checked)
                PageSizeButton.Text = resources.GetString("PageSizeButton.Text");
            else
                PageSizeButton.Text = resources.GetString("PageSizeButton.Text") + ":" + ((ToolStripMenuItem)sender).Text;
        }

        private void addBookmarkButton_Click(object sender, EventArgs e)
        {
            if (FileListBox.SelectedIndex < 0) return;

            BookmarkName bookmark = "";
            if (FileListBox.SelectedItem is BookmarkName)
                bookmark = (BookmarkName)FileListBox.SelectedItem;
            else 
            {
                bookmark = ((PdfInfo)FileListBox.SelectedItem).Filename;
                if (bookmark.Contains("."))
                    bookmark = bookmark.Substring(0, bookmark.LastIndexOf("."));
            }
            BookmarkName newName = Interaction.InputBox(resources.GetString("SetBookmark.Prompt"), resources.GetString("SetBookmark.Title"), bookmark);
            if (newName != "")
            {
                if (FileListBox.SelectedItem is BookmarkName)
                    FileListBox.Items[FileListBox.SelectedIndex] = newName;
                else
                {
                    FileListBox.Items.Insert(FileListBox.SelectedIndex, newName);
                    BookmarkCounter++;
                }
            }
        }
    }
}