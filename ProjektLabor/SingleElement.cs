using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLabor
{
    class SingleElement : INotifyPropertyChanged, IComparable
    {
        string name;
        int index;

        public event PropertyChangedEventHandler PropertyChanged;

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
                OnPropertyChanged("Name");
            }
        }

        public int Index
        {
            get => index; set
            {
                this.index = value;
                OnPropertyChanged("Index");
            }
        }

        public override string ToString()
        {
            return String.Format("{0} | {1}", name, index);
        }

        internal bool isLater(SingleElement first)
        {
            if(index > first.index)
            {
                return true;
            }
            return false;
        }

        public SingleElement getElementByName(string name)
        { 
            if(this.name == name)
            {
                return this;
            }
            return null;
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal bool isEqual(SingleElement firstFull)
        {
            if(index == firstFull.index)
            {
                return true;
            }
            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            SingleElement otherElement = obj as SingleElement;
            if(this.index == -1)
            {
                return 1;
            }
            if (otherElement != null)
                return this.index.CompareTo(otherElement.index);
            else
            {
                throw new ArgumentException("Object is not a SingleElement!");
            }
        }
    }
}
