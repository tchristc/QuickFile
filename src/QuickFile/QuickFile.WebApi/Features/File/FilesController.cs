using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickFile.WebApi.Models;

namespace QuickFile.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        // GET api/files
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guid>>> Get()
        {
            using (var ctx = new Models.QuickFileContext())
            {
                return await ctx.FileStore
                    .Select(s => s.UniqueId)
                    .ToListAsync();
            }
        }

        // GET api/files/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/files
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var files = Request.Form.Files;
            var strigValue = Request.Form.Keys;
            long size = files.Sum(f => f.Length);

            var filePath = Path.GetTempFileName();

            var fileStores = new List<FileStore>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        //string s = Convert.ToBase64String(fileBytes);
                        fileStores.Add(new FileStore
                        {
                            FileData = stream.ToArray()
                        });
                    }
                }
            }

            using(var ctx = new QuickFileContext())
            {
                await ctx.FileStore.AddRangeAsync(fileStores);
                await ctx.SaveChangesAsync();
            }

            return Ok(fileStores.Select(f => new { f.Id, f.UniqueId, f.FileData }));
        }

        // PUT api/files/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/files/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
