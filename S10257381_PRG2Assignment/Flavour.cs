using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================
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
            string displaypremium = "";
            if (premium) displaypremium = " (premium)";
            return $"{type}{displaypremium} {quantity}";
        }
    }
}
