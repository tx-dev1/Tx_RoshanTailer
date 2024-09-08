using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestRoshanTailor.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }

		[NotMapped]
		[Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //public bool AcceptTerms { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters.")]
        public string Username { get; set; }
       
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }        
    }

    public partial class MeasurementViewModel
    {
        public int MId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfOrder { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string BillingDetails { get; set; }
        public string MeasureMentDetails { get; set; }        
    }
}