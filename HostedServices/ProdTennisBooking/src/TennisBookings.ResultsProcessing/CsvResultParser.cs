using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace TennisBookings.ResultsProcessing
{
    public class CsvResultParser : ICsvResultParser
    {
        public IReadOnlyCollection<TennisMatchRow> ParseResult(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader);

            csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
            
            var records = csv.GetRecords<TennisMatchRow>();

            return records.ToArray();
        }
    }
}
