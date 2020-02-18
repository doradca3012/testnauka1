using Microsoft.Win32;
using RecognizePdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PeselValidate
{
    public enum ResultItemType
    {
        Error, Success, Info
    }

    public class ResultItem
    {
        public ResultItemType ItemType { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return $"{ItemType}:\t{Message}";
        }
        public SolidColorBrush Color
        {
            get
            {
                if (ItemType == ResultItemType.Error)
                {

                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Colors.Red;
                    return brush;
                }
                else if (ItemType == ResultItemType.Info)
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Colors.Black;
                    return brush;

                }
                else
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Colors.Green;
                    return brush;
                }
            }
        }

        public static ResultItem CreateError(string message)
        {
            return new ResultItem { Message = message, ItemType = ResultItemType.Error };
        }

        public static ResultItem CreateInfo(string message)
        {
            return new ResultItem { Message = message, ItemType = ResultItemType.Info };
        }

        public static ResultItem CreateSuccess(string message)
        {
            return new ResultItem { Message = message, ItemType = ResultItemType.Success };
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string LoadedText { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF (*.pdf)|*.Pdf",
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadedText = PdfToText.GetText(openFileDialog.FileName);
                DisplayResults();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayResults();
        }

        private void DisplayResults()
        {
            ResultList.Items.Clear();

            try
            {
                var now = DateTime.Now;
                if (InputPesel.Text.Length < 11)
                {
                    return;
                }
                var items = InputPesel.Text.Split(',', ' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray();

                var search = items.Where(PeselHelper.IsValidPesel).ToArray();
                var invalidPesels = items.Where(p => !PeselHelper.IsValidPesel(p)).ToArray().Distinct();
                var fileContent = LoadedText ?? string.Empty;


                var sb = new List<ResultItem>();

                if (invalidPesels.Any())
                {
                    sb.Add(ResultItem.CreateError($"Niepoprawny numer PESEL: {string.Join(", ", invalidPesels)}"));
                    InputPesel.BorderBrush = Brushes.Red;
                    InputPesel.BorderThickness = new Thickness(0, 0, 0, 5);
                }
                else
                {
                    InputPesel.BorderThickness = new Thickness(0, 0, 0, 0);
                }

                if (string.IsNullOrEmpty(fileContent))
                {
                    sb.Add(ResultItem.CreateInfo("Nie wczytano pliku."));
                }

                var searchResults = items.Where(p => !fileContent.Contains(p)).Distinct();

                if (!searchResults.Any())
                {
                    sb.Add(ResultItem.CreateInfo("Wszystkie podane numery PESEL znalazły się chociaż raz w dokumencie."));
                }
                else
                {
                    sb.Add(ResultItem.CreateError($"Podene numery PESEL nie znalazły się w dokumencie: { string.Join(", ", searchResults) }"));
                }

                var posibleOtherPesels = PeselHelper.EnumeratePosiblePesels(fileContent).Where(PeselHelper.IsValidPesel).Where(p => !items.Contains(p)).Distinct();

                if (posibleOtherPesels.Any())
                {
                    sb.Add(ResultItem.CreateError($"Znaleziono inne numery PESEL: {string.Join(", ", posibleOtherPesels)}"));
                }

                foreach (var line in sb)
                {
                    ResultList.Items.Add(line);
                }
            }
            catch (Exception e)
            {
                ResultList.Items.Clear();
                ResultList.Items.Add(ResultItem.CreateError(e.Message));
            }
        }

        private void ResultList_KeyDown(object sender, KeyEventArgs e)
        {
            var stringBuilder = new StringBuilder();
            foreach (var line in ResultList.Items)
            {
                stringBuilder.AppendLine(line.ToString());
            }
            Clipboard.SetText(stringBuilder.ToString());
        }
    }
}
