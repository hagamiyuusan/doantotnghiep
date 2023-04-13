﻿using doan.DTO;
using doan.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace doan.Interface
{
    public interface IUserService
    {
        public Task<string> Authencate(AppUserLogin request);

        public Task<bool> Register(AppUserRegistration request);

        public Task<bool> ChangePassword(AppUserChangePassword request);

    }
}