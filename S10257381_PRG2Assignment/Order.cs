using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
	class Order: IceCream
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

		public void ModifyIceCream(int id)
		{

		}

		public void AddIceCream(IceCream iceCream)
		{

		}

		public void DeleteIceCream(int id)
		{

		}

		public double CalculateTotal()
		{

		}

        public override string ToString()
        {
            return base.ToString();
        }


    }
}
