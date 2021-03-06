﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEventForCSharp
{
    class LogEvent
    {
        private static string defaultFilePath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\Logs\");
        private static string _fileName = string.Empty;
        public static string fileName 
        {
            get 
            { 
                if (_fileName.Equals(string.Empty))
                    _fileName = string.Format("{0:yyyyMMddmmss}.txt", DateTime.Now);
                return _fileName; 
            }
            set
            {
                _fileName = value;
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
        public static void writeLog(string filePath,string writeFileName,string logMessage )
        {
            fileName = writeFileName;
            writeLog(filePath, logMessage);
        }
        public static void writeLogWithFileName(string writeFileName, string logMessage)
        {
            fileName = writeFileName;
            writeLog(logMessage);
        }
        private static object locker = new object();
        private static void writeLogToFile(string fullPath, string logMessage)
        {
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(fullPath))
                {
                    sw.WriteLine(Environment.NewLine);
                    sw.WriteLine(logMessage);
                }
            }
        }
    }
}
