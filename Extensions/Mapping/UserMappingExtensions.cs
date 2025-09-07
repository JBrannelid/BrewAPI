using BrewAPI.DTOs.User;
using BrewAPI.Models;

namespace BrewAPI.Extensions.Mapping
{
    public static class UserMappingExtensions
    {
        public static UserDTO MapToUserDto(this User entity)
        {
            return new UserDTO
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Role = entity.Role
            };
        }

        public static User MapToUser(this UserRegisterDTO dto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            return new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = UserRole.User,
                PasswordHash = passwordHash
            };
        }

        public static void MapToUser(this UserDTO dto, User entity)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Email = dto.Email;
            entity.Role = dto.Role;
        }
    }
}