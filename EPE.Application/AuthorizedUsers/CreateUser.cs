using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EPE.Application.AuthorizedUsers
{
    public class CreateUser
    {
        private UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Request
        {
            public string Username { get; set; }
            public string Email { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            var managerUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };
            
            await _userManager.CreateAsync(managerUser, "password");

            var managerClaim = new Claim("Role", "Manager");

            await _userManager.AddClaimAsync(managerUser, managerClaim);          

            return true;
        }
    }
}