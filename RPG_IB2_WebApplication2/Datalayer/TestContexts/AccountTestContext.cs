﻿using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.TestContexts
{
    public class AccountTestContext
    {
        public bool Inloggen(User user)
        {
            if (user.Email != null && user.Password != null)
            {
                if (user.Email == "luckyluuk.6@gmail.com" && user.Password == "UserLuuk013!")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Registreren(User user)
        {
            if (user.UserName != null || user.UserName != "" && user.Email != null && user.Password != null)
            {
                if (user.Email.Contains("@") && user.Email.Contains(".") && user.Password.Any(char.IsUpper) && user.Password.Any(char.IsLower) && user.Password.Any(c => char.IsDigit(c)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
