using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Players.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private static readonly TennisPlayer[] Players = new[]
        {
            new TennisPlayer{ Id = 1, Forename = "Katie", Surname = "Moreno" },
            new TennisPlayer{ Id = 2, Forename = "Ricky", Surname = "Howell" },
            new TennisPlayer{ Id = 3, Forename = "Kristina", Surname = "Burton" },
            new TennisPlayer{ Id = 4, Forename = "Thomas", Surname = "Cruz" },
            new TennisPlayer{ Id = 5, Forename = "Julie", Surname = "Allison" },
            new TennisPlayer{ Id = 6, Forename = "Eduardo", Surname = "Howell" },
            new TennisPlayer{ Id = 7, Forename = "Natalie", Surname = "Hall" },
            new TennisPlayer{ Id = 8, Forename = "Armando", Surname = "Lindsey" },
            new TennisPlayer{ Id = 9, Forename = "Marcos", Surname = "Cook" },
            new TennisPlayer{ Id = 10, Forename = "Francis", Surname = "Kelley" },
            new TennisPlayer{ Id = 11, Forename = "Jenna", Surname = "Cobb" },
            new TennisPlayer{ Id = 12, Forename = "Billie", Surname = "Ellis" },
            new TennisPlayer{ Id = 13, Forename = "Judy", Surname = "Owens" },
            new TennisPlayer{ Id = 14, Forename = "Angela", Surname = "Guzman" },
            new TennisPlayer{ Id = 15, Forename = "Pedro", Surname = "Byrd" },
            new TennisPlayer{ Id = 16, Forename = "Jamie", Surname = "Matheson" },
        };

        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        [HttpGet]
        public async Task<IEnumerable<TennisPlayer>> GetAll()
        {
            await Task.Delay(Random.Next(1000, 2000)); // Simulates a slow API and network latency

            return Players;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<TennisPlayer> Get(int id)
        {
            await Task.Delay(Random.Next(200, 300)); // Simulates a slow API and network latency

            return Players.SingleOrDefault(x => x.Id == id);
        }
    }
}
