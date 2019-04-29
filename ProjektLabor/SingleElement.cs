using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLabor
{
    class SingleElement
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
            }}

        public int Index
        {
            get => index; set
            {
                this.index = value;
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
    }
}
