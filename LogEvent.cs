using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEventForCSharp
{
    class LogEvent
    {
        private static string defaultFilePath = Path .GetFullPath(Directory.GetCurrentDirectory() + @"\Logs\");
        private static string _fileName = string.Empty;
        public static string fileName 
        {
            get 
            { 
                if (_fileName.Equals(string.Empty))
                    _fileName = string.Format("{0:yyyyMMddmmss}.txt", DateTime.Now);
                return _fileName; 
            }
        } 

        public static void writeLog(string logMessage)
        {
            writeLog(defaultFilePath, logMessage);
        }
        public static void writeLog(string filePath,string logMessage)
        {
            if (string.IsNullOrEmpty(filePath))
                filePath = defaultFilePath;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fullPath = filePath + LogEvent.fileName;
            writeLogToFile(fullPath, logMessage);
        }
        private static void writeLogToFile(string fullPath, string logMessage)
        {
            FileInfo fileInfo = new FileInfo(fullPath);
            if (fileInfo.Directory.Exists == false)
                fileInfo.Directory.Create();
            File.AppendAllText(fullPath, Environment.NewLine, Encoding.Unicode);
            File.AppendAllText(fullPath, logMessage, Encoding.Unicode);
        }
    }
}
