using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }
    }
}
