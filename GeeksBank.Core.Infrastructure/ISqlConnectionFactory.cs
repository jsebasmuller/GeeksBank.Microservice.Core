using System.Data;
using DataAbstractions.Dapper;

namespace GeeksBank.Core.Infrastructure
{
    public interface ISqlConnectionFactory
    {
        IDataAccessor GetOpenConnection();
    }
}
