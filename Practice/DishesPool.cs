using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practice
{
    public class DishesPool
    {
        private static DishesPool instance;
        List<Dish> dishes;

        private DishesPool() { }

        public static DishesPool GetInstance()
        {
            if(instance == null)
            {
                instance = new DishesPool();
            }
            return instance;
        }

        public void AddDishes(List<Dish> dishes)
        {
            if(this.dishes == null)
            {
                this.dishes = dishes;
            }
            else
            {
                this.dishes.AddRange(dishes);
            }
        }

        public List<Dish> GetDishes()
        {
            return this.dishes;
        }

        public Dish[] GetRandomDishes()
        {
            Random r = new Random();
            Dish[] dishes = new Dish[r.Next(0, 5)];
            for(int i = 0; i < dishes.Length; i++)
            {
                dishes[i] = this.dishes[r.Next(0, this.dishes.Count)];
            }
            return dishes;
        }
    }
}
