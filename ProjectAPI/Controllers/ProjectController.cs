using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        /*
        public static List<Project> marvel = new List<Project>
            {
                new Project {
                    Id = 2,
                    Name = "Captain America",
                    FirstName = "Steve",
                    LastName = "Rogers",
                    Place = "Brooklyn"
                }
            };
        */
        private readonly DataContext context;

        public ProjectController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] //all marvel heroes
        public async Task<ActionResult<List<Project>>> Get()
        {
            return Ok(await context.Projects.ToListAsync());
        }

        [HttpGet("{id}")] //single heroes
        public async Task<ActionResult<Project>> Get(int id)
        {
            //without using db 
            //var marvelhero = marvel.Find(h => h.Id == id); 
            var marvelhero = await context.Projects.FindAsync(id);
            if (marvelhero == null)
                return BadRequest("Most of the heroes died in Endgame. \nGo before that phase and try again");
            return Ok(marvelhero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Project>>> AddMarvelHero(Project heroes)
        {
            context.Projects.Add(heroes);
            await context.SaveChangesAsync();

            return Ok(await context.Projects.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Project>>> UpdateHero(Project request)
        {
            //without db
            //var marvelhero = marvel.Find(h => h.Id == request.Id);
            
            var marvelhero = await context.Projects.FindAsync(request.Id);
            if (marvelhero == null)
                return BadRequest("Most of the heroes died in Endgame. \nGo before that phase and try again");

            marvelhero.Name = request.Name;
            marvelhero.FirstName = request.FirstName;
            marvelhero.LastName = request.LastName;
            marvelhero.Place = request.Place;

            await context.SaveChangesAsync();
            return Ok(await context.Projects.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Project>>> Delete(int id)
        {
            // Without db
            //var marvelhero = marvel.Find(h => h.Id == id);

            var marvelhero = await context.Projects.FindAsync(id);
            if (marvelhero == null)
                return BadRequest("Most of the heroes died in Endgame. \nGo before that phase and try again");
            //marvel.Remove(marvelhero);

            context.Projects.Remove(marvelhero);
            await context.SaveChangesAsync();
            //return Ok(marvel);
            return Ok(await context.Projects.ToListAsync());
        }
    }
}
