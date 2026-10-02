using System.Threading;

// Task 1

Console.WriteLine("Task 1");

List<object> values = new()
{
    10,
    "Hello",
    3.14,
    true,
    false
};

Thread thread = new Thread(() =>
{
    foreach (object value in values)
    {
        Console.WriteLine(value.ToString());
    }
});

thread.Start();
thread.Join();


// Waiting for 5 seconds before proceeding to the next task...

Console.WriteLine();
Console.WriteLine("Waiting for 5 seconds...");
Thread.Sleep(5000);


// Task 2

Console.WriteLine();
Console.WriteLine("Task 2");

Bank bank = new Bank();

bank.Name = "PrivatBank";
bank.Money = 10000;
bank.Percent = 15;

Thread.Sleep(1000);

Console.WriteLine("Data written to Bank.txt");


class Bank
{
    private int money;
    private string name = "";
    private int percent;

    private readonly object lockObject = new();

    public int Money
    {
        get => money;
        set
        {
            money = value;
            SaveToFile();
        }
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            SaveToFile();
        }
    }

    public int Percent
    {
        get => percent;
        set
        {
            percent = value;
            SaveToFile();
        }
    }

    private void SaveToFile()
    {
        Thread thread = new Thread(() =>
        {
            using (StreamWriter file = new StreamWriter("Bank.txt", true))
            {
                file.WriteLine($"Name: {name} | Money: {money} | Percent: {percent}");
            }
        });

        thread.Start();
        thread.Join();
    }
}