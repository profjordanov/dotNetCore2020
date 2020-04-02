using System.ComponentModel.DataAnnotations;

namespace TennisBookings.Web.Data
{
    public class ConfigurationEntry
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
