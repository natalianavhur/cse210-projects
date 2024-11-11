using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;
using CsvHelper.Configuration.Attributes;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel;
using System.Net;
class Breathing : Activities
{
    private int _inhaleDuration;
    private int _exhaleDuration;

    private Stopwatch stopwatch = new();

    public Breathing(string name, string description)
       : base(name, description)
    {
        _inhaleDuration = 5;
        _exhaleDuration = 5;
    }
    public void StartBreathing()
    {
        stopwatch.Start();
        while (stopwatch.Elapsed.TotalSeconds < _duration)
        {
            Console.Write("\nBreath in ...");
            for (int i = _inhaleDuration; i > 0; i--)
            {
                if (stopwatch.Elapsed.TotalSeconds >= _duration) break;
                Console.Write($"\rBreath in ... {i}   ");
                Thread.Sleep(1000);
            }
            Console.Write("\rBreath in ...     \n");

            if (stopwatch.Elapsed.TotalSeconds >= _duration) break;

            Console.Write("Now breath out ...");
            for (int i = _exhaleDuration; i > 0; i--)
            {
                if (stopwatch.Elapsed.TotalSeconds >= _duration) break;
                Console.Write($"\rNow breath out ... {i}   ");
                Thread.Sleep(1000);
            }
            Console.Write("\rNow breath out ...     ");
        }
        stopwatch.Stop();
        Thread.Sleep(5000);
    }
}