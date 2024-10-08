using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
  void Update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<MemberDto?> GetUserByIdAsync(int id);
  Task<IEnumerable<MemberDto>> GetMembersAsync();
  Task<MemberDto?> GetMemberAsync(string username);
  Task<IEnumerable<MemberDto?>> GetCompanyMembersAsync(string company);
  Task<AppUser?> GetMemberByUsernameAsync(string username);
}
