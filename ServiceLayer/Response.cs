using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class Response
    {
        public object Result { get; set; }
        public bool IsResultFromCache { get; set; }
        public List<string> ErrorMessages;
        public int StatusCode { get; set; }
        public Response()
        {
            ErrorMessages = new List<string>();
        }
    }
}
