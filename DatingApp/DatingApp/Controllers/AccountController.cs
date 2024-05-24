using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDtos registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("This UserName is already taken");
            }
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }


        /// <summary>
        /// This method allows user to login with username & password
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _context.Users.SingleOrDefaultAsync( x =>
                x.UserName == loginDto.Username);
            if (User == null) return Unauthorized("Invalid UserName");
            using var hmac = new HMACSHA512(User.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != User.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                Username = User.UserName,
                Token = _tokenService.CreateToken(User)
            };
        }


        //this method shows the existance of 
        private async Task<bool> UserExists(string username)
        {
            //here we treat x as one user of the users
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
