using CommandLine;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RecognizePdf
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunApplication(opts));
        }

        static void RunApplication(Options options)
        {
            if (!File.Exists(options.FilePath))
            {
                Console.WriteLine($"File '{options.FilePath}' does not exist");
                return;
            }
            Console.WriteLine($"92019090820 {IsValidPesel("92019090820")}");
            using (var pdfReader = new PdfReader(options.FilePath))
            {
                var pages = pdfReader.NumberOfPages;
                var builder = new StringBuilder();

                for (var i = 0; i < pages; i++)
                {
                    var page = pdfReader.GetPageContent(i + 1);
                    var text = PdfTextExtractor.GetTextFromPage(pdfReader, i + 1);
                    builder.Append(text);
                }

                var textFrom = builder.ToString();
                var textAll = string.Join("", textFrom.ToCharArray().Where(char.IsDigit));
                var thisSearch = textAll.Contains(options.SearchString);
                var pesels = EnumeratePosiblePesels(textAll).Where(IsValidPesel).Where(s => textFrom.Contains(s));

                if (thisSearch)
                {
                    Console.WriteLine($"Znalazłem {options.SearchString}");
                }

                if(pesels.Any())
                {
                    Console.WriteLine($"Jest pesel : {string.Join(", ", pesels)}");
                } else
                {
                    Console.WriteLine("Nie ma peselu :(");
                }

                Console.WriteLine();
                Console.WriteLine();

                Console.Write(textFrom);
            }
        }

        static IEnumerable<string> EnumeratePosiblePesels(string input, int length = 11)
        {
            for (var i = 0; i < input.Length - length; i++)
            {
                yield return input.Substring(i, length);
            }
        }

        static bool IsValidPesel(string pesel)
        {
            var peselArray = pesel.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            var arr = new[] {
                1, 3, 7, 9, 1, 3, 7, 9, 1, 3
            };

            var calculatedSum = arr.Select((v, i) => v * peselArray[i]).Sum();

            var result = (10 - calculatedSum % 10) % 10;

            return result == peselArray.Last();
        }
    }

    class Options
    {
        [Option('f', "file", Required = true, HelpText = "File path")]
        public string FilePath { get; set; }

        [Option('s', "search", Required = true, HelpText = "Search string")]
        public string SearchString { get; set; }
    }
}
