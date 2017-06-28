using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Program_launcher
{
    /// <summary>
    /// Interaction logic for AliasDialog.xaml
    /// </summary>
    public partial class AliasDialog : Window
    {
        public AliasDialog()
        {
            InitializeComponent();
        }
        public string ResponceText = "";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResponceText = AliasTextBox.Text;
            DialogResult = true;
        }
    }
}
