using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ServiceLayer
{
    public class FileLogger
    {
        private string _timeStamp { get;  }
        private string _event { get;  }
        private int _statusCode { get;  }
        private List<string> _errors { get;  }
        public FileLogger(string dateTime, string eventName, int statusCode, List<string> errorMessages)
        {
            _timeStamp = dateTime;
            _event = eventName;
            _statusCode = statusCode;
            _errors = errorMessages;
        }
        public void Log()
        {
            FileStream fileStream = new FileStream("Logger.txt", FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine("===========================================================");
            streamWriter.WriteLine("TimeStamp:"+_timeStamp + "|"+ "Event Name:"+_event + "|"+"StatusCode:"+ _statusCode);
            if(_errors.Count > 0)
            {
                streamWriter.Write("Errors List:\n");
                for (int index = 0; index < _errors.Count; index++)
                {
                    streamWriter.WriteLine((index+1)+"."+_errors[index]);
                }
            }
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
