using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NotificationSystem.Data;
using NotificationSystem.Models;

namespace NotificationSystem.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(UserContext userContext) : ControllerBase
{
    private readonly UserContext _userContext = userContext;

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _userContext.Users.Add(user);
        await _userContext.SaveChangesAsync();

        return Ok(user);
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        User? user = await _userContext.Users.FindAsync(id);

        if(user is null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUserByName([FromQuery]string name)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync<User>(u => u.Name == name);

        if(user is null) return NotFound();
        return Ok(user);
    }
}