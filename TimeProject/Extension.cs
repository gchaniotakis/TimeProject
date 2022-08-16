using System;
using System.Collections.Generic;
using System.Text;

namespace TimeProject {
    public static class Extension {
        public static string ElapsedTime(this System.Diagnostics.Stopwatch sw) {
            var result = string.Empty;

            if (sw.ElapsedMilliseconds > 1000) {
                var seconds = Math.Round(Convert.ToDecimal(sw.ElapsedMilliseconds) / 1000, 2);

                if (seconds > 60) {
                    var minutes = Math.Round(seconds / 60, 2);
                    if (minutes > 60) {
                        var hours = Math.Round(minutes / 60, 2);
                        result = $"{hours} {nameof(hours)}";
                    } else {
                        result = $"{minutes} {nameof(minutes)}";
                    }
                } else {
                    result = $"{seconds} {nameof(seconds)}";
                }
            } else {
                result = $"{sw.ElapsedMilliseconds} ms";
            }

            return result;
        }
    }
}
