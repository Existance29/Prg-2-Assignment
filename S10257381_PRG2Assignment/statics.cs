using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//this contains all static classes used
namespace S10257381_PRG2Assignment
{
    static class flavourHelper
    {
        //check if the flavour is a premium flavour, return true if true
        public static string[] premiumFlavours { get; set; }
        public static bool isPremium(string f)
        {
            //tolower so it is not case sensitive
            return premiumFlavours.Contains(f.ToLower());
        }
    }
    //this class is used to store toppings, flavour information and options as properties
    static class IceCreamData
    {
        //store all flavours in a dictionary
        //key: flavour name, value: flavour cost
        public static Dictionary<string, double> flavours = new Dictionary<string, double>();
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
                Console.Write(prompt); //get prompt from user
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
                if (validArray.Contains(inp) || inp == "done") //check if input is correct. Done is a universally-accepted word, used for quitting menus
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
                string flavourName = inputVal.getValuesInput("Ice cream Flavour (enter 'done' to continue): ", IceCreamData.flavours.Keys.ToArray());
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
                //validate user input
                while (true)
                {
                    int FQ = inputVal.getIntInput("Quantity: ");
                    //make sure that there is a max of 3 scoops
                    if (FQ <= 3 - scoops)
                    {
                        flavourQuantity = FQ;
                        scoops += FQ;
                        break;
                    }
                    Console.WriteLine("Invalid quantity of flavours! Please try again");

                }
                //add flavour object to the list
                bool premium = flavourHelper.isPremium(flavourName);
                flavours.Add(new Flavour(flavourName, premium, flavourQuantity));

            }
            //get toppings. Only allow for a maximum of 4 toppings
            for (int i = 0; i < 4; i++)
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
}
