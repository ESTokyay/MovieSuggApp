using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DATA.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MovieSuggApp.Util.BuildToken
{
    public class BuildToken
    {
        public string CreateToken(IConfiguration config,Users kullanici)
        {
            var bytes = Encoding.UTF8.GetBytes(config["JwtToken:SingningKey"]);
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,kullanici.UserName),
                new Claim(JwtRegisteredClaimNames.Email,kullanici.Email)
            };
            
            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: config["JwtToken:Issuer"],
                audience: config["JwtToken:Audience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(Convert.ToInt16(config["JwtToken:Expiration"])),
                signingCredentials: credentials,
                claims: claims
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}