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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupProject3280
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DatabaseManager Database = new DatabaseManager();    

        public MainWindow()
        {
            InitializeComponent();
        }

        private void debugItemsScreen_Click(object sender, RoutedEventArgs e) {
            Search.wndSearch search = new Search.wndSearch();
            search.ShowDialog();
        }

        private void debugSearchScreen_Click(object sender, RoutedEventArgs e) {
            Items.wndItems items = new Items.wndItems();
            items.ShowDialog();
        }
    }
}
