using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyAPINetCore6.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAPINetCore6.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration) { 
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded) { 
                return "";
            }
            // claim no la cac thong tin cua nguoi dung sau khi dang nhap xong, -> them vao trong cookies or jwt bearer token -> cookies , token se duoc gui ve client -> nguoi dung gui nhung thong tin trong nhung lan tiep theo
            var authenclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email), 
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),// key, value , ma value o dang string -> chuyen ve string

            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authenclaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            return await userManager.CreateAsync(user, model.Password);
        }
    }
}
