using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    class Order
    {

        //Properties
        public int id { get; set; }

        private DateTime timeReceived;

        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }

        private DateTime? timeFulfilled;

        public DateTime? TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }

        public List<IceCream> iceCreamList { get; set; }

        //Constructors

        public Order()
        {
            iceCreamList = new List<IceCream>();
        }

        public Order(int i, DateTime tr)
        {
            id = i;
            timeReceived = tr;
            iceCreamList = new List<IceCream>();
        }

        public void ModifyIceCream(int id)
        {
            //removes icecream
            iceCreamList.RemoveAt(id);
            //adds in the new order created with the orderMaker class
            iceCreamList.Insert(id,orderMaker.iceCream());
        }

        public void AddIceCream(IceCream iceCream)
        {
            iceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int id)
        {
            if (iceCreamList == null) //print message if there is no elements in the iceCreamList.
            {
                Console.WriteLine("You cannot have 0 Ice creams in an order.");
            }
            //confirmation message to indicate that the icecream is removed
            else
            {
                iceCreamList.RemoveAt(id);
                Console.WriteLine("Ice cream removed!");
                if (iceCreamList.Count == 0)
                {
                    iceCreamList = null;
                }
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream icecream in iceCreamList)
            {
                double itemprice = icecream.CalculatePrice();
                total += itemprice;
            }
            return total;
        }

        public override string ToString()
        {
            string orders = string.Join("\n", iceCreamList);
            return $"ID: {id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}\nOrders:\n\n{orders}";
        }


    }
}
