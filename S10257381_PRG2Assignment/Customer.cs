using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        private int getIntInput(string prompt)
        {
            int op;
            while (true)
            {
                Console.Write(prompt);
                string inp = Console.ReadLine().ToLower();
                if (int.TryParse(inp, out op))
                {
                    break;

                }
                Console.WriteLine("Invalid Input! It has to be an integer. Please try again.\n");
            }
            return op;
        }

        private string getValuesInput(string prompt, string[] validArray)
        {
            string validString = string.Join(",", validArray);
            string op;
            while (true)
            {
                Console.Write(prompt);
                string inp = Console.ReadLine();
                if (validArray.Contains(inp))
                {
                    op = inp;
                    break;
                }
                Console.WriteLine($"Invalid Input! The valid values are {validString}. Please try again.\n");
            }
            return op;
        }

        public Order MakeOrder()
        {
            Order orders = new Order();
            while (true)
            {
                
                Console.Write("Who is ordering? ");
                string customer = Console.ReadLine();

                Console.Write("Enter your Ice cream order");
                string option = getValuesInput("Option: ", new string[] { "cup", "cone", "waffle" }).ToLower();
                int scoops = getIntInput("Scoops: ");
                List<Flavour> flavours = new List<Flavour>();
                List<Topping> toppings = new List<Topping>();
                for (int i = 0; i < scoops; i++)
                {
                    string flavourName = getValuesInput("Flavour: ", new string[] {"vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt"});
                    int flavourQuantity;
                    while (true)
                    {
                        int FQ = getIntInput("Quantity of Flavour: ");
                        if (FQ < scoops-i)
                        {
                            flavourQuantity = FQ;
                            break;
                        }
                        Console.WriteLine("Invalid quantity of flavours! Please try again");
                        
                    }
                    bool premium = false;
                    if (flavourQuantity > 0)
                    { 
                        
                    }
                    flavours.Add(new Flavour(flavourName, premium, flavourQuantity));
                    i += flavourQuantity - 1;
                    
                }
                while (true)
                {
                    string topping = getValuesInput("Topping (enter 'done' to continue):", new string[] { "sprinkles", "mochi", "sago", "oreos", "done"});
                    if (topping == "done")
                    {
                        break;
                    }
                    toppings.Add(new Topping(topping));
                }
                if (option == "cup")
                {
                    orders.AddIceCream(new Cup(option,scoops,flavours,toppings));
                }
                else if (option == "cone")
                {

                    Console.Write("Do you want it dipped? [Y/N] ");
                    bool dipped = Convert.ToBoolean(Console.ReadLine());
                    Cone cone = new Cone(option, scoops, flavours, toppings, dipped);
                }
                else
                {
                    Console.Write("Scoops : ");
                    int scoop = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Flavours : ");
                    string flavour = Console.ReadLine();
                    Console.Write("Toppings : ");
                    string topping = Console.ReadLine();
                    Console.Write("Waffle flavour : ");
                    string wf = Console.ReadLine();
                    Waffle waffle = new Waffle(option,scoops, flavours, toppings,wf);
                }
                


                Console.WriteLine("Would you like to add another ice cream to the order? [Y/N]".ToUpper());
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

            return orders;
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
