// See https://aka.ms/new-console-template for more information
//==========================================================
// Student Number : S10257381
// Student Name : Donovan Lye
// Partner Name : Brydon Ti
//==========================================================

using S10257381_PRG2Assignment;
//Get the directory of the program.cs file
//Since Directory.getCurrentDirectory() returns the net6.0 folder, change the string to get the PRG2Assignment folder
string curr_dir = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0","\\");

//read files in the same directory as program.cs, more convenient to place files in this directory than net6.0
//calls File.ReadAllLines, return a string array, each element represents one line of the file
string[] readLines(string f) 
{
    return File.ReadAllLines($"{curr_dir}{f}");
}

List<Customer> customerList = new List<Customer>();
string[] customerFile = readLines("customers.csv");
for (int i = 1; i < customerFile.Length; i++)
{
    string[] x = customerFile[i].Split(",");
    Customer customer = new Customer(x[0], Convert.ToInt32(x[1]), Convert.ToDateTime(x[2]));
    PointCard pointcard = new PointCard(Convert.ToInt32(x[4]), Convert.ToInt32(x[5]));
    pointcard.tier = x[3];
    customer.rewards = pointcard;
    customerList.Add(customer);
}

Queue<Order> orderRQueue = new Queue<Order>();
Queue<Order> orderGQueue = new Queue<Order>();
string[] orderFile = readLines("orders.csv");
for (int i = 1; i < orderFile.Length; i++)
{
    string[] y = orderFile[i].Split(",");
    Order orders = new Order(Convert.ToInt32(y[0]), Convert.ToDateTime(y[2]));
    orderRQueue.Enqueue(orders);
}

while (true)
{
    Console.WriteLine();

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
    if (inp == "1")
    {
        //use the header from customer.csv and print it
        string[] header = customerFile[0].Split(",");
        Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5}", header[0], header[1], header[2], header[3], header[4], header[5]);
        //iterate through customerList and print the properties of customer and its associated point card
        foreach (Customer c in customerList)
        {
            Console.WriteLine("{0,-12}{1,-12}{2,-15}{3,-20}{4,-20}{5}", c.name, c.memberid, c.dob.ToString("MM-dd-yyyy"), c.rewards.tier, c.rewards.points, c.rewards.punchCard);
        }
    }

    Console.WriteLine();

    if (inp == "2")
    {
        int count = 1;
        Console.WriteLine("Current Orders (Regular Queue) : ");
        //use the header from customer.csv and print it
        string[] header = orderFile[0].Split(",");
        Console.WriteLine("{0,-12}{1,-12}", header[0], header[2]);
        foreach (Order o in orderRQueue)
        {
            Console.WriteLine("{0,-11} {1,-12}", o.id, o.TimeReceived.ToString());
            count ++;
        }
    }

    if (inp == "4")
    {
        Console.WriteLine("Customer Names:");
        // Prints out all the names listed in customers.csv
        foreach (Customer customer in customerList)
        {
            Console.WriteLine(customer.name);
        }
        Customer newOrder = new Customer();
        newOrder.MakeOrder();
    }
}
//#3

