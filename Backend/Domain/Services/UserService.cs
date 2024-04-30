﻿using AutoMapper;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Login;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IJWTTokenServices _jWTTokenServices;

        public UserService (IUnitOfWork unitOfWork, IMailService mailService,IMapper mapper,IJWTTokenServices jWTTokenServices)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
            _mapper = mapper;
            _jWTTokenServices = jWTTokenServices;
        }

        public Task<bool> AddAdress(AddressDTO adress)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AuthinticateEmail(AuthinticateEmailDTO randomNumber)
        {
            var x= await _unitOfWork.GetRepositories<User>().Get().Where(r=> r.Username == randomNumber.Username).FirstOrDefaultAsync();
            if (x == null) return false;
            if (x.RandomStringEmailConfirmations != randomNumber.AuthinticationNumber) return false;    
            x.IsEmailConfirmed = true;
            await _unitOfWork.GetRepositories<User>().Update(x);
            return true;
            
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
        public async Task<bool> CheackCredinails(RegisterModelDTO model)
        {
            if (!IsValidEmail(model.Email)) { return false; }
            var x = await _unitOfWork.GetRepositories<User>().Get().Where(x => x.Username == model.Username || x.Email == model.Email).FirstOrDefaultAsync();

            if (x != null) { return false;  }
            return true;
        }

        public async Task<JWTTokensDTO> Login(LoginDTO Dto)
        {
           var user =await _unitOfWork.GetRepositories<User>().Get().Where(r=>r.Username == Dto.Username).FirstOrDefaultAsync();
            var hashed_pass = "";
             using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Dto.Password));
                hashed_pass = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            if (hashed_pass != user.Password) return new JWTTokensDTO { refToken ="",Token="" };
            var temp = _jWTTokenServices.Authenticate(user).Result;
            return new JWTTokensDTO { refToken = temp.refToken,Token = temp.Token };
        }

        public async Task<bool> RecovePasswordRequest(RecoverPasswordRequestDTO passwordRequest)
        {
            var x = await _unitOfWork.GetRepositories<User>().Get().Where(r => (r.Username == passwordRequest.Username || r.Email == passwordRequest.Email) && r.IsEmailConfirmed).FirstOrDefaultAsync();
            if (x == null) return false;
            var random = GenerateRandomString(6);
            _mailService.SendMail(x.Email, "Recover Password", $"hi {x.Name}, forgot your password, heres the code to reseat it {random}, please don't share this code with anyone");
            x.RandomStringEmailConfirmations = random;
            await _unitOfWork.GetRepositories<User>().Update(x);
            return true;
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }

            return sb.ToString();
        }
        public async Task<bool> Register(RegisterModelDTO model)
        {
            if (!CheackCredinails(model).Result) return false;
            var AddedUser = _mapper.Map<User>(model);
            AddedUser.IsEmailConfirmed = false;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
                AddedUser.Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            AddedUser.RandomStringEmailConfirmations = GenerateRandomString(6);
            _mailService.SendMail(AddedUser.Email, "Email Confirmations", $"welcome to mediConnect Plus, your confirmation code is {AddedUser.RandomStringEmailConfirmations}");
            await _unitOfWork.GetRepositories<User>().Add(AddedUser);
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var x = await _unitOfWork.GetRepositories<User>().Get().Where(r => r.Username == resetPassword.Username).FirstOrDefaultAsync();
            if (x == null) return false;
            if (x.RandomStringEmailConfirmations != resetPassword.RandomNumber) return false;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(resetPassword.Password));
                x.Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            await _unitOfWork.GetRepositories<User>().Update(x);
            return true;
        }
    }
}
