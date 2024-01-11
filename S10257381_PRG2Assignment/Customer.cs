﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public Order MakeOrder()
        {
            Order orders = new Order();
            while (true)
            {
                
                Console.Write("Who is ordering? ");
                string customer = Console.ReadLine();

                Console.Write("Enter your Ice cream order");
                Console.Write("Option : ");
                string option = Console.ReadLine();
                if (option == "Cup")
                {
                    Console.Write("Scoops : ");
                    int scoops = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Flavours : ");
                    string flavours = Console.ReadLine();
                    Console.Write("Toppings : ");
                    string toppings = Console.ReadLine();
                    Cup cup = new Cup(option,scoops,flavours,toppings);
                }
                else if (option == "Cone")
                {
                    Console.Write("Scoops : ");
                    int scoops = Console.ReadLine();
                    Console.Write("Flavours : ");
                    string flavours = Console.ReadLine();
                    Console.Write("Toppings : ");
                    string toppings = Console.ReadLine();
                    Console.Write("Do you want it dipped? [Y/N] ");
                    bool dipped = Console.ReadLine();
                    Cone cone = new Cone(option, scoops, flavours, toppings, dipped);
                }
                else if (option == "Waffle")
                {
                    Console.Write("Scoops : ");
                    int scoops = Console.ReadLine();
                    Console.Write("Flavours : ");
                    string flavours = Console.ReadLine();
                    Console.Write("Toppings : ");
                    string toppings = Console.ReadLine();
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
