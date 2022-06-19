using ProgrammersBlog.Shared.Utilities.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Result.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; } //ResultStatus.Success // ResultStatus.Error
        public string Message { get; }
        public Exception Exception { get; }

    }
}
