// See https://aka.ms/new-console-template for more information
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================

using S10257381_PRG2Assignment;

//List of customers
string path = "customers.csv";


//Test class console.write

Flavour fav = new Flavour("Vanilla", false, 1);
Topping top = new Topping("Sprinkles");
Cup cup = new Cup("Cup",2, new List<Flavour> {fav}, new List<Topping> {top});
Console.WriteLine(cup);