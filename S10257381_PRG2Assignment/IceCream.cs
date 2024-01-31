using S10257381_PRG2Assignment;
using System;
using System.Collections.Generic;
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
    abstract class IceCream
    {
        //Class properties
        public string option { get; set; }
        public int scoops { get; set; }
        public List<Flavour> flavours { get; set; }
        public List<Topping> toppings { get; set; }

        //Class constructor
        public IceCream() { }
        public IceCream(string o, int s, List<Flavour> f, List<Topping> t) 
        { 
            option = o;
            scoops = s;
            flavours = f;
            toppings = t;
        }

        //Since scoop and topping calculations are the same for all options (waffle is slightly different but can still be applied),
        //Perform the calculation in the superclass method, to be easily used by its subclasses
        //Use virtual as it is to be overrided by subclasses for their unique calculations
        public abstract double CalculatePrice();

        //Return values of class properties
        public override string ToString() {

            //Initalise variables to store the content of lists for the output
            //Use String.Join() method to concatenate the list elements with a comma seperator
            string flavoursOut = string.Join(", ", flavours);
            string toppingsOut = string.Join(", ", toppings);

            return $"Option: {option}\nScoops: {scoops}\nFlavours: {flavoursOut}\nToppings: {toppingsOut}";
        }
    }

    class Cup:IceCream
    {
        //Class constructors
        public Cup() { }
        public Cup(string o, int s, List<Flavour> f, List<Topping> t): base(o,s,f,t){ }

        public override double CalculatePrice()
        {
            //Calculate topping and scoop cost
            //Initalise output variable and calculate topping cost
            double finalout = toppings.Count;
            //Calculate scoop cost (number of scoops)
            if (scoops == 1)
            {
                finalout += 4;
            }
            else if (scoops == 2)
            {
                finalout += 5.5;
            }
            else
            {
                finalout += 6.5;
            }

            //Calculate scoop cost (flavour type)
            //Iterate though the flavours
            //For every premium flavour, +2 to the cost
            foreach (Flavour f in flavours)
            {
                if (f.premium)
                {
                    finalout += 2 * f.quantity;
                }
            }
            return finalout;
        }
    }

    class Cone: IceCream
    {
        //Additional class properity
        public bool dipped { get; set; }

        //Class constructors
        public Cone() { }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d) : base(o, s, f, t) 
        {
            dipped = d;
        }

        public override double CalculatePrice()
        {
            //Initalise output variable and calculate topping and scoop cost
            double finalout = toppings.Count;
            //Calculate scoop cost (number of scoops)
            if (scoops == 1)
            {
                finalout += 4;
            }
            else if (scoops == 2)
            {
                finalout += 5.5;
            }
            else
            {
                finalout += 6.5;
            }

            //Calculate scoop cost (flavour type)
            //Iterate though the flavours
            //For every premium flavour, +2 to the cost
            foreach (Flavour f in flavours)
            {
                if (f.premium)
                {
                    finalout += 2 * f.quantity;
                }
            }
            //Check if its chocolate-dipped and calculate cost accordingly
            if (dipped) 
            {
                finalout += 2;
            }
            return finalout;
        }
        public override string ToString()
        {
            string flavoursOut = string.Join(", ", flavours);
            string toppingsOut = string.Join(", ", toppings);
            string chocolatedipped = "";
            if (dipped) chocolatedipped = " (Chocolate-dipped)";
            return $"Option: {option}{chocolatedipped}\nScoops: {scoops}\nFlavours: {flavoursOut}\nToppings: {toppingsOut}";
        }


    }

    class Waffle : IceCream
    {
        //Additional class properity
        public string waffleFlavour { get; set; }

        //Class constructors
        public Waffle() { }
        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf) : base(o, s, f, t)
        {
            waffleFlavour = wf.ToLower(); //Set it to lowercase to avoid logic errors caused by capitalisation
        }

        public override double CalculatePrice()
        {
            //Initalise output variable and calculate topping cost
            double finalout = toppings.Count;
            //Calculate scoop cost (number of scoops)
            if (scoops == 1)
            {
                finalout += 7;
            }
            else if (scoops == 2)
            {
                finalout += 8.5;
            }
            else
            {
                finalout += 9.5;
            }

            //Calculate scoop cost (flavour type)
            //Iterate though the flavours
            //For every premium flavour, +2 to the cost
            foreach (Flavour f in flavours)
            {
                if (f.premium)
                {
                    finalout += 2 * f.quantity;
                }
            }

            //Check and calculate for Red velvet, charcoal, and pandan waffle flavours
            //Initalise a new array, storing the waffle flavours with extra cost
            //If the waffleFlavour is inside the array, account for the additional cost
            if (new string[] { "red velvet", "charcoal", "pandan" }.Contains(waffleFlavour))
            {
                finalout += 3;
            }
            return finalout;
        }

        public override string ToString()
        {
            string flavoursOut = string.Join(", ", flavours);
            string toppingsOut = string.Join(", ", toppings);
            return $"Option: {option} ({waffleFlavour})\nScoops: {scoops}\nFlavours: {flavoursOut}\nToppings: {toppingsOut}";
        }
    }

}
