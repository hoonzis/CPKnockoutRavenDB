using System;
using System.Collections.Generic;
using System.Globalization;

namespace AssetManager.Model
{

    public class ChangePasswordModel
    {
        
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public String ErrorMessage { get; set; }
    }

    public class RegisterModel
    {
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
