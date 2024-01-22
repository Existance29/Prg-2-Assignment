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
            
            Order orders = new Order();
            while (true)
            {

                Console.Write("Enter your Ice cream order\n");
                string option = inputVal.getValuesInput("Option: ", new string[] { "cup", "cone", "waffle" }).ToLower();
                List<Flavour> flavours = new List<Flavour>();
                List<Topping> toppings = new List<Topping>();
                int scoops = 0;
                while (scoops < 3)
                {
                    string flavourName = inputVal.getValuesInput("Ice cream Flavour (enter 'done' to continue): ", new string[] { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt", "done" });
                    if (flavourName == "done")
                    {
                        if (scoops == 0)
                        {
                            Console.WriteLine("You need to order at least one scoop of ice cream.");
                            continue;
                        }
                        break;
                    }
                    int flavourQuantity;
                    while (true)
                    {
                        int FQ = inputVal.getIntInput("Quantity: ");
                        if (FQ <= 3 - scoops)
                        {
                            flavourQuantity = FQ;
                            scoops += FQ;
                            break;
                        }
                        Console.WriteLine("Invalid quantity of flavours! Please try again");

                    }
                    bool premium = flavourHelper.isPremium(flavourName);
                    flavours.Add(new Flavour(flavourName, premium, flavourQuantity));

                }
                while (true)
                {
                    string topping = inputVal.getValuesInput("Topping (enter 'done' to continue): ", new string[] { "sprinkles", "mochi", "sago", "oreos", "done" });
                    if (topping == "done")
                    {
                        break;
                    }
                    toppings.Add(new Topping(topping));
                }
                if (option == "cup")
                {
                    Cup c = new Cup(option, scoops, flavours, toppings);
                    Console.WriteLine(c);
                    orders.AddIceCream(c);
                }
                else if (option == "cone")
                {

                    bool dipped = inputVal.getValuesInput("Do you want to upgrade your cone to a chocolate-dipped one? [Y/N]: ", new string[] { "y", "n" }) == "y";
                    orders.AddIceCream(new Cone(option, scoops, flavours, toppings, dipped));
                }
                else
                {
                    Console.Write("Waffle flavour: ");
                    string wf = Console.ReadLine();
                    orders.AddIceCream(new Waffle(option, scoops, flavours, toppings, wf));
                }
                
                break;
                

            }
        }

        public void AddIceCream(IceCream iceCream)
        {
            iceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int id)
        {
            iceCreamList.RemoveAt(id);
            if (iceCreamList == null) //print message if there is no elements in the iceCreamList.
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
            string orders = string.Join("\n", iceCreamList);
            return $"ID: {id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}\nOrders:\n\n{orders}";
        }


    }
}
