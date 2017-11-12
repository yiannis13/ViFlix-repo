﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace ViFlix.DataAccess.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
        }

        public AppRole(string roleName)
            : base(roleName)
        {
        }

    }
}