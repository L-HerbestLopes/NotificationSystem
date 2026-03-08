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

    // registers an user
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        _userContext.Users.Add(user);
        await _userContext.SaveChangesAsync();

        return Ok(user);
    }

    // get user by id
    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        User? user = await _userContext.Users.FindAsync(id);

        if(user is null) return NotFound();
        return Ok(user);
    }

    // get all users
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return await _userContext.Users.ToListAsync();
    }

    // search user by name or email
    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUserByName([FromQuery]string? name, [FromQuery]string? email)
    {
        if(name is null && email is null) return BadRequest("A name or email is needed for search.");

        var user = await _userContext.Users.FirstOrDefaultAsync<User>(u => u.Name == name || u.Email == email);

        if(user is null) return NotFound();
        return Ok(user);
    }
}