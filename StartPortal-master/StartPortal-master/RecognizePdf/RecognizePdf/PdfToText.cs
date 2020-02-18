using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecognizePdf
{
    public static class PdfToText
    {
        public static IEnumerable<string> GetPagesText(string filePath)
        {
            using (var pdfReader = new PdfReader(filePath))
            {
                var pages = pdfReader.NumberOfPages;
                var builder = new StringBuilder();

                for (var i = 0; i < pages; i++)
                {
                    var page = pdfReader.GetPageContent(i + 1);
                    var text = PdfTextExtractor.GetTextFromPage(pdfReader, i + 1);
                    yield return text;
                }

            }
        }

        public static IEnumerable<string> ReadLineByLine(this string inputString)
        {
            using (var strReader = new StringReader(inputString))
            {
                do
                {
                    var line = strReader.ReadLine();

                    if (line == null)
                    {
                        yield break;
                    }

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    yield return line;
                } while (true);
            }
        }

        public static string GetText(string filePath)
        {
            return string.Join("", GetPagesText(filePath));
        }
    }
}
