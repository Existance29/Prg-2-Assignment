// See https://aka.ms/new-console-template for more information
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================

using S10257381_PRG2Assignment;

//List of customers
string path = "customers.csv";


string[] c = File.ReadAllLines(path);
foreach (string line in c)
{
    Console.WriteLine(line);
}