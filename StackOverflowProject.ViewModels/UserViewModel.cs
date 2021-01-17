using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
    public class UserViewModel
    {

        public int UserID { get; set; }
        public string Email { get; set; }
        public string Passwrod { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public bool isAdmin { get; set; }

    }
}
