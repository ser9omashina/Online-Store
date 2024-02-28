using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWpfApp1.Model
{
    public class PcComponents
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int CountCopies { get; set; }
        public PcComponents(int id, string manufacturer, string title, string type, int price, int countcopies)
        {
            Id = id;
            Manufacturer = manufacturer;
            Title = title;
            Type = type;  
            Price = price;
            CountCopies = countcopies;

        }

    }
}

