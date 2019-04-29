using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int minIndex = -1;
            foreach (SingleElement element in list)
            {
                if(!(element.Index == -1))
                {
                    minIndex = element.Index;
                }
            }
            list.Remove(firstFull);
            firstFull.Index = minIndex + 1;
            list.Insert(firstFull.Index, firstFull);
        }

        internal void insertAfterElement(SingleElement firstFull, SingleElement lastFull)
        {
            list.Remove(lastFull);
            lastFull.Index = firstFull.Index + 1;
            list.Insert(lastFull.Index, lastFull);
        }

        public void listToBeSorted()
        {
            throw new NotImplementedException();
        }
    }
}
