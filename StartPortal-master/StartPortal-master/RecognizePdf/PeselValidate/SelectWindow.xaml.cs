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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SelectWindow : Window
    {
        private string LoadedText { get; set; }

        public SelectWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AccountCompare().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new AccountCompare(false).ShowDialog();
        }
    }
}
