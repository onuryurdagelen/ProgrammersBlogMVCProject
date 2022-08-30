using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Extensions
{
    public class GlobalErrorHandler
    {

        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class CustomeErrorDetails<T> where T: Exception
    {
        public IEnumerable<T> Errors { get; set; }
    }
}
