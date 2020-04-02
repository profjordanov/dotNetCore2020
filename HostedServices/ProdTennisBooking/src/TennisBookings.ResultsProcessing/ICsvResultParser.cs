using System.Collections.Generic;
using System.IO;

namespace TennisBookings.ResultsProcessing
{
    public interface ICsvResultParser
    {
        IReadOnlyCollection<TennisMatchRow> ParseResult(Stream stream);
    }
}
