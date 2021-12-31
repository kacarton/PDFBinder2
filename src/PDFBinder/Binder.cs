/*
 * This file is part of PDFBinder2.
 *
 * PDFBinder2 is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * PDFBinder2 is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with PDFBinder2.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace PDFBinder
{
    class Combiner : IDisposable
    {
        private readonly Document _document;
        private readonly PdfCopy _pdfCopy = null;
        private PdfWriter _writer = null;
        public static string Author = "";//文档作者
        private PageSize _newPageSize = PageSize.Original;//合并时调整页面尺寸
        private string BookMarkName = null;

        public Combiner(string outputFilePath, PageSize newPageSize = PageSize.Original)
        {
            _newPageSize = newPageSize;
            var outputStream = File.Create(outputFilePath);

            _document = new Document();
            if (_newPageSize == PageSize.Original)
                _pdfCopy = new PdfCopy(_document, outputStream);
            else
                _writer = PdfWriter.GetInstance(_document, outputStream);
            _document.Open();
            if (Author != "") _document.AddAuthor(Author);
        }

        public void AddBookmark(string bookmark)
        {
            this.BookMarkName = bookmark;
        }

        public void AddFile(string fileName, string range)
        {
            var reader = new PdfReader(fileName);

            int firstPage = 1;
            int lastPage = reader.NumberOfPages;
            if (range == null || range == "")
                range = firstPage.ToString() + "-" + lastPage.ToString();
            string[] ranges = range.Split(',');
            for (int n = 0; n < ranges.Length; n++)
            {
                if (Regex.IsMatch(ranges[n], @"^\d+$"))
                {
                    firstPage = lastPage = int.Parse(ranges[n]);
                }
                else if (Regex.IsMatch(ranges[n], @"^\d+-\d+$"))
                {
                    string[] arr = ranges[n].Split('-');
                    firstPage = Math.Min(int.Parse(arr[0]), int.Parse(arr[1]));
                    lastPage = Math.Max(int.Parse(arr[0]), int.Parse(arr[1]));
                }
                else
                    continue;

                if (firstPage > reader.NumberOfPages)
                    continue;

                for (var i = firstPage; i <= lastPage; i++)
                {
                    if (i > reader.NumberOfPages)
                        continue;

                    var size = reader.GetPageSizeWithRotation(i);
                    if (_newPageSize == PageSize.Original)
                    {
                        _document.SetPageSize(size);
                    }
                    else
                    {
                        switch(_newPageSize)
                        {
                            case PageSize.A4: _document.SetPageSize(iTextSharp.text.PageSize.A4); break;
                            case PageSize.A5: _document.SetPageSize(iTextSharp.text.PageSize.A5); break;
                            case PageSize.B4: _document.SetPageSize(iTextSharp.text.PageSize.B4); break;
                        }
                        _document.SetMargins(
                            Math.Max(0, (_document.PageSize.Width - size.Width) / 2),
                            Math.Max(0, (_document.PageSize.Width - size.Width) / 2),
                            Math.Max(0, (_document.PageSize.Height - size.Height) / 2),
                            Math.Max(0, (_document.PageSize.Height - size.Height) / 2)
                        );
                    }
                    _document.NewPage();
                    
                    //添加书签
                    if (!string.IsNullOrEmpty(this.BookMarkName))
                    { 
                        Chapter _chapter = new Chapter("", 1);
                        _chapter.BookmarkTitle = this.BookMarkName;
                        _chapter.BookmarkOpen = true;
                        _document.Add(_chapter);
                        this.BookMarkName = null;
                    }

                    if (_newPageSize == PageSize.Original)
                    {
                        var page = _pdfCopy.GetImportedPage(reader, i);
                        _pdfCopy.AddPage(page);
                    }
                    else
                    {
                        var page = _writer.GetImportedPage(reader, i);
                        _document.Add(iTextSharp.text.Image.GetInstance(page));
                    }
                }
            }

            reader.Close();
        }

        public void Dispose()
        {
            _document.Close();
        }

        public static SourceTestResult TestSourceFile(string fileName, out int PageCount)
        {
            PageCount = 0;
            try
            {
                PdfReader reader = new PdfReader(fileName);
                bool ok = !reader.IsEncrypted() ||
                    (reader.Permissions & PdfWriter.AllowAssembly) == PdfWriter.AllowAssembly;
                if (ok)
                    PageCount = reader.NumberOfPages;
                reader.Close();

                return ok ? SourceTestResult.Ok : SourceTestResult.Protected;
            }
            catch
            {
                return SourceTestResult.Unreadable;
            }
        }

        public enum SourceTestResult
        {
            Ok, Unreadable, Protected
        }

    }
    
    //调整页面尺寸为
    public enum PageSize
    {
        Original, A4, B4, A5
    }
}
