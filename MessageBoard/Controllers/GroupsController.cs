using System.Collections;
using MessageBoard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroupsController : ControllerBase
  {
    private MessageBoardContext _db;

    public GroupsController(MessageBoardContext db)
    {
      _db = db;
    }

    //GET api/groups
    [HttpGet]
    public ActionResult<IEnumerable<Group>> Get()
    {
      return _db.Groups.ToList();
    }

    //POST api/groups
    [HttpPost]
    public void Post([FromBody] Group group)
    {
      _db.Groups.Add(group);
      _db.SaveChanges();
    }
  }
}