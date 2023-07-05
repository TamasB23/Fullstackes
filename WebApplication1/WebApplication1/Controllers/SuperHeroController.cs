using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.IO;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly string _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");

        [HttpGet]
        public ActionResult<IEnumerable<SuperHero>> GetAllHeroes()
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(_dataFilePath);
                var superheroes = JsonSerializer.Deserialize<IEnumerable<SuperHero>>(jsonData);
                return Ok(superheroes);
            }
            catch (IOException)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SuperHero> GetHeroById(int id)
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(_dataFilePath);
                var superheroes = JsonSerializer.Deserialize<IEnumerable<SuperHero>>(jsonData);
                foreach (var hero in superheroes)
                {
                    if (id == hero.Id)
                    {
                        return Ok(hero);
                    }
                }
                return NotFound(); // Hero with the specified ID was not found
            }
            catch (IOException)
            {
                return StatusCode(500);
            }
        }
    }
}
