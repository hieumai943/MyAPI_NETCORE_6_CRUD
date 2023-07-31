using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPINetCore6.Models;
using MyAPINetCore6.Repositories;

namespace MyAPINetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        public AccountsController(IAccountRepository accountRepo)
        {
            this.accountRepo = accountRepo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }   

            return Unauthorized();
        }   
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }

    }
}
