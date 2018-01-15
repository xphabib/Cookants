using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PosCookents.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
    }
}