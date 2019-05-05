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

namespace ProjektLabor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RouteManager manager;

        public MainWindow()
        {
            InitializeComponent();
            manager = new RouteManager();
            DataContext = manager;
        }

        private void UserInsertion(object sender, SelectionChangedEventArgs e)
        {
            //manager.insertElement((SingleElement)((ListBox)sender).SelectedItem);
        }

        private void BtnStartOrdering_Click(object sender, RoutedEventArgs e)
        {
            manager.updateFullList();
            manager.orderedRandomListBox.Clear();
            manager.continueDestinationProcess();
        }

        private void BtnAddElement_Click(object sender, RoutedEventArgs e)
        {
            SingleElement selectedItem = (SingleElement)listUnorderedRandomElements.SelectedItem;
            if(selectedItem != null)
            {
                manager.insertElement(selectedItem);
            }
        }

        private void BtnRemoveElement_Click(object sender, RoutedEventArgs e)
        {
            SingleElement selectedItem = (SingleElement)listUnorderedRandomElements.SelectedItem;
            if (selectedItem != null)
            {
                manager.removeElement(selectedItem);
            }
        }

        private void BtnResetList_Click(object sender, RoutedEventArgs e)
        {
            manager.resetUnorderedList();
        }
    }
}
