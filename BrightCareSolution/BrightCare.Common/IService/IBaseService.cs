using System;
using System.Collections.Generic;
using System.Text;

namespace BrightCare.Common.IService
{
    public interface IBaseService
    {
        T ExecuteFunctions<T>(Func<T> method);
    }
}
