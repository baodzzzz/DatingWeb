using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
      
        public AccountController(DatingAppContext context) : base(context){

        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new User{
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;    
        }
         [HttpPost("login")]
         public async Task<ActionResult<User>> login(LoginDto loginDto){
             var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
             if(user == null ) return Unauthorized("Invalid Username");

             using var hmac = new HMACSHA512(user.PasswordSalt);
             var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
             for(int i = 0; i < computedHash.Length; i++){
                 if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
             }
             return user;
         }        
        private async Task<bool> UserExists (string username){
          return await _context.Users.AnyAsync(x => x.Username == username.ToLower());  
        }

    }
}