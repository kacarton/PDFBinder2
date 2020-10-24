using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace PDFBinder
{
    public partial class MainForm : Form
    {
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
                    inputListBox.Items.Add(file + "\n\n" + Pages.ToString());
                    break;
            }
        }

        public void UpdateUI()
        {
            if (inputListBox.Items.Count < 2)
            {
                sortButton.Enabled = false;
                completeButton.Enabled = false;
                helpLabel.Text = "请拖放PDF文档到列表框中，或点击工具栏的“添加文档...”按钮";
            }
            else
            {
                sortButton.Enabled = true;
                completeButton.Enabled = true;
                helpLabel.Text = "请点击“合并文档”按钮对列表中的文档进行合并";
            }

            if (inputListBox.SelectedIndex < 0)
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = false;
            }
            else if (inputListBox.SelectedItems.Count == 1)
            {
                removeButton.Enabled = true;
                moveUpButton.Enabled = (inputListBox.SelectedIndex > 0);
                moveDownButton.Enabled = (inputListBox.SelectedIndex < inputListBox.Items.Count - 1);
            }
            else
            {
                removeButton.Enabled = moveUpButton.Enabled = moveDownButton.Enabled = true;
            }
        }

        private void inputListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void inputListBox_DragDrop(object sender, DragEventArgs e)
        {
            var fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);
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

                    for (int i = 0; i < inputListBox.Items.Count; i++)
                    {
                        combiner.AddFile(((string)inputListBox.Items[i]).Split('\n')[0], ((string)inputListBox.Items[i]).Split('\n')[1]);
                        progressBar.Value = (int)(((i + 1) / (double)inputListBox.Items.Count) * 100);
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

        private void inputListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            for (int i = inputListBox.SelectedItems.Count - 1; i >= 0; i--)
                inputListBox.Items.Remove(inputListBox.SelectedItems[i]);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (inputListBox.SelectedItems.Count <= 0) return;
            for (int i = inputListBox.SelectedItems.Count - 1; i >= 0; i--)
            {
                object dataItem = inputListBox.SelectedItems[i];
                int index = inputListBox.Items.IndexOf(dataItem);

                if (index >= inputListBox.Items.Count - (inputListBox.SelectedItems.Count - i)) continue;

                inputListBox.Items.Remove(dataItem);
                inputListBox.Items.Insert(++index, dataItem);

                inputListBox.SelectedItems.Add(dataItem);
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (inputListBox.SelectedItems.Count <= 0) return;
            object[] collect = new object[inputListBox.SelectedItems.Count];
            for (int i = 0; i < inputListBox.SelectedItems.Count; i++)
                collect[i] = inputListBox.SelectedItems[i];

            for (int i = 0; i < collect.Length; i++)
            {
                object dataItem = collect[i];
                int index = inputListBox.Items.IndexOf(dataItem);

                if (index <= i) continue;

                inputListBox.Items.Remove(dataItem);
                inputListBox.Items.Insert(--index, dataItem);
            }
            inputListBox.SelectedItems.Clear();
            for (int i = 0; i < collect.Length; i++)
                inputListBox.SelectedItems.Add(collect[i]);
        }

        private Color ColorAlt = Color.FromArgb(240, 240, 240);//交替色 
        private Color ColorSelected = Color.FromArgb(150, 200, 250);//选择项目颜色 

        private void inputListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            Brush myBrush = Brushes.Black;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myBrush = new SolidBrush(ColorSelected);
            }
            else if (e.Index % 2 == 0)
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
            string[] text = inputListBox.Items[e.Index].ToString().Split('\n');
            if (showNameButton.Checked)
                e.Graphics.DrawString(text[0], e.Font, Brushes.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height), Formater);
            else
                e.Graphics.DrawString(Path.GetFileName(text[0]), e.Font, Brushes.Black, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height), Formater);

            Formater.Alignment = StringAlignment.Far;
            e.Graphics.DrawString((text[1]=="" ?  "" : text[1] + " | ") + "共 " + text[2] + " 页", e.Font, Brushes.Gray, e.Bounds, Formater);
        }

        private void showNameButton_Click(object sender, EventArgs e)
        {
            showNameButton.Text = showNameButton.Checked ? "全" : "简";
            inputListBox.Invalidate();
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

            List<String> list = new List<string>();
            foreach (String s in inputListBox.Items)
            {
                list.Add(s);
            }
            list.Sort((a, b) =>
            {
                if ((string)sortButton.Tag == "Asc")
                {
                    if (showNameButton.Checked)
                        return a.CompareTo(b);
                    else
                        return Path.GetFileName(a.Split('\n')[0]).CompareTo(Path.GetFileName(b.Split('\n')[0]));
                }
                else
                {
                    if (showNameButton.Checked)
                        return b.CompareTo(a);
                    else
                        return Path.GetFileName(b.Split('\n')[0]).CompareTo(Path.GetFileName(a.Split('\n')[0]));
                }
            });

            inputListBox.Items.Clear();
            foreach (string s in list)
            {
                inputListBox.Items.Add(s);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            inputListBox.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            mnuClear.Enabled = inputListBox.Items.Count > 0;
            mnuSetPageRange.Enabled = inputListBox.SelectedItems.Count == 1;
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            inputListBox.Items.Clear();
        }

        private void mnuSetPageRange_Click(object sender, EventArgs e)
        {
            string text = ((string)inputListBox.SelectedItem);
            string range = Interaction.InputBox("如：1-5,8,10,13", "设置合并的页码范围或次序", text.Split('\n')[1]);
            if (range != text.Split('\n')[1])
            {
                if (range=="")
                {
                    inputListBox.Items[inputListBox.SelectedIndex] = text.Split('\n')[0] + "\n\n" + text.Split('\n')[2];
                    return;
                }

                string[] arr = range.Replace("，", ",").Replace(" ", "").Split(',');
                range = "";
                for (int i=0; i<arr.Length; i++)
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
                inputListBox.Items[inputListBox.SelectedIndex] = text.Split('\n')[0] + "\n" + range + "\n" + text.Split('\n')[2];
            }
        }
    }
}