using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpt.Helpers
{
    public class TaskReader
    {
        static public (List<TimeTriggeredTask>, List<EventTriggeredTask>) LoadTasks(string path)
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
                        csv.GetField("name"),
                        csv.GetField<int>("seperation")
                    );
                    tasks.etList.Add(etTask);

                }
            }
            return tasks;
        }
    }
}
