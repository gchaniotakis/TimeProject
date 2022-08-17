using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TimeProject {
    public class Dto {

        public string Tag { get; set; }
        public long Count { get; set; }
        public List<int> Years { get; set; }

        public long Past { get; set; }
        public long Future { get; set; }

        public bool IsPresent { get; set; }
 
        public Dto(string tag, long count, int year = 0) {
            if (year != 0) {
                Years = new List<int>() { year };
            }
            Past = 0;
            Future = 0;
            Tag = tag;
            Count = count;
            IsPresent = false;
        }

        public void Calculate() {

            var present = DateTime.Today.Year;

            foreach(var year in Years) {
                if(year > present) {
                    Future += 1;
                }
                else if(year < present) {
                    Past += 1;
                } else {
                    IsPresent = true;
                }
            }
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ConcentratedDto {
        public string Range { get; set; }
        public double TotalDays { get; set; }
        public IEnumerable<string> DaysOfWeek { get; set; }
        public IEnumerable<string> DaysOfWeekAndDaysOfMonth { get; set; }
        public IEnumerable<string> DaysOfWeekDaysOfMonthAndMonth { get; set; }        
    }
}
