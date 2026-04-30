using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> Create(UserDto.Request newUser)
    {
        var result = await _userService.Create(newUser);
        if (!result) return BadRequest("Failed to create user.");
        return Ok("User created successfully.");
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(new { users = users, count = users.Count });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userService.GetById(id);
        if (user == null) return BadRequest("User not found.");
        return Ok(user);
    }
}
