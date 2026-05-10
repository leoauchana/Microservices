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

    // Endpoints for api

    [HttpPost]
    public async Task<IActionResult> Create(UserDto.Create newUser)
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
    [HttpGet("{idUser}")]
    public async Task<IActionResult> GetById(string idUser)
    {
        var user = await _userService.GetById(idUser);
        
        if (user == null) return BadRequest("User not found.");
        
        return Ok(new { userFound = user });
    }

    // Endpoints for microservices communication

    [HttpGet("validate/{idUser}")]
    public async Task<IActionResult> Validate(string idUser)
    {
        var userValidated = await _userService.Validate(idUser);
        
        return Ok(userValidated);
    }
}
