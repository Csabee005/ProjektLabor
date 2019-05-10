using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLabor
{
    class IndexManager : ISortList
    {
        ISortList sortList;
        public IndexManager(ISortList sortList)
        {
            this.sortList = sortList;
        }

        ObservableCollection<SingleElement> list;
        internal ObservableCollection<SingleElement> initializeFullList()
        {
            list = new ObservableCollection<SingleElement>();
            list.Add(new SingleElement("Cica"));
            list.Add(new SingleElement("Kutyus"));
            list.Add(new SingleElement("Nyuszi"));
            list.Add(new SingleElement("Póni"));
            list.Add(new SingleElement("Csacsi"));
            list.Add(new SingleElement("Maki"));
            list.Add(new SingleElement("Tigi"));
            list.Add(new SingleElement("Breki"));
            list.Add(new SingleElement("Füles"));
            list.Add(new SingleElement("Kacsusz"));
            list.Add(new SingleElement("Elefáni"));
            return list;
        }

        internal static ObservableCollection<SingleElement> shuffleNewElements(ObservableCollection<SingleElement> fullListBox)
        {
            ObservableCollection<SingleElement> randomList = new ObservableCollection<SingleElement>();
            Random random = new Random();
            int capacity = random.Next(2, fullListBox.Count);
            int sortRate = random.Next(1, fullListBox.Count/2);
            for (int i = 0; i < fullListBox.Count; i++)
            {
                if( (i%sortRate) == 0 && randomList.Count < capacity)
                {
                    randomList.Add(fullListBox[i]);
                }
            }
            return randomList;
        }

        internal void addNewElements(ObservableCollection<SingleElement> unorderedList, ObservableCollection<SingleElement> fullList)
        {
            Random random = new Random();
            int capacity = random.Next(2, fullList.Count);
            int sortRate = random.Next(1, fullList.Count / 2);
            for (int i = 0; i < fullList.Count; i++)
            {
                if ((i % sortRate) == 0 && unorderedList.Count < capacity)
                {
                    unorderedList.Add(fullList[i]);
                }
            } 
        }

        internal void insertAfterFirstDefined(SingleElement firstFull)
        {
            //MessageBox.Show("insertAfterFirstDefined" + firstFull);
            list.Remove(firstFull);
            int minIndex = -1;
            foreach (SingleElement element in list)
            {
                if(!(element.Index == -1))
                {
                    minIndex = element.Index;
                }
            }
            firstFull.Index = minIndex + 1;
            list.Insert(firstFull.Index, firstFull);
            list = sortList.listToBeSorted();
        }

        internal void insertAfterElement(SingleElement firstFull, SingleElement lastFull)
        {
            //MessageBox.Show("insertAfterElement" + firstFull + " || " + lastFull);
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(lastFull.Name))
                {
                    index = i;
                }
            }
            list.RemoveAt(index);
            lastFull.Index = firstFull.Index + 1;
            list.Insert(lastFull.Index, lastFull);
            list = sortList.listToBeSorted();
        }

        internal void insertAtFirstIndex(SingleElement firstFull)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(firstFull.Name))
                {
                    list.RemoveAt(i);
                }
            }
            list.Insert(0, firstFull);
            list = sortList.listToBeSorted();
        }

        public ObservableCollection<SingleElement> listToBeSorted()
        {
            return null;
        }

        internal void swapElement(SingleElement firstFull, SingleElement lastFull)
        {
            //MessageBox.Show("swapElement" + firstFull + " || " + lastFull);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(lastFull.Name))
                {
                    list.RemoveAt(i);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(firstFull.Name))
                {
                    list.RemoveAt(i);
                }
            }
            list.Insert(lastFull.Index, firstFull);
            list.Insert(lastFull.Index + 1, lastFull);
            list = sortList.listToBeSorted();
        }
    }
}
