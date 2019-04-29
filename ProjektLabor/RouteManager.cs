using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLabor
{
    class RouteManager : ObservableCollection<SingleElement>
    {
        public ObservableCollection<SingleElement> fullListBox { get; set; }
        public ObservableCollection<SingleElement> unorderedRandomListBox { get; set; }
        public ObservableCollection<SingleElement> orderedRandomListBox { get; set; }

        public RouteManager()
        {
            fullListBox = IndexManager.initializeFullList();
            unorderedRandomListBox = IndexManager.shuffleNewElements(fullListBox);
            orderedRandomListBox = new ObservableCollection<SingleElement>();
        }

        internal void insertElement(SingleElement selectedItem)
        {
            if(selectedItem != null)
            {
                unorderedRandomListBox.Remove(selectedItem);
                selectedItem.Index = orderedRandomListBox.Count;
                orderedRandomListBox.Add(selectedItem);
                if (unorderedRandomListBox.Count == 0)
                {
                    updateFullList();
                }
            }
            
        }

        private void updateFullList()
        {
            for (int i = 0; i < orderedRandomListBox.Count-1; i++)
            {
                SingleElement firstOrdered = orderedRandomListBox[i];
                SingleElement lastOrdered = orderedRandomListBox[i + 1];
                SingleElement firstFull = null, lastFull = null;
                int index = 0;
                while(firstFull == null || lastFull == null)
                {
                    if(firstFull == null)
                    {
                        firstFull = fullListBox[index].getElementByName(firstOrdered.Name);
                    }
                    if(lastFull == null)
                    {
                        lastFull = fullListBox[index].getElementByName(lastOrdered.Name);
                    }
                    index++;
                }
                MessageBox.Show("Elements that are being compared are " + firstFull + " and " + lastFull + ".");
                if (lastFull.isLater(firstFull))
                {
                    MessageBox.Show(firstFull + " is earlier than " + lastFull);
                }
            }
        }
    }
}
