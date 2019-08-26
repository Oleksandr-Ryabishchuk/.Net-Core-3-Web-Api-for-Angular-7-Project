using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApi.Models;
using AuthApi.SettingsModels;
using AuthApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettings;
        public AdminPanelController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationSettings = appSettings.Value;

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        //POST: /api/AdminPanel/AddNewUser
        public async Task<Object> AddNewUser(ApplicationUserModel model)
        {
            model.Role = "User";
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.FullName,
                PasswordHash = model.Password
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdminView")]
        //POST: /api/AdminPanel/AddNewUser
        public async Task<Object> ViewUsers(ApplicationUserModel model)
        {
            var users = _userManager.Users.Select(u =>
            new
            {
                Email = u.Email,
                Name = u.UserName,
                UserName = u.UserName,
                Password = u.PasswordHash

            });
            return Ok(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdminEdit")]
        //POST: /api/AdminPanel/AddNewUser
        public async Task<Object> UpdateUser(ApplicationUserModel model)
        {
            var changeUser = await _userManager.FindByEmailAsync(model.Email);

            changeUser.Email = model.Email;
            changeUser.Name = model.FullName;
            changeUser.UserName = model.UserName;
            changeUser.PasswordHash = model.Password;

            await _userManager.UpdateAsync(changeUser);

            var users = _userManager.Users.Select(u =>
            new
            {
                Email = u.Email,
                Name = u.UserName,
                UserName = u.UserName,
                Password = u.PasswordHash

            });


          

            return Ok(users);           
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("ForAdminDelete")]
        //POST: /api/AdminPanel/AddNewUser
        public async Task<Object> DeleteUser(ApplicationUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            await _userManager.DeleteAsync(user);

            var users = _userManager.Users.Select(u =>
            new
            {
                Email = u.Email,
                Name = u.UserName,
                UserName = u.UserName,
                Password = u.PasswordHash

            });
            return Ok(users);
            
        }
    }
}