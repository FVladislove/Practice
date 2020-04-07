using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice
{
    public class OrdersQueue
    {
        private static OrdersQueue instance;
        private OrdersQueue() { }
        public static OrdersQueue GetInstance()
        {
            if(instance == null)
            {
                instance = new OrdersQueue();
            }
            return instance;
        }

        public static Order CreateRandomOrder()
        {
            var dishes = DishesPool.GetInstance();
            Random r = new Random();
            return new Order(Guid.NewGuid().ToString(), dishes.GetRandomDishes(), r.Next(0, 8));
        }
    }
}
