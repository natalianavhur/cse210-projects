using System;

class Program
{
    static void Main(string[] args)
    {
        string first_name, last_name;

        Console.Write("What is your first name? ");
        first_name = Console.ReadLine();

        Console.Write("What is your last name? ");
        last_name = Console.ReadLine();

        Console.WriteLine($"Your name is {last_name}, {first_name} {last_name}.");

        Console.Write("What is your grade percentage?>");
        int grade = int.Parse(Console.ReadLine()); 
        string letter;
        if (grade >= 90){
            letter = "A";
        }
        else if (grade >=80){
            letter = "B";
        }
        else if (grade >=70) {
            letter = "C";
        }
        else if (grade >=60) {
            letter = "D";
        }
        else{
            letter = "F";
        }
        Console.WriteLine($"Your grade is {letter}.");

        if (grade >=70){
            Console.WriteLine($"Congratulations! {first_name} {last_name} passed the couse.");
        }
        else{
            Console.WriteLine("Good Luck next time!");
        }
    }
    
}