using System;
using System.IO;
using System.Data.SQLite;

class DatabaseInitializer
{
    public void InitializeDatabase(string databasePath)
    {
        if (!File.Exists(databasePath))
        {
            SQLiteConnection.CreateFile(databasePath);
            Console.WriteLine($"Database file '{databasePath}' created.");
        }
        else
        {
            Console.WriteLine($"Database file '{databasePath}' already exists.");
        }

        using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
        {
            connection.Open();

            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS StockData (
                    Symbol TEXT,
                    Timestamp TEXT,
                    Open REAL,
                    High REAL,
                    Low REAL,
                    Close REAL,
                    Volume INTEGER,
                    PRIMARY KEY (Symbol, Timestamp)
                );";

            SQLiteCommand command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Database and table initialized successfully.");
        }
    }
}