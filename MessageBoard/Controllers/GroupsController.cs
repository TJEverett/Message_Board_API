using System.Collections;
using MessageBoard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Group>> Get()
    {
      return _db.Groups.ToList();
    }

    //POST api/groups
    [Authorize]
    [HttpPost]
    public void Post([FromBody] Group group)
    {
      _db.Groups.Add(group);
      _db.SaveChanges();
    }

    //GET api/groups/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<Group> Get(int id)
    {
      return _db.Groups.FirstOrDefault(entry => entry.GroupId == id);
    }
  }
}