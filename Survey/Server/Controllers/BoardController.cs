using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Survey.Server.Model;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Survey.Server.Controllers
{
    [Route(Survey.Shared.Constants.BACKEND_URL.API_BOARD_URL)]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BoardController : ControllerBase
    {
        private readonly SurveyDbContext _context;
        public BoardController(SurveyDbContext surveyDbContext)
        {
            this._context = surveyDbContext;
        }

        // read
        [HttpGet]
        public List<BoardModel> Get()
        {
            return _context.BoardModel
                .Include(r => r.OwnerUser)
                .Where(x => x.OwnerUser == ServerHelper.GetIdentityUserByEmail(_context, HttpContext))
                .ToList();
        }


        // create
        // POST api/<BoardController>
        [HttpPost]
        public void Post([FromBody] BoardModel bm)
        {
            IdentityUser user = ServerHelper.GetIdentityUserByEmail(_context, HttpContext);
            bm.Cards = _context.CardModel.ToList();
            bm.OwnerUser = user;
            _context.BoardModel.Add(bm);
            _context.SaveChanges();

        }
        // update
        // PUT api/<BoardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // delete
        // DELETE api/<BoardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
