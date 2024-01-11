using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    class Customer
    {
        //Properties

        public string name { get; set; }

        public int memberid { get; set; }

        public DateTime dob { get; set; }

        public Order currentOrder { get; set; }

        public List<Order> orderHistory { get; set; }

        public PointCard rewards { get; set; }

        //Constructors

        public Customer() { }

        public Customer(string n,int mid,DateTime d)
        {
            name = n;
            memberid = mid;
            dob = d;
        }

        public Order MakeOrder()
        {
            while (true)
            {
                Order orders = new Order();
                IceCream obj = new IceCream(option, scoops, flavours, toppings);
                orders.AddIceCream(obj);

                Console.WriteLine("Would you like to add another ice cream to the order? ".ToUpper());
                string again = Console.ReadLine();

                if (again == "Y")
                {
                    continue;
                }
                else if (again == "N")
                {
                    break;
                }
            }

        }

        public bool Isbirthday()
        {
            return true;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
