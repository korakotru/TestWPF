using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPF.Data
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
    public class ItemModelList : List<ItemModel> { }

}
