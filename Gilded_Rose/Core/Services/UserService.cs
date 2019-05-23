using System;
using System.Collections.Generic;
using System.Linq;
using Gilded_Rose.Core.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Gilded_Rose.Core.Interfaces;
using Gilded_Rose.Helpers;

namespace Gilded_Rose.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        //sample user data 
        private List<ApiUser> _users = new List<ApiUser>
        {
            new ApiUser{Id="1",FirstName = "Admin", LastName = "User", UserName = "admin", Password = "admin", Role = Role.Admin},
            new ApiUser{Id="2",FirstName = "Jim", LastName = "Vrana", UserName = "jvrana", Password = "passwordsAreFun", Role = Role.ApiUser},
            new ApiUser{Id="3",FirstName = "test", LastName = "User", UserName = "testuser", Password = "password2", Role = Role.User}
        };

        public string DecodeBase64(string encodedString)
        {
            var data = System.Convert.FromBase64String(encodedString);
            string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);

            return base64Decoded;
        }

        public string EncodeBase64(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            string base64Encoded = System.Convert.ToBase64String(plainTextBytes);

            return base64Encoded;
        }

        public bool IsBase64(string base64String)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, buffer, out int bytesParsed);
        }

        public ApiUser Authenticate(string userName, string password)
        {
            string DecodedPassword;

            //Decode the password from a base64 string if necessary
            if (IsBase64(password))
            {
                DecodedPassword = DecodeBase64(password);
            }
            else
            {
                DecodedPassword = password;
            }

            ApiUser user = _users.SingleOrDefault(x => x.UserName == userName && x.Password == DecodedPassword);

            //return null if the user was not found
            if (user == null)
            {
                return null;
            }

            //generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            //clear out the password
            user.Password = null;

            return user;
        }
    }
}
