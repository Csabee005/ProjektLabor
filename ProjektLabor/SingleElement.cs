using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLabor
{
    class SingleElement : INotifyPropertyChanged
    {
        string name;
        int index;

        public SingleElement(string name)
        {
            this.name = name;
            this.index = -1;
        }
        public SingleElement(string name, int index)
        {
            this.name = name;
            this.index = index;
        }

        public string Name { get => name; set {
                this.name = value;
                onPropertyChanged(this, "name");
            }}

        public int Index { get => index; set {
                this.index = value;
                onPropertyChanged(this, "index");
            }}

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return String.Format("{0,-20} at index: {1,5}", name, index);
        }
    }
}
