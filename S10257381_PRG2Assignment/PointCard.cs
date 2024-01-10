using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    class PointCard:Customer
    {
        //Properties
        public int points { get; set; }

        public int punchCard { get; set; }

        public string tier { get; set; }

        //Constructors

        public PointCard() { }

        public PointCard(int p, int pc)
        {
            points = p;
            punchCard = pc;
        }

        public void AddPoints(int points)
        {
            
        }

        public void RedeemPoints(int points)
        {

        }

        public void Punch()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
