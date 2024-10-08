using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
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

        if (user == null) return NotFound();

        return Ok(user);
    }

    // [HttpGet("{company}")]
    // public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserByCompany(string company)
    // {
    //     var users = await userRepository.GetCompanyMembersAsync(company.ToLower());

    //     if (users == null) return NotFound();

    //     return Ok(users);
    // }

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMemberByUsername(string username)
    {
        var users = await userRepository.GetMemberAsync(username.ToLower());

        if (users == null) return NotFound();

        return Ok(users);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await userRepository.GetMemberByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find the user");

        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await userRepository.GetMemberByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Cannot update user");

        var result = await photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PlubicId = result.PublicId
        };

        user.Photos.Add(photo);

        if (await userRepository.SaveAllAsync()) return mapper.Map<PhotoDto>(photo);

        return BadRequest("Problem adding photo");

    }

}
