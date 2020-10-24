/*
 * Copyright 2009-2011 Joern Schou-Rode
 * 
 * This file is part of PDFBinder.
 *
 * PDFBinder is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * PDFBinder is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with PDFBinder.  If not, see <http://www.gnu.org/licenses/>.
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
        private readonly PdfCopy _pdfCopy;

        public Combiner(string outputFilePath)
        {
            var outputStream = File.Create(outputFilePath);

            _document = new Document();
            _pdfCopy = new PdfCopy(_document, outputStream);
            _document.Open();
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
                    _document.SetPageSize(size);
                    _document.NewPage();

                    var page = _pdfCopy.GetImportedPage(reader, i);
                    _pdfCopy.AddPage(page);
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
}
