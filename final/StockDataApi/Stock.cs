public class Stock
{
    private string _symbol;
    private string _companyName;
    private double _price;
    private long _volume;
    private double _change;
    private DateTime _date;
    private double _open;
    private double _high;
    private double _low;
    private double _close;

    public Stock() { }

    public Stock(string symbol, string companyName, double price, long volume, double change, DateTime date)
    {
        _symbol = symbol;
        _companyName = companyName;
        _price = price;
        _volume = volume;
        _change = change;
        _date = date;
    }

    // Add a constructor for database mapping
    public Stock(string symbol, DateTime date, double open, double high, double low, double close, long volume)
    {
        _symbol = symbol;
        _date = date;
        _open = open;
        _high = high;
        _low = low;
        _close = close;
        _volume = volume;
    }

    public string Symbol
    {
        get => _symbol;
        set => _symbol = value;
    }

    public string CompanyName
    {
        get => _companyName;
        set => _companyName = value;
    }

    public double Price
    {
        get => _price;
        set => _price = value;
    }

    public long Volume
    {
        get => _volume;
        set
        {
            if (value >= 0)
                _volume = value;
            else
                throw new ArgumentException("Volume cannot be negative.");
        }
    }

    public double Change
    {
        get => _change;
        set => _change = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public double Open
    {
        get => _open;
        set => _open = value;
    }

    public double High
    {
        get => _high;
        set => _high = value;
    }

    public double Low
    {
        get => _low;
        set => _low = value;
    }

    public double Close
    {
        get => _close;
        set => _close = value;
    }
}
