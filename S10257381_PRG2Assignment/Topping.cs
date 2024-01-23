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
    class Topping
    {
        //Class property
        public string type { get; set; }

        //Class Constructor
        public Topping() { }
        public Topping(string t) 
        {
            type = t;
        }

        //Return value of class property
        public override string ToString()
        {
            return $"{type}";
        }
    }
}
