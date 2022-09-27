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
            Console.WriteLine("TT");
        }
        else if (type == "ET")
        {
            Console.WriteLine("ET");

            //Create ET
        }


    }
}
LoadTasks("test_cases\\inf_10_10\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv");
Console.WriteLine("Hello, World!");


