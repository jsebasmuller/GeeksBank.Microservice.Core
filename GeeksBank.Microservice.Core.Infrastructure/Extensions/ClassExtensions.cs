using System;
using System.Linq;
using GeeksBank.Microservice.Core.Domain.Exception;

namespace GeeksBank.Microservice.Core.Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        ///  Check any instance is Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return (obj is null);
        }
        
        /// <summary>
        /// Check any instance is not null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return !(obj is null);
        }
        
        
        /// <summary>
        /// Check any instance is not null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object ThrowNotFoundIfNull(this object obj, string message)
        {
           if (obj is null)
           {
               throw new NotFoundException(message);
           }

           return obj;
        }
        
        /// <summary>
        /// Check any instance is not null
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static object ThrowNotFoundIfZero(this int number, string message)
        {
            if (number == 0)
            {
                throw new NotFoundException(message);
            }
            return number;
        }
        
        public static T ThrowBadRequestIfNull<T>(this T obj, string message)
        {

            if (obj.IsNull())
                throw new BadRequestException(message);

            return obj;
        }
        
        public static void PatchObject<T>(this T target, T source)
        {
            var t = typeof(T);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}
