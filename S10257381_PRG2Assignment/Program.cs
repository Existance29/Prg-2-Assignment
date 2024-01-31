// See https://aka.ms/new-console-template for more information
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================

using S10257381_PRG2Assignment;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
//Get the directory of the program.cs file
//Since Directory.getCurrentDirectory() returns the net6.0 folder, change the string to get the PRG2Assignment folder
string curr_dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0", "\\");

//read files in the same directory as program.cs, more convenient to place files in this directory than net6.0
//calls File.ReadAllLines, return a string array, each element represents one line of the file
string[] readLines(string f)
{
    try
    {
        return File.ReadAllLines($"{curr_dir}{f}");
    }
    catch(FileNotFoundException e)
    { 
        Console.WriteLine($"File Not found: {f}");
        System.Environment.Exit(1);
        return new string[] {}; // 
    }

}
//customerDict stores all customers from customers.csv in a dictionary
//Key: customer's ID, value: customer object
Dictionary<string, Customer> customerDict = new Dictionary<string, Customer>();
string[] customerFile = readLines("customers.csv");
//this varible is meant to store the length of customers in the customers.csv file
//this is used with customerDict to add any new customers to add to the csv file on exit
int customerCSVLength = customerFile.Length;
//add all customers from customers.csv to customerList
for (int i = 1; i < customerCSVLength; i++)
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

//store premium flavours
List<string> pFlavours = new List<string>();
//store all flavours in a dictionary
//key: flavour name, value: flavour cost
Dictionary<string, double> flavourData = new Dictionary<string, double>();
//get a list of flavours from flavour.csv
string[] flavourFile = readLines("flavours.csv");
for (int i = 1; i < flavourFile.Length; i++)
{
    string[] x = flavourFile[i].Split(",");
    double cost = Convert.ToDouble(x[1]);
    //assume that all non-free flavours are premium
    //if its premium, add it to pFlavours
    if (cost > 0)
    {
        pFlavours.Add(x[0].ToLower());
    }
    flavourData.Add(x[0].ToLower(), cost);
}
//update the appropriate static classes
flavourHelper.premiumFlavours = pFlavours.ToArray();
IceCreamData.flavours = flavourData;

//store all toppings in a dictionary
Dictionary<string, double> toppingData = new Dictionary<string, double>();
//read the toppingsCSV file
string[] toppingFile = readLines("toppings.csv");
for (int i = 1; i < toppingFile.Length; i++)
{
    string[] x = toppingFile[i].Split(",");
    double cost = Convert.ToDouble(x[1]);
    toppingData.Add(x[0].ToLower(), cost);
}
//update the appropriate static classes

IceCreamData.toppings = toppingData;
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
        order.AddIceCream(new Cone(x[4], Convert.ToInt32(x[5]), flavours.Values.ToList(), toppings, x[6] == "TRUE"));
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
int currOrderID = orderCSVDict.Count + 1;

//initalise gold and regular order queues
Queue<Order> goldQueue = new Queue<Order>();
Queue<Order> regularQueue = new Queue<Order>();

//Add a list used to store ice creams ordered
//it will be added to orders.csv
List<IceCream> orderedIceCreams = new List<IceCream>();

void appendToOrdercsv()
{

    //Add a list to store new orders that have been fulfilled
    List<string> fulfilledOrder = new List<string>();
    List<string> toppingAdd = new List<string>();
    List<string> flavoursAdd = new List<string>();
    
    //append orders to order.csv
    //loop through each customer and each order
    foreach (Customer customer in customerDict.Values)
    {
        foreach (Order order in customer.orderHistory)
        {
            List<IceCream> iceCreams = order.iceCreamList;
            if (order.TimeFulfilled.HasValue)
            {
                for (int i = 0; i < iceCreams.Count; i++)
                {
                    List<Flavour> flavourList = iceCreams[i].flavours;
                    //get toppings
                    foreach (Topping t in iceCreams[i].toppings)
                    {
                        toppingAdd.Add(t.ToString());
                    }
                    //get flavours
                    foreach (Flavour f in flavourList)
                    {
                        for (int j = 0; j < f.quantity; j++)
                        {
                            flavoursAdd.Add(f.type);
                        }
                    }
                    //add blanks so that flavoursAdd will always be length 3 to avoid messing up the csv format
                    for (int k = 0; i < 3 - flavourList.Count; i++)
                    {
                        flavoursAdd.Add("");
                    }
                    string dipped = "";
                    string waffleFlavour = "";
                    string option = iceCreams[i].option;
                    if (iceCreams[i] is Cone)
                    {
                        Cone coneCast = (Cone)iceCreams[i];
                        dipped = coneCast.dipped.ToString();
                    } 
                    else if (option.ToLower() == "waffle") 
                    {
                        Waffle waffleCast = (Waffle)iceCreams[i];
                        waffleFlavour = waffleCast.waffleFlavour;
                    }
                    string format = $"{order.id},{customer.memberid},{order.TimeReceived.ToString("dd/MM/yyyy HH:mm")},{order.TimeFulfilled.Value.ToString("dd/MM/yyyy HH:mm")},{option},{iceCreams[i].scoops},{dipped},{waffleFlavour},{string.Join(",",flavoursAdd)},{string.Join(",",toppingAdd)}";
                    fulfilledOrder.Add(format);
                    flavoursAdd.Clear();
                    toppingAdd.Clear();
                }


            }

        }
    }


    File.AppendAllLines($"{curr_dir}orders.csv", fulfilledOrder);


}


void advancedA()
{
    //Initialise the variable to store the order object
    Order order;
    //process gold queue orders before regular queue
    //check if gold queue is empty, then get the order from regular queue
    if (goldQueue.Count != 0)
    {
        order = goldQueue.Dequeue();
    }
    else if (regularQueue.Count != 0)
    {
        order = regularQueue.Dequeue();
    }
    else
    {
        Console.WriteLine("There are no orders to process!");
        return;
    }
    //output the ice creams
    Console.WriteLine("Ice Creams");
    Console.WriteLine(string.Join("\n\n", order.iceCreamList));
    //Calculate and display the total bill
    double total = order.CalculateTotal();
    Console.WriteLine($"Total Bill: ${total.ToString("0.00")}");
    //Find the customer associated with the order
    Customer? customer = null;
    foreach (Customer c in customerDict.Values.ToArray())
    {
        if (c.currentOrder != null && c.currentOrder.id == order.id)
        {
            customer = c;
            break;
        }
    }
    //in case the customer is not found
    if (customer == null)
    {
        Console.WriteLine("Cannot find customer associated with the order.");
        return;
    }
    Console.WriteLine($"Membership Status: {customer.rewards.tier}\nMembership Points: {customer.rewards.points}");
    //check if birthday and deduct the cost of the most expensive ice cream
    int birthdayIndex = -1;
    if (customer.Isbirthday())
    {
        //find the most expensive ice cream
        double highest = 0;
        foreach (IceCream x in order.iceCreamList)
        {
            if (x.CalculatePrice() > highest)
            {
                birthdayIndex += 1;
                highest = x.CalculatePrice();
                break;
            } 
        }
        //deduct the ice cream cost from the bill
        total -= highest;
        Console.WriteLine($"Birthday: -${highest.ToString("0.00")}");

    }

    //check punch card
    //deduct the 11th ice cream (first ice cream in the order)
    //this assumes that the ice creams in the order do not count towards the punch card
    if (customer.rewards.punchCard == 10)
    {
        double punchDeduct = order.iceCreamList[0].CalculatePrice();
        //account for the case where the same ice cream will get subtracted by both the birthday + punch card
        //do not subtract the punchcard, since its already been subtracted by birthday
        //reset punch card anyways
        if (birthdayIndex != 0)
        {
            total -= punchDeduct;
            Console.WriteLine($"11th ice cream: -${punchDeduct.ToString("0.00")}");
        }
        //reset punch card
        customer.rewards.punchCard = 0;
    }

    //check if customer can redeem points
    if (customer.rewards.tier == "Gold" || customer.rewards.tier == "Silver")
    {
        int pointRedeem = inputVal.getIntInput("How many points do you want to redeem? ");
        customer.rewards.RedeemPoints(pointRedeem);
        //calculate how much to deduct from the number of points
        double pointDeduct = pointRedeem * 0.02;
        total -= pointDeduct;
        Console.WriteLine($"{pointRedeem} points redeemed: -${pointDeduct.ToString("0.00")}");
    }

    Console.WriteLine($"Final bill: ${total.ToString("0.00")}\nPress any key to pay");
    Console.ReadKey();

    //increment punchCard
    //use max.min to ensure it doesnt exceed 10
    customer.rewards.punchCard += Math.Min((order.iceCreamList.Count + customer.rewards.punchCard), 10);
    Console.WriteLine($"\nNew punch card: {customer.rewards.punchCard}");
    //calculate how many points to give to the user
    int pointsEarned = Convert.ToInt32(Math.Floor(total * 0.72));
    customer.rewards.AddPoints(pointsEarned);
    Console.WriteLine($"Points Earned: {pointsEarned}");

    //set the order's time fufilled
    order.TimeFulfilled = DateTime.Now;
    //add order to customer's history
    customer.orderHistory.Add(order);
    //remove the currentOrder from the customer
    customer.currentOrder = null;

    
}

void advancedB()
{
    try
    {
        //prompt for year
        Console.Write("Enter the year: ");
        int inpYr = Convert.ToInt32(Console.ReadLine());
        //brings user back to menu if user enter a year that is after the current year
        if (inpYr > DateTime.Now.Year)
        {
            Console.WriteLine("Please enter a valid year.");
            return;
        }
        //create a list to store orders in selected year
        List<Order> ordersInYr = new List<Order>();
        //retrieve orders fulfilled in the inputted year
        foreach (Customer customer in customerDict.Values)
        {
            foreach (Order order in customer.orderHistory)
            {
                //adds the orders into ordersInYr list only when order has been fulfilled
                if (order.TimeFulfilled.HasValue && order.TimeFulfilled.Value.Year == inpYr)
                {
                    ordersInYr.Add(order);
                }
            }
        }
        //storing each cost based on its month
        double[] monthlyAmt = new double[12]; //0 for Jan, 11 for Dec
        //accumulates all of the cost across the whole year
        double totalAmt = 0;

        foreach (Order o in ordersInYr)
        {
            //calculate each order cost and add into the totalAmt
            double orderAmt = o.CalculateTotal();
            totalAmt += orderAmt;
            //subsequently add each order cost into each of their respective months
            int monthindex = o.TimeFulfilled.Value.Month - 1; // -1 as the Month component starts from index 1
            monthlyAmt[monthindex] += orderAmt;
        }
        //display results
        Console.WriteLine("Monthly charged amounts breakdown for {0}", inpYr);
        //used to display each month, if not it will just display numbers 0-11
        string[] monthNames = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        for (int month = 0; month < 12; month++)
        {
            string mnthName = monthNames[month];
            Console.WriteLine($"{mnthName} {inpYr}: {monthlyAmt[month]:F2}");
        }
        //display total amount
        Console.WriteLine($"Total charged amounts for {inpYr}: ${totalAmt:F2}");
    }
    catch (FormatException fx)
    {
        //catch exception if user does not enter an integer
        Console.WriteLine("A valid year must be an integer!");
    }
    catch (IndexOutOfRangeException ex)
    {
        //if somehow, index is out of range of the mnthName array
        Console.WriteLine(ex.Message);
    }

}


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
    Console.WriteLine("[7] Process an order and checkout");
    Console.WriteLine("[8] Monthly charged amounts and total charged amounts for the year");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("-------------------------------");
    Console.Write("Enter an option: ");
    string inp = Console.ReadLine();

    if (inp == "0")
    {
        //store the lines to be added to the file
        List<string> customerAdd = new List<string> { };
        //loop through only the new customers from customerDict
        for (int i = customerCSVLength-1; i < customerDict.Count; i++)
        {
            Customer c = customerDict.ElementAt(i).Value;
            //add the line to the new customer
            customerAdd.Add($"{c.name},{c.memberid},{c.dob.ToString("dd/MM/yyyy")},{c.rewards.tier},{c.rewards.points},{c.rewards.punchCard}");
        }
        //add new customers to Customer.csv
        File.AppendAllLines($"{curr_dir}customers.csv", customerAdd);

        appendToOrdercsv();
        break;
    }
    else if (inp == "1")
    {
        printCustomers();
    }
    else if (inp == "2")
    {
        Console.WriteLine("Current Orders (Gold Queue) : ");
        //Prints out every order from the gold queue
        foreach (Order o in goldQueue)
        {
            Console.WriteLine(o.ToString());
        }
        Console.WriteLine();
        //Prints out every order from the regular queue
        Console.WriteLine("Current Orders (Regular Queue) : ");
        foreach (Order o in regularQueue)
        {
            Console.WriteLine(o.ToString());
        }
    }
    else if (inp == "3")
    {
        //create new customer
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
        //Confirmation message
        Console.WriteLine("Regristration Successful!");


    }
    else if (inp == "4")
    {
        printCustomers();
        //since customerDict stores customers from .csv and gets updated, there is no need to read the csv file again
        string cselect = inputVal.getValuesInput("Customer ID Number: ", customerDict.Keys.ToArray());
        //get the associated customer
        Customer customer = customerDict[cselect];
        //get the user's order -> prompt user for the options and ice creams
        //makeOrder returns the order object
        Order neworder = customer.MakeOrder();
        //set the order's properties
        neworder.TimeReceived = DateTime.Now;
        neworder.id = currOrderID;
        //link the new order to the customer's current order
        customer.currentOrder = neworder;
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
        //confirmation message
        Console.WriteLine("Order has been successfully made!");
        //update the order ID
        currOrderID++;
    }
    else if (inp == "5")
    {
        printCustomers();
        //selects customer from list of customers in csv file
        string cname = inputVal.getValuesInput("Select a customer via ID : ", customerDict.Keys.ToArray());
        //retrieve selected customer
        Customer c = customerDict[cname];
        Console.WriteLine("Current orders: ");
        Console.WriteLine("------------------");
        //display the current orders
        if (c.currentOrder != null)
        {
            Console.WriteLine(c.currentOrder.ToString());
        }
        else //when retrieved customer does not have any current orders placed
        {
            Console.WriteLine("No current orders placed.");
        }
        Console.WriteLine();
        //display order history
        Console.WriteLine("Order History: ");
        Console.WriteLine("------------------");
        //reads through each order stored in retrieved customer's order history to display the order details
        foreach (Order order in c.orderHistory)
        {
            Console.WriteLine("Order ID: {0}",order.id);
            Console.WriteLine("Time Received: {0}",order.TimeReceived);
            Console.WriteLine("Time Fulfilled: {0}",order.TimeFulfilled);

            foreach (IceCream iceCream in order.iceCreamList)
            {
                
                Console.WriteLine("Ice Cream Details:");
                Console.WriteLine(iceCream);
                Console.WriteLine("Price: ${0}", iceCream.CalculatePrice().ToString("0.00"));
                Console.WriteLine();
            }
            Console.WriteLine("------");
        }
        if (c.orderHistory == null)
        {
            Console.WriteLine("Customer has yet to create an order!");
        }
        
    }
    else if (inp == "6")
    {
        printCustomers();
        //selects customer from list of customers in csv file
        string cname = inputVal.getValuesInput("Select a customer via ID : ", customerDict.Keys.ToArray());
        //retrieve selected customer
        Customer c = customerDict[cname];
        Console.WriteLine("{0}'s Current orders: ", customerDict[cname].name);
        //display the current orders
        try
        {
            if (c.currentOrder != null)
            {
                Console.WriteLine(c.currentOrder.ToString());
            }
            else
            {
                /*if there is no current orders from the customer, it will not proceed to give customer options to modify
                and lead them back to start menu again.*/
                Console.WriteLine("There are currently no orders from this customer. Unable to modify orders.\nPlease add an order before doing so.");
                continue;
            }
        }
        catch (NullReferenceException n)
        {
            //only happens when a customer with 1 order r
            Console.WriteLine("There are currently no orders from this customer. Unable to modify orders.\nPlease add an order before doing so.");
            continue;
        }
        Console.WriteLine("-------------------------------");
        Console.WriteLine("[1] Modify existing ice cream");
        Console.WriteLine("[2] Add new ice cream to order");
        Console.WriteLine("[3] Delete existing ice cream in order");
        Console.WriteLine("-------------------------------");
        Console.Write("What would you like to do? ");
        string opt = Console.ReadLine();

        if (opt == "1")
        {
            //store each current order icecream into a list to be printed out
            List<IceCream> iceCreams = c.currentOrder.iceCreamList;
            //print out the id of each icecream in the order and each icecream details
            for (int i = 0; i < iceCreams.Count; i++)
            {
                Console.WriteLine($"ID: {i}\n{iceCreams[i]}\n\n");
            }
            try
            {
                //ask for user input of which specific ice cream order to modify
                Console.Write("Which ice cream would you like to modify? ");
                int id = Convert.ToInt32(Console.ReadLine());
                //calls modifyicecream method
                c.currentOrder.ModifyIceCream(id);
                //confirmation message
                Console.WriteLine("Ice cream order successfully modified!");
            }
            catch (FormatException fx)
            {
                Console.WriteLine("Please enter the correct Ice cream ID");
            }
        }
        else if (opt == "2")
        {
            //store each current order icecream into a list to be printed out
            List<IceCream> iceCreams = c.currentOrder.iceCreamList;
            //call the iceCream method to create a new ice cream
            IceCream newIcecream =  orderMaker.iceCream();
            //add the new ice cream to the current order
            c.currentOrder.AddIceCream(newIcecream);
            //Confirmation message
            Console.WriteLine("New ice cream added to the order!");
            //print icecream to show the newly added ice cream
            for (int i = 0; i < iceCreams.Count; i++)
            {
                Console.WriteLine($"ID: {i}\n{iceCreams[i]}\n\n");
            }
        }
        else if (opt == "3")
        {
            //store each current order icecream into a list to be printed out
            List<IceCream> iceCreams = c.currentOrder.iceCreamList;
            //if customer only has ordered 1 ice cream, he will not be able to access this option to delete ice cream
            //as he cannot have 0 ice creams in an order
            if (iceCreams.Count <= 1)
            {
                Console.WriteLine("You cannot have 0 ice creams in an order.\nOrder more ice creams!");
                continue;
            }
            else
            {
                //print out the id of each icecream in the order and each icecream details
                for (int i = 0; i < iceCreams.Count; i++)
                {
                    Console.WriteLine($"ID: {i}\n{iceCreams[i]}\n\n");
                }
                try
                {
                    //ask for user input on which icecream id would they like to remove
                    Console.Write("Which ice cream id would you like to cancel? ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    //remove the icecream by calling the method
                    c.currentOrder.DeleteIceCream(id);
                }
                catch (FormatException fx)
                {
                    Console.WriteLine("Please enter the correct Ice cream ID.");
                }
            }
        }

    }
    else if (inp == "7")
    {
        advancedA();
    }
    else if (inp == "8")
    {
        advancedB();
    }

    Console.WriteLine();
    
}


