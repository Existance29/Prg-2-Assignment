using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257381_PRG2Assignment
{
    class PointCard
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
            tier = "Ordinary"; //defaults to ordinary tier
        }

        public void AddPoints(int p)
        {
            points += p;
            //Increase their tier based on their new points
            if (points >= 100)
            {
                tier = "Gold";
            }
            else if (points >= 50 && tier != "Gold") { //do not allow for tier dropping
                tier = "Silver";
            }
        }

        public void RedeemPoints(int p)
        {
            //Only gold and silver members can redeem
            if (tier == "Gold" || tier == "Silver")
            {
                points -= p;
            }
        }

        public void Punch()
        {
            //increase the punchCard counter
            //Logic for free ice cream will be handled by main program
            punchCard++;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
