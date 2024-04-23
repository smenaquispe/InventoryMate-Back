namespace InventoryMate.Controllers;
using Microsoft.AspNetCore.Mvc;
using InventoryMate.Models;
using InventoryMate.Services;
using InventoryMate.Utilities;
using InventoryMate.Dto;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        var user = await _userService.GetUserById(id!);
        if (user == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetUserById), user);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetUsers();
        return CreatedAtAction(nameof(GetUsers), users.ToList());
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser?.Id }, createdUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(User user)
    {
        var loggedInUser = await _userService.GetUserById(user.Id!);
        if (loggedInUser == null)
        {
            return NotFound();
        }

        loggedInUser = await _userService.GetUserByEmailAndPassword(user.Id!, user.Email!, user.Password!);
        if (loggedInUser == null)
        {
            return Unauthorized();
        }

        var token = JwtHandler.CreateToken(loggedInUser.Id!, loggedInUser.Role!);
        Console.WriteLine(token);


        var loginResponse = new LoginResponseDto
        {
            user = loggedInUser,
            token = token
        };

        return CreatedAtAction(nameof(GetUserById), new { id = loggedInUser.Id }, loginResponse);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, User")]
    public async Task<ActionResult<User>> UpdateUser(string id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        var updatedUser = await _userService.UpdateUser(user);
        if (updatedUser == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(GetUserById), new { id = updatedUser.Id }, updatedUser);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<User>> DeleteUser(string id)
    {
        var deletedUser = await _userService.DeleteUser(id);
        if (deletedUser == null)
        {
            return NotFound();
        }
        return CreatedAtAction(nameof(DeleteUser), deletedUser);
    }
}