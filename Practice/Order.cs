using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice
{
    public class Order
    {
        string uuid;
        Dish[] dishes;
        int cashBox;

        public Order(string uuid, Dish[] dishes, int cashBox)
        {
            this.uuid = uuid;
            this.dishes = dishes;
            this.cashBox = cashBox;
        }
        public string GetUUID()
        {
            return uuid;
        }
    }
}
