﻿namespace Dtos.Accounts
{
    public class RegisterRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Confirmpassword { get; set; }
        public required DateTime? Dob { get; set; }
    }
}
