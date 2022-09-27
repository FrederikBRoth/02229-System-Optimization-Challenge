// See https://aka.ms/new-console-template for more information
using CsvHelper.Configuration;
using CsvHelper;
using SysOpt;
using System.Globalization;
using System.Xml.Linq;

static (List<TimeTriggeredTask>, List<EventTriggeredTask>) LoadTasks(string path)
{
    //Tuple containing two lists, TT task list and ET task list
    (List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = (new(), new());
    //Configures CSV parser and sets delimiter
    using var reader = new StreamReader(path);
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = ";"
    };
    using var csv = new CsvReader(reader, config);

    csv.Read();
    csv.ReadHeader();
    // Reads CSV file and creates TT and ET task objects bases on type field
    while (csv.Read())
    {
        string type = csv.GetField("type");
        if (type == "TT")
        {
            //Create TT
            TimeTriggeredTask ttTask = new TimeTriggeredTask
            (
                csv.GetField<int>("period"),
                csv.GetField<int>("duration"),
                csv.GetField<int>("priority"),
                csv.GetField<int>("deadline"),
                csv.GetField("name")
            );
            tasks.ttList.Add(ttTask);
        }
        else if (type == "ET")
        {
            //Create ET
            EventTriggeredTask etTask = new EventTriggeredTask
            (
                csv.GetField<int>("period"),
                csv.GetField<int>("duration"),
                csv.GetField<int>("priority"),
                csv.GetField<int>("deadline"),
                csv.GetField("name")
            );
            tasks.etList.Add(etTask);
     
        }
    }
    return tasks;
}
string testCasePath = "test_cases\\inf_10_10\\taskset__1643188013-a_0.1-b_0.1-n_30-m_20-d_unif-p_2000-q_4000-g_1000-t_5__0__tsk.csv";

(List<TimeTriggeredTask> ttList, List<EventTriggeredTask> etList) tasks = LoadTasks(testCasePath);

Console.WriteLine("Number of TT Tasks: " + tasks.ttList.Count);
Console.WriteLine("Number of ET Tasks: " + tasks.etList.Count);



