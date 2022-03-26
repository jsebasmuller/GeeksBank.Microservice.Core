using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GeeksBank.Core.Domain.SeedWork
{
    public interface IFinder<T,Key> where T : IDto where Key: IComparable
    {
        Task<T> FindByIdAsync(Key id);
        Task<IEnumerable<T>> ListAsync();
    }
}