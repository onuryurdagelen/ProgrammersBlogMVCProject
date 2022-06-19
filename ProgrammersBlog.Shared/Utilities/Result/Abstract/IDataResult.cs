﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Result.Abstract
{
    public interface IDataResult<out T>: IResult
    {
        public T Data { get; } // new DataResult<Category>(ResultStatus.Success,category)
                               //new DataResult<IList<Category>>(ResultStatus.Success,categoryList);
    }
}
