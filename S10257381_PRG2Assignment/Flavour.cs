using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    class Flavour
    {
        //Class properties
        public string type { get; set; }
        public bool premium { get; set; }
        public int quantity { get; set; }

        //Class constructor
        public Flavour() { }
        public Flavour(string t, bool p, int q) 
        {
            type = t;
            premium = p;
            quantity = q;
        }

        //Return values of class properties
        public override string ToString()
        {
            return $"Type: {type}, Premium: {premium}, Quantity: {quantity}";
        }
    }
}
