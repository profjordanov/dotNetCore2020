using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Configuration.Custom
{
    public class EntityFrameworkConfigurationProvider : ConfigurationProvider
    {
        public EntityFrameworkConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<TennisBookingDbContext>();

            OptionsAction(builder);

            using (var dbContext = new TennisBookingDbContext(builder.Options))
            {
                Data = dbContext.ConfigurationEntries.Any()
                    ? dbContext.ConfigurationEntries.ToDictionary(c => c.Key, c => c.Value)
                    : new Dictionary<string, string>();
            }
        }
    }
}
