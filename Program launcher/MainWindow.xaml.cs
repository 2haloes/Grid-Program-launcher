using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Program_launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Programs> ProgramsList;

        public MainWindow()
        {
            InitializeComponent();
            InitListView();
        }

        public void InitListView()
        {
            ProgramsList = Programs.LoadPrograms();
            itemListBox.ItemsSource = ProgramsList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Programs.AddProgram();
            InitListView();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            Programs.LaunchProgram(itemListBox.SelectedItem.ToString());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Programs.DeleteProgram(itemListBox.SelectedIndex);
            InitListView();
        }

        private void itemListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Programs.LaunchProgram(itemListBox.SelectedItem.ToString());
        }

        private void itemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteButton.IsEnabled = itemListBox.SelectedIndex != 0? true:false;
            LaunchButton.IsEnabled = true;
        }
    }
}
