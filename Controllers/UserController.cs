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
        if(_userContext.Users
            .Where(u => u.Email == user.Email).ToList().Count > 0)
        {
            return BadRequest($"User email \"{user.Email}\" was already registered.");
        }

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

    // search user by email
    [Route("search")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUserByEmail([FromQuery]string? email)
    {
        if(email is null) return BadRequest("An email is needed for the search.");

        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if(user is null) return NotFound();
        return Ok(user);
    }
}