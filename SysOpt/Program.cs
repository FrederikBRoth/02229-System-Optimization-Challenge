// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;

static void LoadTasks(string path)
{
    using var reader = new StreamReader(path);
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ";"
    };
    using var csv = new CsvReader(reader, config);

    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
        string type = csv.GetField("type");
        if (type == "TT")
        {
           //Create TT
        }
        else if (type == "ET")
        {
           //Create ET
        }


    }
}

Console.WriteLine("Hello, World!");

var t = new MyTask();

