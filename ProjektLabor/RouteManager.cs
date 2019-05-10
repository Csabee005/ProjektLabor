using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjektLabor
{
    class RouteManager : ObservableCollection<SingleElement>, ISortList
    {
        public ObservableCollection<SingleElement> fullListBox { get; set; }

        public ObservableCollection<SingleElement> unorderedRandomListBox { get; set; }

        public ObservableCollection<SingleElement> orderedRandomListBox { get; set; }

        private List<SingleElement> backupList;
        public Button canUpdate { get; set; }

        IndexManager indexManager;

        public RouteManager()
        {
            indexManager = new IndexManager(this);
            fullListBox = indexManager.initializeFullList();
            unorderedRandomListBox = IndexManager.shuffleNewElements(fullListBox);
            backupList = unorderedRandomListBox.ToList();
            orderedRandomListBox = new ObservableCollection<SingleElement>();
            canUpdate = new Button();
            canUpdate.IsEnabled = false;
        }

        public void continueDestinationProcess()
        {
            indexManager.addNewElements(unorderedRandomListBox, fullListBox);
            backupList = unorderedRandomListBox.ToList();
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
                    canUpdate.IsEnabled = true;
                }
            }
        }

        internal void removeElement(SingleElement selectedItem)
        {
            unorderedRandomListBox.Remove(selectedItem);
            if (unorderedRandomListBox.Count == 0)
            {
                canUpdate.IsEnabled = true;
            }
        }

        public void updateFullList()
        {
            canUpdate.IsEnabled = false;
            bool madeChange = false;
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
                        lastFull.Index = orderedRandomListBox.Count + 1;
                    }
                }
                //MessageBox.Show("Elements that are being compared: " + firstFull + " and " + lastFull + ".");
                if(firstFull.Index == -1)
                {
                    indexManager.insertAfterFirstDefined(firstFull);
                    madeChange = true;
                    i--;
                }
                else if (lastFull.Index == -1)
                {
                    indexManager.insertAfterElement(firstFull, lastFull);
                    madeChange = true;
                }
                else if (lastFull.isEqual(firstFull))
                {
                    //MessageBox.Show(firstFull + "'s index is equal to " + lastFull + "'s index, inserting the first!");
                    indexManager.insertAfterFirstDefined(firstFull);
                    madeChange = true;
                }
                else if (lastFull.Index != -1 && firstFull.isLater(lastFull))
                {
                    indexManager.swapElement(firstFull, lastFull);
                    madeChange = true;
                }
                else if (firstFull.isLater(lastFull))
                {
                    indexManager.insertAfterElement(firstFull, lastFull);
                    madeChange = true;
                }
                else
                {
                    if (lastFull.Index == -1)
                    {
                        indexManager.insertAfterElement(firstFull, lastFull);
                        madeChange = true;
                    }
                    else if (lastFull.isEqual(firstFull))
                    {
                        //MessageBox.Show(firstFull + "'s index is equal to " + lastFull + "'s index, inserting the first!");
                        indexManager.insertAfterFirstDefined(firstFull);
                        madeChange = true;
                    }
                    else if (firstFull.isLater(lastFull) && lastFull.Index != -1)
                    {
                        indexManager.swapElement(firstFull, lastFull);
                        madeChange = true;
                    }
                    else if (firstFull.isLater(lastFull))
                    {
                        indexManager.insertAfterElement(firstFull, lastFull);
                        madeChange = true;
                    }
                }
                //MessageBox.Show(firstFull + " is later than " + lastFull + ", so inserting " + lastFull.Name + " before " + firstFull.Name);
                listToBeSorted();
            }
            //checkLastElement(madeChange);
            if (madeChange)
                updateFullList();
        }

        public ObservableCollection<SingleElement> listToBeSorted()
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
            return fullListBox;
        }

        public void resetUnorderedList()
        {
            canUpdate.IsEnabled = false;
            unorderedRandomListBox.Clear();
            orderedRandomListBox.Clear();
            foreach (SingleElement element in backupList)
            {
                unorderedRandomListBox.Add(element);
            }
        }
    }
}
