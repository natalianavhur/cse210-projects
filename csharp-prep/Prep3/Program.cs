using System;

class Program
{
    static void Main(string[] args)
    {

        // Console.Write("What is the magic number? ");
        // int magic_number = int.Parse(Console.ReadLine());
        
        Random random = new Random();
        int magic_number = random.Next(1,100);

        bool pass = true;
        do
        {
            Console.Write("What is your guess? ");
            int guess = int.Parse(Console.ReadLine());

            if (guess > magic_number)
            {
                Console.WriteLine("Lower");

            }
            else if (guess < magic_number)
            {
                Console.WriteLine("Higher");
            }
            else
            {
                Console.WriteLine("You guessed it!");
                pass = false;
            }
        } while (pass);
    }
}