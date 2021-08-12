using MessageBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoard.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MessagesController : ControllerBase
  {
    private MessageBoardContext _db;

    public MessagesController(MessageBoardContext db)
    {
      _db = db;
    }

    //GET api/messages
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<Message>> Get(int groupId, string userName, DateTime afterDate, DateTime beforeDate)
    {
      var query = _db.Messages.AsQueryable();

      if(groupId > 0)
      {
        query = query.Where(entry => entry.GroupId == groupId);
      }

      if(userName != null)
      {
        query = query.Where(entry => entry.UserName == userName);
      }

      if(afterDate != DateTime.MinValue)
      {
        query = query.Where(entry => DateTime.Compare(entry.Date, afterDate) >= 0);
      }

      if(beforeDate != DateTime.MinValue)
      {
        query = query.Where(entry => DateTime.Compare(beforeDate, entry.Date) >= 0);
      }

      return query.ToList();
    }

    //POST api/messages
    [Authorize]
    [HttpPost]
    public void Post([FromBody] Message message)
    {
      var currentUser = HttpContext.User;

      message.Date = DateTime.Now;
      if(currentUser.HasClaim(c => c.Type == "Username"))
      {
        message.UserName = currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value;
        _db.Messages.Add(message);
        _db.SaveChanges();
      }
    }

    //GET api/messages/5
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<Message> Get(int id)
    {
      return _db.Messages.FirstOrDefault(entry => entry.MessageId == id);
    }

    //PUT api/messages/5
    [Authorize]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Message message)
    {
      var currentUser = HttpContext.User;
      if (currentUser.HasClaim(c => c.Type == "Username"))
      {
        message.UserName = currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value;
      }

      Message oldMessage = _db.Messages.FirstOrDefault(entry => entry.MessageId == id);
      _db.Entry(oldMessage).State = EntityState.Detached;
      if(message.UserName == oldMessage.UserName)
      {
        message.MessageId = id;
        message.Date = DateTime.Now;
        _db.Entry(message).State = EntityState.Modified;
        _db.SaveChanges();
      }
    }

    //DELETE api/messages/5
    [Authorize]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var currentUser = HttpContext.User;
      string username = null;
      if (currentUser.HasClaim(c => c.Type == "Username"))
      {
        username = currentUser.Claims.FirstOrDefault(c => c.Type == "Username").Value;
      }

      Message messageToDelete = _db.Messages.FirstOrDefault(entry => entry.MessageId == id);
      if(messageToDelete.UserName == username)
      {
        _db.Messages.Remove(messageToDelete);
        _db.SaveChanges();
      }
    }
  }
}