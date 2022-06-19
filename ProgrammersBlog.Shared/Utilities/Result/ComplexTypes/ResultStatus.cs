using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Result.ComplexTypes
{
    public  enum ResultStatus
    {
        Success = 0,
        Error = 1,
        Warning = 2, //ResultStatus.Warning
        Info = 3 //ResultStatus.Info
    }
}
