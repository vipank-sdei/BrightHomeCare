using HC.Common.HC.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static BrightCare.Common.Enums.CommonEnum;

namespace BrightCare.Common.Service
{
    public class BaseService
    {
        public T ExecuteFunctions<T>(Func<T> method)
        {
            T obj = default(T);
            try
            {
                return method();
            }
            catch (Exception ex)
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        switch (prop.Name)
                        {
                            case "data":
                                {
                                    prop.SetValue(obj, new object(), null);
                                    break;
                                }
                            case "Message":
                                {
                                    prop.SetValue(obj, StatusMessage.ServerError, null);
                                    break;
                                }
                            case "StatusCode":
                                {
                                    prop.SetValue(obj, HttpStatusCodes.InternalServerError, null);
                                    break;
                                }
                            case "AppError":
                                {
                                    prop.SetValue(obj, ex.Message, null);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return obj;
            }
        }
    }
}
