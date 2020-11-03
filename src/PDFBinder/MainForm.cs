using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        public void AddInputFile(string file)
        {
            int Pages = 0;
            switch (Combiner.TestSourceFile(file, out Pages))
            {
                case Combiner.SourceTestResult.Unreadable:
                    MessageBox.Show(string.Format("文件不能以PDF格式打开:\n\n{0}", file), "无效的文件类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case Combiner.SourceTestResult.Protected:
                    MessageBox.Show(string.Format("PDF文档不允许复制:\n\n{0}", file), "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    break;
                case Combiner.SourceTestResult.Ok:
                    //FileListBox.Items.Add(file + "\n\n" + Pages.ToString());
                    FileListBox.Items.Add(new PdfInfo() { Fullname = file, Filename = Path.GetFileName(file), Ranges = "", TotalPages = Pages });
                    break;
            }
        }

        public void UpdateUI()
        {
            if (FileListBox.Items.Count < 1)
            {
                sortButton.Enabled = false;
                completeButton.Enabled = false;
                helpLabel.Text = "请拖放PDF文档到列表框中，或点击工具栏的“添加文档...”按钮";
            }
            //else if (FileListBox.Items.Count == 1 && ((string)FileListBox.Items[0]).Split('\n')[1]!="")
            else if (FileListBox.Items.Count == 1 && ((PdfInfo)FileListBox.Items[0]).Ranges != "")
            {
                sortButton.Enabled = false;
                completeButton.Enabled = true;
                completeButton.Text = "拆分文档";
                helpLabel.Text = "请点击“拆分文档”按钮对文档进行拆分";
            }
            else if (FileListBox.Items.Count > 1)
            {
                sortButton.Enabled = true;
                completeButton.Enabled = true;
                completeButton.Text = "合并文档";
                helpLabel.Text = "请点击“合并文档”按钮对列表中的文档进行合并";
            }
            else
            {
                sortButton.Enabled = false;
                completeButton.Enabled = false;
                helpLabel.Text = "请拖放PDF文档到列表框中，或点击工具栏的“添加文档...”按钮";
            }

            if (FileListBox.SelectedIndex < 0)
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = false;
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
                using (var combiner = new Combiner(saveFileDialog.FileName))
                {
                    progressBar.Visible = true;
                    this.Enabled = false;

                    for (int i = 0; i < FileListBox.Items.Count; i++)
                    {
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
                FileListBox.Items.Remove(FileListBox.SelectedItems[i]);
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
            e.Graphics.DrawString(showNameButton.Checked ? item.Fullname : item.Filename, e.Font, Brushes.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - RIGHT_MARGIN, e.Bounds.Height), Formater);

            Formater.Alignment = StringAlignment.Far;
            e.Graphics.DrawString((item.Ranges == "" ? "" : item.Ranges + " | ") + "共 " + item.TotalPages.ToString() + " 页", e.Font, Brushes.Gray, e.Bounds, Formater);
        }

        private void showNameButton_Click(object sender, EventArgs e)
        {
            showNameButton.Text = showNameButton.Checked ? "全" : "简";
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
            foreach (PdfInfo item in FileListBox.Items)
            {
                list.Add(item);
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
        }

        private void mnuSetPageRange_Click(object sender, EventArgs e)
        {
            PdfInfo item = ((PdfInfo)FileListBox.SelectedItem);
            string range = Interaction.InputBox("如：1-5,8,10,13", "设置合并的页码范围或次序", item.Ranges);
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
                        MessageBox.Show("页码格式无效!");
                        return;
                    }
                }
                ((PdfInfo)FileListBox.Items[FileListBox.SelectedIndex]).Ranges = range;
            }
        }
    }
}