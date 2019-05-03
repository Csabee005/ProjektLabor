using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLabor
{
    class RouteManager : ObservableCollection<SingleElement>, ISortList
    {
        Boolean processDestinations;
        public ObservableCollection<SingleElement> fullListBox { get; set; }

        public ObservableCollection<SingleElement> unorderedRandomListBox { get; set; }

        public ObservableCollection<SingleElement> orderedRandomListBox { get; set; }

        IndexManager indexManager;

        public RouteManager()
        {
            processDestinations = true;
            indexManager = new IndexManager(this);
            fullListBox = indexManager.initializeFullList();
            unorderedRandomListBox = IndexManager.shuffleNewElements(fullListBox);
            orderedRandomListBox = new ObservableCollection<SingleElement>();
        }

        public void continueDestinationProcess()
        {
            indexManager.addNewElements(unorderedRandomListBox, fullListBox);
        }

        internal void insertElement(SingleElement selectedItem)
        {
            if(selectedItem != null)
            {
                unorderedRandomListBox.Remove(selectedItem);
                SingleElement newItem = new SingleElement(selectedItem.Name, orderedRandomListBox.Count);
                orderedRandomListBox.Add(newItem);
                if (unorderedRandomListBox.Count == 0)
                {
                    updateFullList();
                    orderedRandomListBox.Clear();
                    if (processDestinations)
                    {
                        continueDestinationProcess();
                    }
                }
            }
        }

        private void updateFullList()
        {
            for (int i = 0; i < orderedRandomListBox.Count - 1; i++)
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

                    if (lastFull == null && index > orderedRandomListBox.Count)
                    {
                        lastFull = lastOrdered;
                    }
                }
                //MessageBox.Show("Elements that are being compared: " + firstFull + " and " + lastFull + ".");
                if(lastFull.isLater(firstFull))
                {
                    if(lastFull.Index == -1)
                    {
                        indexManager.insertAfterElement(firstFull, lastFull);
                    }
                    //MessageBox.Show(firstFull + " is before " + lastFull + ", so inserting before it, at index " + firstFull.Index + 1);
                }
                else if (lastFull.isEqual(firstFull))
                {
                    //MessageBox.Show(firstFull + "'s index is equal to " + lastFull + "'s index, inserting the first!");
                    indexManager.insertAfterFirstDefined(firstFull);
                }
                else
                {
                    if(lastFull.Index != -1)
                        indexManager.swapElement(firstFull, lastFull);
                    else
                    {
                        indexManager.insertAfterElement(firstFull, lastFull);
                    }
                    //MessageBox.Show(firstFull + " is later than " + lastFull + ", so inserting " + lastFull.Name + " before " + firstFull.Name);
                }
                listToBeSorted();
            }
            checkLastElement();
        }


        private void checkLastElement()
        {
            SingleElement firstOrdered = orderedRandomListBox[orderedRandomListBox.Count-2];
            SingleElement lastOrdered = orderedRandomListBox[orderedRandomListBox.Count-1];
            SingleElement firstFull = null, lastFull = null;
            int index = 0;
            while (firstFull == null || lastFull == null)
            {
                if (firstFull == null)
                {
                    firstFull = fullListBox[index].getElementByName(firstOrdered.Name);
                }
                if (lastFull == null)
                {
                    lastFull = fullListBox[index].getElementByName(lastOrdered.Name);
                }
                index++;
            }
            if(lastFull.Index == -1)
            {
                indexManager.insertAfterElement(firstFull,lastFull);
            }else if (!lastFull.isLater(firstFull))
            {
                indexManager.swapElement(firstFull, lastFull);
            }
            listToBeSorted();
        }

        public void listToBeSorted()
        {
            int index = 0;
            foreach (SingleElement element in fullListBox)
            {
                if(element.Index != -1)
                {
                    element.Index = index++;
                }
            }
            ArrayList elements = new ArrayList(fullListBox.ToArray());
            elements.Sort();
            fullListBox.Clear();
            foreach (SingleElement element in elements)
            {
                fullListBox.Add(element);
            }
        }
    }
}
