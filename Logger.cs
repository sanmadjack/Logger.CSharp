using System;
using System.IO;
using System.Text;
namespace Logger {
    public class Logger {

        private static StreamWriter log_writer = null;

        static Logger() {
        }

        public static string file_name;

        public static int MaxFiles = 100;
        public static string AppName = null;

        public static void log(String line) {
            DirectoryInfo log_folder;
            if (AppName != null)
                log_folder = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName, "logs"));
            else
                log_folder = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "logs"));

            if (!log_folder.Exists)
                log_folder.Create();

            FileInfo[] existing = log_folder.GetFiles("*.log");
            if (existing.Length > MaxFiles - 1) {
                for (int i = 0; i < MaxFiles; i++) {
                    try {
                        existing[i].Delete();
                    } catch (Exception e) {
                        throw new Exception("Error deleting log file: " + existing[i].FullName, e);
                    }
                }
            }


            if (file_name == null) {
                file_name = Path.Combine(log_folder.FullName, System.DateTime.Now.ToString().Replace('/', '-').Replace(':', '-') + ".log");


            }
            try {
                log_writer = new StreamWriter(file_name, true, Encoding.UTF8);

                StringBuilder output = new StringBuilder(DateTime.Now.ToString());
                output.Append(" - ");
                output.Append(line);



                log_writer.WriteLine(output.ToString());

                log_writer.Close();
                log_writer.Dispose();

            } catch (Exception e) {
                throw new Exception("Error writing to log file: " + file_name, e);
            }
        }

        public static void log(Exception e) {
            StringBuilder output = new StringBuilder(DateTime.Now.ToString());
            output.Append(" - ");
            output.Append(e.Message);
            output.AppendLine(e.StackTrace);
            log(output.ToString());
            if (e.InnerException != null) {
                log(e.InnerException);
            }
        }



    }
}
