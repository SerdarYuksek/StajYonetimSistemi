﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Model
{
    //User Servicesindeki Personal Tablosunun Identity kütüphanesi ile entegreli Entityleri
    public class Personal : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string TCNO { get; set; }
        public string PersonalNo { get; set; }
        public string Title { get; set; }
        public bool Gender{ get; set; }
        public bool RegistrationCheck { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

    }
}

