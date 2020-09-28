using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsApp.Models.Users;
using TestsApp.ViewModels.FromServer;

namespace TestsApp.Controllers
{
    public class AdminController : CustomControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(UserManager<AppUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _db = dbContext;
        }

        [Authorize(Roles = "AdminUser")]
        [HttpGet("/api/admin/teachers")]
        public async Task<ActionResult> GetTeachers()
        {
            try
            {
                var teachers = await _db.Teachers.ProjectTo<TeacherUserViewModel>().ToListAsync();
                return Ok(teachers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Регистрация нового преподавателя
        /// </summary>
        /// <remarks>
        /// Требуемая роль: **Admin**.
        /// После регистрации на указанный email будет отправлено письмо с паролем.
        /// </remarks>
        /// <param name="email">Email нового преподавателя</param>
        /// <param name="full_name">Полное имя нового преподавателя</param>
        /// <response code="403">Текущий пользователь не Admin</response>
        [HttpPost("/api/admin/teachers")]
        [Authorize(Roles = "AdminUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(MultipleErrorsViewModel), 400)]
        public async Task<ActionResult> RegisterTeacher
        (
            [Required] [FromForm] [EmailAddress] string email,
            [Required] [FromForm] string full_name
        )
        {
            TeacherUser newUser = new TeacherUser()
            {
                UserName = email,
                Email = email,
                FullName = full_name
            };
            string password = new Random().Next(10000000, 99999999).ToString();

            var creatingResult = await _userManager.CreateAsync(newUser, password);

            if (!creatingResult.Succeeded)
            {
                return StatusCode(
                    (int) HttpStatusCode.BadRequest,
                    new MultipleErrorsViewModel(
                        creatingResult.Errors.Select(e => e.Description).ToList()
                    )
                );
            }

            await Email.SendLoginAndPassword(email, full_name, password);

            return new OkResult();
        }
    }
}
