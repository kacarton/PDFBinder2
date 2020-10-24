PDFBinder v2.0
==============

Brief:
A simple tool to merge several PDF documents into one or split PDF document.
It's enhance on base project PDFBinder v1.2 [see Original gitHub repository], 
it takes more feature, ex:multiple select, batch move up/down, sort, page ranges...

PDFBinder2是一个简单而易于使用的PDF文档合并/拆分工具。它以PDFBinder v1.2为基础（见
原项目GitHub资源），增加了列表多选、排序、合并次序调整、设定合并的页码范围等功能，能
帮助文档工作者快速高效地处理PDF文件。

Contents:

  1. Installation and usage
  2. Contributing to the project
  3. Licensing

Installation and usage
----------------------

PDFBinder can be installed on Microsoft Windows systems using a pretty
installer, which can be downloaded from the GitHub repository.

In order to use PDFBinder on other platforms - or if the installer seems
like a bad choice for other reasons - PDFBinder can be built installed from
source. Grab the latest source package the GitHub repository. Use
whatever C# compiler you have available to build the project, or use the
provided project file for Microsoft Visual Studio 2010 - 2015.

Hopefully, the user interface of PDFBinder is pretty self-explanatory. You
can add source PDF documents by using the "Add source documents..." button,
or by dragging the files in from a file browser. Documents can be moved up,
moved down or remoevd by pressing the respective buttons in the toolbar.
Press the "Bind!" button when your list of documents seems fine.

If PDFBinder was installed using the pre-built installer, an extra option
should have been automatically added to your context menu (the menu that on
Windows displays when right clicking a file) for all PDF files. Select any
number of PDFs in a file browser and choose the "Add to PDFBinder..." option
to bring up the PDFBinder interface with those files already in the list.

Contributing to the project
---------------------------

Any kind of contibutions to the project are very welcome. Issues should be
reported directly in the issue tracker on the GitHub repository, and
reporters are very welcome to attach patches to their reports.

If you wish to encourage further development of PDFBinder by donating to the
project, please get in contact with the project owner (e-mail:kacarton@msn.com), 
and we will find some way for you to transfer a reasonable amount of money
or beer to the project.

Licensing
---------

PDFBinder is released under the terms of the GNU General Public License.
Please see LICENSE.txt for the complete legal text.

All of the PDF magic is done using the iTextSharp 4.1.6 library, released
under both MPL and LGPL. Please refer to the iTextSharp project site.


[GitHub repository]:          https://github.com/kacarton/pdfbinder2
[Original gitHub repository]: https://github.com/schourode/pdfbinder
[iTextSharp project site]:    http://itextsharp.sourceforge.net/
