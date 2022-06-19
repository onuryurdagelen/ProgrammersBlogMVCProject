using ProgrammersBlog.Shared.Utilities.Result.Abstract;
using ProgrammersBlog.Shared.Utilities.Result.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Result.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus,T data):base(resultStatus)
        {
            Data = data;
        }
        public DataResult(ResultStatus resultStatus,string message,T data) : base(resultStatus,message)
        {
            Data = data;
        }
        public DataResult(T data, ResultStatus resultStatus, string message,Exception exception) : base(resultStatus, message, exception)
        {
            Data = data;
        }



        public T Data { get; }
    }
}
