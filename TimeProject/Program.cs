using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TimeProject {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello there!");

            Thread.Sleep(5000);

            Console.WriteLine("Now begin calculating...");
            var min = DateTime.MinValue;
            var max = DateTime.MaxValue;

            var daysOfWeek = new List<Dto>();
            var dayOfWeekAndDayOfMonth = new List<Dto>();
            var dayOfWeekMonthAndDayOfMonth = new List<Dto>();
            var sw = new Stopwatch();
            sw.Start();
            for (DateTime i = min; i < max; i = i.AddDays(1)) {
                Console.WriteLine($"Now processing {i: dd/MM/yyyy}");
                
                var year = i.Year;

                var dayOfWeek = i.DayOfWeek.ToString();
                Upsert(dayOfWeek, daysOfWeek, year);

                var dayOfMonth = $"{dayOfWeek}, {i.Day}";
                Upsert(dayOfMonth, dayOfWeekAndDayOfMonth, year);

                var month = dayOfMonth + $"/{i.Month}";
                Upsert(month, dayOfWeekMonthAndDayOfMonth, year);

                if(i.Day == max.Day && i.Month == max.Month && i.Year == max.Year) {
                    break;
                }
            }

            sw.Stop();
            Console.WriteLine($"Finished Processing all dates in {sw.ElapsedTime()}");
            
            dayOfWeekAndDayOfMonth.ForEach(x => x.Calculate());
            dayOfWeekMonthAndDayOfMonth.ForEach(x => x.Calculate());

            var conc = new ConcentratedDto {
                Range = $"{min: dd/MM/yyyy} - {max: dd/MM/yyyy}",
                TotalDays = (max - min).TotalDays,
                DaysOfWeek = daysOfWeek.Select(x => x.ToString()),
                DaysOfWeekAndDaysOfMonth = dayOfWeekAndDayOfMonth.Select(x => x.ToString()),
                DaysOfWeekDaysOfMonthAndMonth = dayOfWeekMonthAndDayOfMonth.Select(x => x.ToString())
            };

            File.WriteAllText("C:\\temp\\timeproject.json", JsonConvert.SerializeObject(conc));
        }

        static void Upsert(string tag, List<Dto> collection, int year) {
            var found = collection.FirstOrDefault(x => x.Tag == tag);
            if(found == null) {
                collection.Add(new Dto(tag, 1, year));
            } else {
                found.Count += 1;
                found.Years.Add(year);
                found.Years = found.Years.Distinct().ToList();
            }
        }
    }
}
