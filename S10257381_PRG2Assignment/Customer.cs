using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    //Methods for input validation - prompt user and evaluate input, return only valid inputs
    static class inputVal
    {
        //gets only integer inputs, returns integer
        public static int getIntInput(string prompt)
        {
            int op;
            while (true)
            {
                Console.Write(prompt); //get prompt from yser
                string inp = Console.ReadLine();
                if (int.TryParse(inp, out op)) //check if input is an integer
                {
                    break;

                }
                //continue asking the user
                Console.WriteLine("Invalid Input! It has to be an integer. Please try again.\n");
            }
            return op;
        }

        //checks if a string input falls within set values, return the valid input
        //all elements in validArray will have to be lowercase, since input will also be converted to lower case.
        public static string getValuesInput(string prompt, string[] validArray)
        {
            //convert the array to a string for outputting the valid options
            string validString = string.Join(",", validArray);
            string op;
            while (true)
            {
                Console.Write(prompt);
                string inp = Console.ReadLine().ToLower(); //set the input to lowercase, inputs are not case-sensetive
                if (validArray.Contains(inp)) //check if input is correct
                {
                    op = inp; 
                    break;
                }
                //input invalid, loop again
                Console.WriteLine($"Invalid Input! The valid values are {validString}. Please try again.\n");
            }
            return op;
        }
    }
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
            Order orders = new Order();
            while (true)
            {
                
                Console.Write("Who is ordering? ");
                string customer = Console.ReadLine();

                Console.Write("Enter your Ice cream order");
                string option = inputVal.getValuesInput("Option: ", new string[] { "cup", "cone", "waffle" }).ToLower();
                List<Flavour> flavours = new List<Flavour>();
                List<Topping> toppings = new List<Topping>();
                int scoops = 0;
                while (scoops <= 3)
                {
                    string flavourName = inputVal.getValuesInput("Ice cream Flavour (enter 'done' to continue): ", new string[] {"vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt", "done"});
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
                        if (FQ <= 3-scoops)
                        {
                            flavourQuantity = FQ;
                            scoops += FQ;
                            break;
                        }
                        Console.WriteLine("Invalid quantity of flavours! Please try again");
                        
                    }
                    bool premium = new string[] { "durain", "ebe", "sea salt" }.Contains(flavourName.ToLower());
                    flavours.Add(new Flavour(flavourName, premium, flavourQuantity));
                    
                }
                while (true)
                {
                    string topping = inputVal.getValuesInput("Topping (enter 'done' to continue):", new string[] { "sprinkles", "mochi", "sago", "oreos", "done"});
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

                    Console.Write("Do you want it dipped? [Y/N]: ");

                    bool dipped = inputVal.getValuesInput("Do you want to upgrade your cone to a chocolate-dipped one? [Y/N]: ", new string[] { "y", "n" }) == "y";
                    orders.AddIceCream(new Cone(option, scoops, flavours, toppings, dipped));
                }
                else
                {
                    Console.Write("Waffle flavour: ");
                    string wf = Console.ReadLine();
                    orders.AddIceCream(new Waffle(option,scoops, flavours, toppings,wf));
                }
                


                string again = inputVal.getValuesInput("Would you like to add another ice cream to the order? [Y/N]", new string[] {"y","n"}).ToUpper();

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
