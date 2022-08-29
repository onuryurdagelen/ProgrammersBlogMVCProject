using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Result.ComplexTypes
{
    public  enum ResultStatus
    {
        Success = 200,
        Created =201,
        InternalServerError=500,
        AuthorizationError=401,
        ForbiddenError=403,
        ClientError=400,
        NotFound=404,
        Error = 1,
        Warning = 2, //ResultStatus.Warning
        Info = 3 //ResultStatus.Info
    }
}
