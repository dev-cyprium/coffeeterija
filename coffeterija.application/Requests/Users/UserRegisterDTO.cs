using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class UserRegisterDTO : UserLoginDTO
    {
        [Required]
        public string FirstName { get; set; }  

        [Required]
        public string LastName { get; set; }
    }
}
