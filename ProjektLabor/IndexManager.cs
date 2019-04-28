using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLabor
{
    class IndexManager
    {
        internal static ObservableCollection<SingleElement> initializeFullList()
        {
            ObservableCollection<SingleElement> list = new ObservableCollection<SingleElement>();
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
    }
}
