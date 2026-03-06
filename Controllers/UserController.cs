using Microsoft.AspNetCore.Mvc;
using NotificationSystem.Data;
using NotificationSystem.Models;

namespace NotificationSystem.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserContext _userContext;

    public UserController(UserContext userContext)
    {
        _userContext = userContext;
    }

    [Route("api/users")]
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        _userContext.Users.Add(user);
        _userContext.SaveChanges();

        return Ok(user);
    }
}