using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<MemberDto?> GetCompanyMembersAsync(string company)
    {
      return await context.Users
          .Where(x => x.UserName == company)
          .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
          .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();        
    }

  public async Task<MemberDto?> GetUserByIdAsync(int id)
  {
    return await context.Users
          .Where(x => x.Id == id)
          .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
          .SingleOrDefaultAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }

  public void Update(AppUser user)
  {
    context.Entry(user).State = EntityState.Modified;
  }
}
