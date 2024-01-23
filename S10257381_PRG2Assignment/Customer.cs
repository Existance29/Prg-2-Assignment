using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================
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
            orderHistory = new List<Order>();
        }

        public Order MakeOrder()
        {
            Order orders = new Order();
            while (true)
            {
                orders.AddIceCream(orderMaker.iceCream());
                string again = inputVal.getValuesInput("Would you like to add another ice cream to the order? [Y/N]: ", new string[] {"y","n"}).ToUpper();

                if (again == "Y")
                {
                    continue;
                }
                else if (again == "N")
                {
                    break;
                }
                
            }

            return orders;
        }

        public bool Isbirthday()
        {
            return DateTime.Now.ToString("dd/MM") == dob.ToString("dd/MM");
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
