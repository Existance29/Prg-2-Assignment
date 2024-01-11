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

		public Order() { }

		public Order(int i, DateTime tr)
		{
			id = i;
			timeReceived = tr;
		}

		public void ModifyIceCream(int id, IceCream modifiedIcecream)
		{
			iceCreamList[id].option = modifiedIcecream.option;
			iceCreamList[id].scoops = modifiedIcecream.scoops;
			iceCreamList[id].flavours = modifiedIcecream.flavours;
			iceCreamList[id].toppings = modifiedIcecream.toppings;
		}

		public void AddIceCream(IceCream iceCream)
		{
			iceCreamList.Add(iceCream);
		}

		public void DeleteIceCream(int id)
		{
			iceCreamList.RemoveAt(id);
			if (iceCreamList  == null ) //print message if there is no elements in the iceCreamList.
			{
				Console.WriteLine("You cannot have 0 Ice creams in an order.");
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
            return base.ToString();
        }


    }
}
