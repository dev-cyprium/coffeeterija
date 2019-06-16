using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
