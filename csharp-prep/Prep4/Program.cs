using System;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished");
        int number = 1;
        int count = 0;
        float list_sum = 0;
        float largest_number = float.NegativeInfinity;
        List<float> num_list = new List<float>();


        while (number != 0)
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());
            if (number != 0)
            {
                count += 1;
                num_list.Add(number);
            }
        }
        for (int i = 0; i < count; ++i)
        {
            list_sum += num_list[i];
            if (num_list[i] > largest_number)
            {
                largest_number = num_list[i];
            }
        }
        float list_average = list_sum / count;

        Console.WriteLine($"The sum is: {list_sum}");
        Console.WriteLine($"The average is: {list_average}");
        Console.WriteLine($"The largest number is: {largest_number}");



    }
}