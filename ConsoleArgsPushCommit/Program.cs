class MainClass
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid args");
            return;
        }

        var command = args[0];

        switch (command)
        {
            case "push":
                Push();
                break;
            case "commit" when args.Length == 3 && args[1] == "-m":
                Commit(args[2]);
                break;
            default:
                Console.WriteLine("Invalid command");
                break;
        }

    }
    static void Push()
    {
        Console.WriteLine("Executing Push");
    }
    static void Commit(string message)
    {
        Console.WriteLine($"Executing Commit with message: {message}");
    }

}