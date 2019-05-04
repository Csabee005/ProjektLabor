using System.Collections.ObjectModel;

namespace ProjektLabor
{
    internal interface ISortList
    {
        ObservableCollection<SingleElement> listToBeSorted();
    }
}