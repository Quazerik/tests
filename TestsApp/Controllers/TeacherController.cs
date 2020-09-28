using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsApp.Enums;
using TestsApp.Mapping;
using TestsApp.Models;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Questions;
using TestsApp.Models.Users;
using TestsApp.ViewModels.Both.Question;
using TestsApp.ViewModels.FromServer;
using TestsApp.ViewModels.ToServer;

namespace TestsApp.Controllers
{
    public class TeacherController : CustomControllerBase
    {
        private readonly AppDbContext _db;
        private readonly QuestionMappingFactory _mappingFactory;

        public TeacherController(UserManager<AppUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _db = dbContext;
            _mappingFactory = new QuestionMappingFactory();
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpGet("/api/teacher/groups")]
        public async Task<ActionResult> GetGroups()
        {
            try
            {
                var groups = await _db.Groups.Where(g => g.TeacherId == GetCurrentUserId())
                    .ProjectTo<GroupViewModel>().ToListAsync();
                return Ok(groups);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpPost("/api/teacher/groups")]
        public async Task<ActionResult> CreateGroup
        (
            [Required] [FromForm] string groupName
        )
        {
            try
            {
                var teacher = await _db.Teachers.FindAsync(GetCurrentUserId());

                var group = new Group {Name = groupName, Teacher = teacher, TeacherId = teacher.Id};
                await _db.Groups.AddAsync(group);
                await _db.SaveChangesAsync();
                
                return Ok(Mapper.Map<GroupViewModel>(group));
//                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpGet("/api/teacher/groups/{group_id}/students")]
        public async Task<ActionResult> GetGroupStudents([FromRoute] [Required] string group_id)
        {
            try
            {
                int.TryParse(group_id, out var id);
                var group = await _db.Groups.FindAsync(id);
                if (group == null) return NotFound(group_id);
                await _db.Entry(group).Collection(x => x.Students).LoadAsync();
                var students = group.Students.Select(Mapper.Map<StudentUserViewModel>).ToList();
                return Ok(students);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpPost("/api/teacher/groups/{group_id}/students")]
        public async Task<ActionResult> RegisterStudent(
            [FromRoute] [Required] string group_id,
            [Required] [FromForm] string studentFullName,
            [Required] [FromForm] string studentEmail
        )
        {
            try
            {
                int.TryParse(group_id, out var id);
                var group = await _db.Groups.FindAsync(id);
                if (group == null) return NotFound(group_id);
                if (GetCurrentUserId() != group.TeacherId) return Forbid();

                var newUser = new StudentUser
                {
                    UserName = studentEmail,
                    Email = studentEmail,
                    FullName = studentFullName,
                    Group = group,
                    GroupId = group.Id
                };
                var password = new Random().Next(10000000, 99999999).ToString();

                var creatingResult = await _userManager.CreateAsync(newUser, password);
                if (!creatingResult.Succeeded)
                    return BadRequest(
                        new MultipleErrorsViewModel(
                            creatingResult.Errors.Select(e => e.Description).ToList()
                        ));

                await Email.SendLoginAndPassword(studentEmail, studentFullName, password);

                return Ok(Mapper.Map<StudentUserViewModel>(newUser));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }


        [Authorize(Roles = "TeacherUser")]
        [HttpGet("/api/teacher/groups/{group_id}/tests")]
        public async Task<ActionResult> GetGroupTests([FromRoute] [Required] string group_id)
        {
            try
            {
                int.TryParse(group_id, out var id);
                var group = await _db.Groups.FindAsync(id);
                if (group == null) return NotFound(group_id);
                var tests = _db.Tests.Where(x => x.GroupId == group.Id);
                foreach (var t in tests)
                {
                    await _db.Entry(t).Collection(s => s.Questions).LoadAsync();
                }
                
                return Ok(tests.AsEnumerable().Select(Mapper.Map<TestExViewModel>).ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpPost("/api/teacher/groups/{group_id}/tests")]
        public async Task<ActionResult> CreateGroupTest
        ([FromRoute] [Required] string group_id,
            [Required] [FromBody] TestCreationViewModel testCreationViewModel)
        {
            try
            {
                int.TryParse(group_id, out var id);
                var group = await _db.Groups.FindAsync(id);
                if (group == null) return NotFound(group_id);

                if (GetCurrentUserId() != group.TeacherId) return Forbid();

                if (testCreationViewModel.TimeInSeconds < 60)
                {
                    ModelState.TryAddModelError("TimeInSeconds",
                        "Время прохождения теста не может быть меньше минуты");
                }

                if (!ModelState.IsValid) return BadRequest(ModelState);
                
                var questions = new List<Question>();
                foreach (var questionModel in testCreationViewModel.Questions)
                {
                    questions.Add(_mappingFactory.Map(questionModel));
                }

                var test = new Test(group, testCreationViewModel, questions);
                
                foreach (Question question in test.Questions)
                    if (question is TwoColumnsQuestion twoColumnsQuestion)
                        twoColumnsQuestion.RandomizeOrder();

                await _db.Tests.AddAsync(test);               
                await _db.SaveChangesAsync();
                return Ok(Mapper.Map<TestExViewModel>(test));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "TeacherUser")]
        [HttpGet("/api/teacher/tests/{test_id}/results")]
        public async Task<ActionResult> GetTestResults
            ([FromRoute] [Required] int test_id)
        {
            try
            {
                var test = await _db.Tests.FindAsync(test_id);
                if (test == null) return NotFound(test_id);

                var testResult = _db.TestResults.Include(s=>s.Student)
                    .Where(x => x.TestId == test_id);
                foreach (var t in testResult)
                {
                    await _db.Entry(t).Collection(s => s.Answers).LoadAsync();
                }
                
                return Ok(testResult.AsEnumerable().Select(Mapper.Map<TestResultViewModel>).ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
