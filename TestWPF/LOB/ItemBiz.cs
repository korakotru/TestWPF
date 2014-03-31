using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Data;

namespace TestWPF.LOB
{
    public class ItemBiz
    {
        public int Count()
        {
            ItemModelList obj = new ItemModelList();
            return obj.Count();
        }

        public ItemModelList GetAll()
        {
            ItemModelList items = new ItemModelList();
            // MOCKUP: Generate data for return
            for (int i = 0; i < 100; i++)
            {
                items.Add(new ItemModel() {Id=i,ItemName="item "+i,Description="Desc "+i, Cost=i, Price= i+1, Qty=i });
            }
            return items;
        }
    }
}
