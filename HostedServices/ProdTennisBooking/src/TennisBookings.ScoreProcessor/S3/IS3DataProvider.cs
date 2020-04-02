using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TennisBookings.ScoreProcessor.S3
{
    public interface IS3DataProvider
    {
        Task<Stream> GetStreamAsync(string objectKey, CancellationToken cancellationToken = default);
    }
}
