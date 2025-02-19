using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI_Launch
{
    public class Item
    {
        public string name { get; set; }
        public string url { get; set; }
        public byte[] img { get; set; }
    }

    public class RootObject
    {

        public List<Item> Item { get; set; }
    }
    
}
