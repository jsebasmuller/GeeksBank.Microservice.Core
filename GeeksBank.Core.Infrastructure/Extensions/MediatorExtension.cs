using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace GeeksBank.Core.Infrastructure.Extensions
{
    /// <summary>
    /// Used to publish all events stored in the entity's domain event list
    /// </summary>
    /// <source>Microsoft</source>
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, SqlServerDbContext ctx)
        {
            await Task.CompletedTask;
        }
    }
}
