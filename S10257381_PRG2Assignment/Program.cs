// See https://aka.ms/new-console-template for more information
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================

using S10257381_PRG2Assignment;
using System.Net.Http.Headers;
//Get the directory of the program.cs file
//Since Directory.getCurrentDirectory() returns the net6.0 folder, change the string to get the PRG2Assignment folder
string curr_dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0","\\");

//read files in the same directory as program.cs, more convenient to place files in this directory than net6.0
//calls File.ReadAllLines, return a string array, each element represents one line of the file
string[] readLines(string f) 
{
    return File.ReadAllLines($"{curr_dir}{f}");
}

//customerDict stores all customers from customers.csv in a dictionary
//Key: customer's ID, value: customer object
Dictionary<string, Customer> customerDict = new Dictionary<string, Customer>();
//add all customers from customers.csv to customerList
string[] customerFile = readLines("customers.csv");
for (int i = 1; i < customerFile.Length; i++)
{
    string[] x = customerFile[i].Split(",");
    //create customer object
    Customer customer = new Customer(x[0], Convert.ToInt32(x[1]), Convert.ToDateTime(x[2]));
    //Create point card
    PointCard pointcard = new PointCard(Convert.ToInt32(x[4]), Convert.ToInt32(x[5]));
    pointcard.tier = x[3];
    //Assign point card to customer
    customer.rewards = pointcard;
    customerDict.Add(x[1], customer);
}

void printCustomers()
{
    //use the header from customer.csv and print it
    string[] header = customerFile[0].Split(",");
    Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5}", header[0], header[1], header[2], header[3], header[4], header[5]);
    //iterate through customerList and print the properties of customer and its associated point card
    foreach (Customer c in customerDict.Values)
    {
        Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5}", c.name, c.memberid, c.dob.ToString("MM-dd-yyyy"), c.rewards.tier, c.rewards.points, c.rewards.punchCard);
    }
}
//Get orders from orders.csv and add it to the customer's order history
//Rows with the same order id are merged into one order
string[] orderFile = readLines("orders.csv");
//a dictionary to store orders from the csv file
//key is in this format: orderID.MemberID, value is order object
//need to know which customer the order belongs to, so its stored in the dictionary's key
Dictionary<String, Order> orderCSVDict = new Dictionary<String, Order>();
for (int i = 1; i < orderFile.Length; i++)
{
    //store the values of the row into an array
    string[] x = orderFile[i].Split(",");
    //set the key of the dictionary
    string key = $"{x[0]}.{x[1]}";
    if (!orderCSVDict.ContainsKey(key)) //check if order is present
    {
        //create order object
        Order orderAdd = new Order(Convert.ToInt32(x[0]), Convert.ToDateTime(x[2]));
        orderAdd.TimeFulfilled = Convert.ToDateTime(x[3]);
        //add order to dict
        orderCSVDict.Add(key, orderAdd);  
    }
    //order object is present, add ice cream to order
    Order order = orderCSVDict[key];
    //store all toppings of the ice cream
    List<Topping> toppings = new List<Topping>();
    //iterate through toppings 1-4
    for (int j = 11; j <= 14; j++)
    {
        //if there is a topping, add it to the list
        if (x[j] != "") toppings.Add(new Topping(x[j])); 
    }

    //merge duplicate flavours together into one flavour object with the appropriate quantity
    //store flavours of the ice cream in a dictionary
    //key is flavour name, value is flavour object
    Dictionary<String, Flavour> flavours = new Dictionary<String, Flavour>();
    for (int j = 8; j <= 10; j++)
    {
        //check if there is a flavour there
        if (x[j] != "") 
        {
            //flavour is already stored
            if (!flavours.ContainsKey(x[j]))
            {
                flavours.Add(x[j], new Flavour(x[j], flavourHelper.isPremium(x[j]), 1));
            }
            else
            {
                //increment quantity
                flavours[x[j]].quantity++;
            }
        }
    }

    //make new ice cream subclass and add it to the order's ice cream list
    if (x[4] == "Cup")
    {
        order.AddIceCream(new Cup(x[4], Convert.ToInt32(x[5]), flavours.Values.ToList(), toppings));
    }
    else if (x[4] == "Cone")
    { 
        order.AddIceCream(new Cone(x[4], Convert.ToInt32(x[5]), flavours.Values.ToList(), toppings, x[6]=="TRUE"));
    }
    else
    {
        order.AddIceCream(new Waffle(x[4], Convert.ToInt32(x[5]), flavours.Values.ToList(), toppings, x[7]));
    }
}
//Add order to customer's order history
foreach (KeyValuePair<string, Order> kv in orderCSVDict)
{
    //get the associated customer ID from the key
    string customerID = kv.Key.Split('.')[1];
    //add the order to the customer orderHistory
    customerDict[customerID].orderHistory.Add(kv.Value);
}
//stores the ID of the next order
//assumes that the orders' id are incremental
//Account for the orders in orders.csv on initalisation
int currOrderID = orderCSVDict.Count+1;

//initalise gold and regular order queues
Queue<Order> goldQueue = new Queue<Order>();
Queue<Order> regularQueue = new Queue<Order>();
while (true)
{
    //Print menu
    Console.WriteLine("-------------MENU--------------");
    Console.WriteLine("[1] List all customers");
    Console.WriteLine("[2] List all current orders");
    Console.WriteLine("[3] Register a new customer");
    Console.WriteLine("[4] Create a customer’s order");
    Console.WriteLine("[5] Display order details of a customer");
    Console.WriteLine("[6] Modify order details");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("-------------------------------");
    Console.Write("Enter an option: ");
    string inp = Console.ReadLine();

    if (inp == "0")
    {
        break;
    }
    else if (inp == "1")
    {
        printCustomers();
    }
    else if (inp == "3")
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        int id = inputVal.getIntInput("ID Number: ");
        DateTime dob;
        //get only valid datetime input
        while (true)
        {
            try
            {
                Console.Write("Date of Birth (dd/mm/yyyy): ");
                dob = Convert.ToDateTime(Console.ReadLine());
                break; //if the input is correct, the break will trigger

            }
            catch (FormatException) //if its an invalid date-time format, prompt user again
            {
                Console.WriteLine("Invalid date format, try again");
            }

        }
        //create new customer object and assign a point card
        Customer newCustomer = new Customer(name, id, dob);
        newCustomer.rewards = new PointCard(0, 0);
        customerDict.Add(Convert.ToString(id), newCustomer);
        //add to customers.csv
        File.AppendAllLines($"{curr_dir}customers.csv", new string[] { $"{name},{id},{dob.ToString("dd/MM/yyyy")},Ordinary,0,0" });
        //Confirmation message
        Console.WriteLine("Regristration Successful!");


    }
    else if (inp == "4")
    {
        printCustomers();
        //since customerDict stores customers from .csv and gets updated, there is no need to read the csv file again
        string cselect = inputVal.getValuesInput("Customer ID Number:", customerDict.Keys.ToArray());
        //get the associated customer
        Customer customer = customerDict[cselect];
        //get the user's order -> prompt user for the options and ice creams
        //makeOrder returns the order object
        Order neworder = customer.MakeOrder();
        //link the new order to the customer's current order
        customer.currentOrder = neworder;
        //set the order's properties
        neworder.TimeReceived = DateTime.Now;
        neworder.id = currOrderID;
        //append the order to the appropriate queue
        //check customer's tier
        if (customer.rewards.tier == "Gold")
        { 
            goldQueue.Enqueue(neworder);
        }
        else
        {
            regularQueue.Enqueue(neworder);
        }
        //update the order ID
        currOrderID++;
        //confirmation message
        Console.WriteLine("Order has been successfully made!");
    }

    Console.WriteLine();

    if (inp == "2")
    {

    }
}
//#3

