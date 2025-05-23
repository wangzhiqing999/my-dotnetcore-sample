﻿using System;
using System.Threading.Tasks;
using W4113_AntDesignProServer.Models;

namespace W4113_AntDesignProServer.Services
{
    public interface IAccountService
    {
        Task LoginAsync(LoginParamsType model);
        Task<string> GetCaptchaAsync(string modile);
    }

    public class AccountService : IAccountService
    {
        private readonly Random _random = new Random();

        public Task LoginAsync(LoginParamsType model)
        {
            // todo: login logic
            return Task.CompletedTask;
        }

        public Task<string> GetCaptchaAsync(string modile)
        {
            var captcha = _random.Next(0, 9999).ToString().PadLeft(4, '0');
            return Task.FromResult(captcha);
        }
    }
}