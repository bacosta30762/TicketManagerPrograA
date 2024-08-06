﻿using Microsoft.AspNetCore.Identity;

namespace Database.Models
{
    public class Role : IdentityRole
    {
        public Role() { }

        public Role(string roleName) : base(roleName) { }
    }
}
