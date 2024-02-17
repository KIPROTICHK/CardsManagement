using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application.ViewModels
{
    public class LoginRequest
    {

        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 

    }
    public class UserLoginResponse : UserProfile
    {

        public string Token { get; set; } 
        public int ExpiresIn { get; set; }

    }

    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; } 
        public Guid Id { get; set; }
        public string UserType { get; set; }
        
    }
}
