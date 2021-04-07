using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using Utilities.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public RepositoryContext DbContext;

        public UserController(RepositoryContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }
        // POST api/<UserController>/<Action>
        [HttpPost]
        public ActionResult<User> Register([FromForm]User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //store date created
                    user.CreateDateTime = DateTime.Now;
                    //populate default for new users
                    Transaction trans = new Transaction
                    {
                        UserId = user.Id,
                        Date = user.CreateDateTime,
                        Account = user.Id,
                        Amount = 0,
                        Type = 0,
                        PrevBalance = 0,
                        CurrentBalance = 0,
                    };

                    DbContext.users.Add(user);
                    //add default transactino with zero balance
                    DbContext.transactions.Add(trans);
                    DbContext.SaveChanges();
                    return Ok("Inserted Successfully");
                }
                else
                {
                    return BadRequest("Failed to insert!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }



    }
}
