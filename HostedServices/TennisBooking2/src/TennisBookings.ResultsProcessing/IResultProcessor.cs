using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TennisBookings.ResultsProcessing
{
    public interface IResultProcessor
    {
        Task ProcessAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}
