using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

public class Stock
{
    protected string _symbol;
    protected string _name;
    protected double CurrentPrice;
    protected int Volume { get; set; }

    public Stock(string symbol, string name)
    {
        _symbol = symbol;
        _name = name;
    }
}
