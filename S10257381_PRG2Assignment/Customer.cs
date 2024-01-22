using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{

    static class flavourHelper
    {
        //check if the flavour is a premium flavour, return true if true
        public static bool isPremium(string f)
        {   
            //tolower so it is not case sensitive
            return new string[] { "durian", "ube", "sea salt" }.Contains(f.ToLower());
        }
    }
    static class orderMaker
    {
        public static IceCream iceCream()
        {
            Console.Write("Enter your Ice cream order\n");
            string option = inputVal.getValuesInput("Option: ", new string[] { "cup", "cone", "waffle" }).ToLower();
            List<Flavour> flavours = new List<Flavour>();
            List<Topping> toppings = new List<Topping>();
            //store the number of scoops in an ice cream
            int scoops = 0;
            //prompt for flavour + scoops, until 3 scoops
            while (scoops < 3)
            {
                string flavourName = inputVal.getValuesInput("Ice cream Flavour (enter 'done' to continue): ", new string[] { "vanilla", "chocolate", "strawberry", "durian", "ube", "sea salt", "done" });
                if (flavourName == "done")
                {
                    //ensure that an ice cream has at least one scoop
                    if (scoops == 0)
                    {
                        Console.WriteLine("You need to order at least one scoop of ice cream.");
                        continue;
                    }
                    break;
                }
                int flavourQuantity;
                //make sure that there is a max of 3 scoops
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
                return new Cup(option, scoops, flavours, toppings);
            }
            else if (option == "cone")
            {

                bool dipped = inputVal.getValuesInput("Do you want to upgrade your cone to a chocolate-dipped one? [Y/N]: ", new string[] { "y", "n" }) == "y";
                return new Cone(option, scoops, flavours, toppings, dipped);
            }
            else
            {
                Console.Write("Waffle flavour: ");
                string wf = Console.ReadLine();
                return new Waffle(option, scoops, flavours, toppings, wf);
            }
        }
    }
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
            string validString = string.Join(", ", validArray);
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
            orderHistory = new List<Order>();
        }

        public Order MakeOrder()
        {
            Order orders = new Order();
            orders.AddIceCream(orderMaker.iceCream());
            while (true)
            {  
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
            return DateTime.Now.Date == dob.Date;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
