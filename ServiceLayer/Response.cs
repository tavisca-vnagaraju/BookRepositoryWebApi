using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class Response
    {
        public object Result { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
