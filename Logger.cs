using System;
using System.IO;
using System.Text;
namespace Logger {
    public class Logger {

        private static StreamWriter log_writer;


        static Logger() {
            log_writer = new StreamWriter("log.txt", true, Encoding.UTF8);
        }

        public static void log(String line) {
            StringBuilder output = new StringBuilder(DateTime.Now.ToString());
            output.Append(" - ");
            output.Append(line);

            log_writer.WriteLine(output.ToString());
        }

        public static void log(Exception e) {
            StringBuilder output = new StringBuilder(DateTime.Now.ToString());
            output.Append(" - ");
            output.Append(e.Message);
            output.AppendLine(e.StackTrace);
            log_writer.WriteLine(output.ToString());
            if (e.InnerException != null) {
                log(e.InnerException);
            }
        }



    }
}
