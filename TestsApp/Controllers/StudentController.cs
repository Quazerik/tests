using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestsApp.Models.Users;
using TestsApp.ViewModels.FromServer;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Answers;
using TestsApp.ViewModels.Both.Answer;
using TestsApp.Mapping;

namespace TestsApp.Controllers
{
    public class StudentController : CustomControllerBase
    {
        private readonly AppDbContext _db;

        public StudentController(UserManager<AppUser> userManager, AppDbContext dbContext) : base(userManager)
        {
            _db = dbContext;
        }

        /// <summary>
        /// Получение списка доступных (и завершенных) тестов, с краткой информацией по каждому
        /// (количество вопросов, время, баллы за тест (если уже пройден)...)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "StudentUser")]
        [HttpGet("/api/student/tests")]
        public async Task<ActionResult> GetTests()
        {
            try
            {
                var user = await _db.Students.FindAsync(GetCurrentUserId());

                var results = await _db.TestResults
                    .Where(x => x.StudentId == user.Id)
                    .Include(x => x.Test)
                    .ToListAsync();
                var tests = await _db.Tests
                    .Where(x => x.GroupId == user.GroupId && !results.Any(y => y.TestId == x.Id))
                    .Include(x => x.Questions)
                    .Select(t => Mapper.Map<StartingTestViewModel>(t))
                    .ToListAsync();
                var continuing = results
                    .Where(x => x.FinishTime == null)
                    .Select(t => Mapper.Map<ContinuingTestViewModel>(t))
                    .ToList();
                var finished = results
                    .Where(x => x.FinishTime != null)
                    .Select(t => Mapper.Map<TestResultViewModel>(t))
                    .ToList();

                return Ok(new AllTestsViewModel()
                {
                    StartingTests = tests,
                    ContinuingTests = continuing,
                    FinishedTests = finished
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "StudentUser")]
        [HttpPost("/api/student/tests/{test_id}/start")]
        public async Task<ActionResult> StartTest
            ([FromRoute] [Required] int test_id)
        {
            try
            {
                var test = await _db.Tests.FindAsync(test_id);
                if (test == null) return NotFound(test_id);

                var student = await _db.Students.FindAsync(GetCurrentUserId());

                if (test.GroupId != student.GroupId) return Forbid();
                if (await _db.TestResults.FirstOrDefaultAsync(x => x.StudentId == student.Id && x.TestId == test_id) != null)
                    return Forbid();

                var testResult = new TestResult
                {
                    Student = student,
                    Test = test,
                };
                await _db.TestResults.AddAsync(testResult);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Получение списка вопросов теста.
        /// </summary>
        /// <param name="test_id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Authorize(Roles = "StudentUser")]
        [HttpGet("/api/student/tests/{test_id}/questions")]
        public async Task<ActionResult> GetTestQuestions
            ([FromRoute] [Required] int test_id)
        {
            try
            {
                var student = await _db.Students.FindAsync(GetCurrentUserId());

                var testResult = await _db.TestResults
                    .Include(x => x.Test)
                    .Include(x => x.Test.Questions)
                    .FirstOrDefaultAsync(x => x.TestId == test_id && x.StudentId == student.Id);
                if (testResult == null) return NotFound(test_id);
                if (testResult.FinishTime != null) return Forbid();

                if (testResult.Test.GroupId != student.GroupId) return Forbid();

                return Ok(Mapper.Map<GetTestViewModel>(testResult));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "StudentUser")]
        [HttpPost("/api/student/tests/{test_id}/questions/{question_num}/answer")]
        public async Task<ActionResult> Answer
        ([FromRoute] [Required] int test_id,
            [FromRoute] [Required] int question_num,
            [FromBody] AnswerViewModel answerViewModel)
        {
            var test = await _db.Tests.FindAsync(test_id);
            if (test == null) return NotFound(test_id);

            var student = await _db.Students.FindAsync(GetCurrentUserId());

            if (test.GroupId != student.GroupId) return Forbid();
            var result = await _db.TestResults.FirstOrDefaultAsync(x => x.StudentId == student.Id && x.TestId == test_id);
            if (result == null || result.FinishTime != null)
                return Forbid();

            AnswerMappingFactory amf = new AnswerMappingFactory();

            var newAnswer = amf.Map(answerViewModel);
            newAnswer.TestResultId = result.Id;
            newAnswer.QuestionNumber = question_num;

            var answer = await _db.Answers.FirstOrDefaultAsync(x => x.TestResultId == result.Id && x.QuestionNumber == question_num);

            if (answer != null)
                _db.Answers.Remove(answer);

            await _db.Answers.AddAsync(newAnswer);
            await _db.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Досрочное завершение теста.
        /// Тесты автоматически завершаются по истечении времени.
        /// </summary>
        /// <param name="test_id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Authorize(Roles = "StudentUser")]
        [HttpPost("/api/student/tests/{test_id}/finish")]
        public async Task<ActionResult> FinishTest
            ([FromRoute] [Required] int test_id)
        {
            try
            {
                var test = await _db.Tests.FindAsync(test_id);
                if (test == null) return NotFound(test_id);

                var student = await _db.Students.FindAsync(GetCurrentUserId());

                if (test.GroupId != student.GroupId) return Forbid();

                var testResult = await _db.TestResults
                    .Include(x => x.Answers)
                    .Include(x => x.Test)
                    .Include(x => x.Test.Questions)
                    .Include(x => x.Student)
                    .FirstOrDefaultAsync(x => x.TestId == test.Id && x.StudentId == student.Id);
                if (testResult == null) return NotFound(test_id);
                if (testResult.FinishTime != null) return Forbid();

                testResult.FinishTest();
                await _db.SaveChangesAsync();

                return Ok(Mapper.Map<TestResultViewModel>(testResult));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}
