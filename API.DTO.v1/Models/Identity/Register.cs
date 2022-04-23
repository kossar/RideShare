﻿namespace API.DTO.v1.Models.Identity;

public class Register
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PasswordConfirmation { get; set; } = default!;
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
}