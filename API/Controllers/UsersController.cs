using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberDto>> GetUser(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if(user == null) return NotFound();

        return Ok(user);
    }

    [HttpGet("{company}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserByCompany(string company)
    {
        var users = await userRepository.GetCompanyMembersAsync(company.ToLower());

        if(users == null) return NotFound();

        return Ok(users);
    }
    
}
