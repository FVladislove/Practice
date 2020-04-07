using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice
{
    public class Dish
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        
        public string ImageLink { get; set; }
        public override string ToString()
        {
            return "Name:" + this.Name + '\n'
                + "Category:" + this.Category + '\n'
                + "Description:" + this.Description + '\n'
                + "Price:" + this.Price + '\n'
                + "ImageLink:" + this.ImageLink;
        }
    }
}
