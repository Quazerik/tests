using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TestsApp.Models;
using TestsApp.Models.Test;
using TestsApp.Models.Test.Answers;
using TestsApp.Models.Test.Json;
using TestsApp.Models.Test.Questions;
using TestsApp.Models.Users;

namespace TestsApp
{
    public static class ExampleData
    {
        public static void FillDatabase(AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            var adminUser = new AdminUser()
            {
                AdminData = "SomeAdminData",
                UserName = "admin@a.a",
                Email = "admin@a.a"
            };

            var result = userManager.CreateAsync(adminUser, "pass3").Result;
            if (!result.Succeeded)
            {
                throw new Exception("can't create admin");
            }


            var teacher = new TeacherUser()
            {
                FullName = "First Teacher",
                Email = "teacher1@a.a",
                UserName = "teacher1@a.a"
            };

            if (!userManager.CreateAsync(teacher, "pass3").Result.Succeeded)
                throw new Exception("can't create teacher");

            var group = new Group()
            {
                Teacher = teacher,
                Name = "First Group"
            };

            dbContext.Groups.Add(group);

            dbContext.SaveChanges();

            var test = new Test()
            {
                TimeInSeconds = 3600,
                Name = "test1",
                Group = group,
                Questions = new List<Question>()
                {
                    new OneChoiceQuestion()
                    {
                        Number = 0,
                        Score = 2,
                        Text = "q1 ?",
                        AnswerVariants = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "answ1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url1"
                            }
                        },
                        RightAnswerNum = 1
                    },
                    new MultiChoiceQuestion()
                    {
                        Number = 1,
                        Score = 2,
                        Text = "q2 ?",
                        AnswerVariants = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "answ2"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url2"
                            }
                        },
                        RightAnswersNums = new[] {0, 1},
                    },
                    new TwoColumnsQuestion()
                    {
                        Number = 2,
                        Score = 2,
                        Text = "q3 ?",
                        LeftColumn = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "left1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url_left_2"
                            }
                        },
                        RightColumn = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "right1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url_right_2"
                            }
                        },
                        Connections = new (int, int)[]
                        {
                            (0, 1),
                            (1, 0)
                        }
                    },
                    new OpenAnswerQuestion()
                    {
                        Number = 3,
                        Score = 2,
                        Text = "q4",
                        RightAnswer = "asd"
                    }
                }
            };

            var test2 = new Test()
            {
                TimeInSeconds = 3600,
                Name = "test2",
                Group = group,
                Questions = new List<Question>()
                {
                    new OneChoiceQuestion()
                    {
                        Number = 0,
                        Score = 2,
                        Text = "q1 ?",
                        AnswerVariants = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "answ1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url1"
                            }
                        },
                        RightAnswerNum = 1
                    },
                    new MultiChoiceQuestion()
                    {
                        Number = 1,
                        Score = 2,
                        Text = "q2 ?",
                        AnswerVariants = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "answ2"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url2"
                            }
                        },
                        RightAnswersNums = new[] {0, 1},
                    },
                    new TwoColumnsQuestion()
                    {
                        Number = 2,
                        Score = 2,
                        Text = "q3 ?",
                        LeftColumn = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "left1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url_left_2"
                            }
                        },
                        RightColumn = new AnswerVariant[]
                        {
                            new TextAnswerVariant()
                            {
                                Text = "right1"
                            },
                            new ImageAnswerVariant()
                            {
                                ImageUrl = "url_right_2"
                            }
                        },
                        Connections = new (int, int)[]
                        {
                            (0, 1),
                            (1, 0)
                        }
                    },
                    new OpenAnswerQuestion()
                    {
                        Number = 3,
                        Score = 2,
                        Text = "q4",
                        RightAnswer = "asd"
                    }
                }
            };

            dbContext.Tests.Add(test);
            dbContext.Tests.Add(test2);

            var student = new StudentUser()
            {
                FullName = "First Student",
                Email = "student1@a.a",
                UserName = "student1@a.a",
                Group = group
            };

            if (!userManager.CreateAsync(student, "pass3").Result.Succeeded)
                throw new Exception("can't create student");

            TestResult testResult = new TestResult()
            {
                Student = student,
                Test = test,
                Answers = new List<Answer>()
            };

            testResult.Answers.Add(new OneChoiceAnswer()
            {
                QuestionNumber = 0,
                ChosenAnswerNum = 1
            });

            testResult.Answers.Add(new MultiChoiceAnswer()
            {
                QuestionNumber = 1,
                AnswersNums = new[] { 1, 0 }
            });

            testResult.Answers.Add(new TwoColumnsAnswer()
            {
                QuestionNumber = 2,
                Connections = new (int, int)[] { (1, 0), (0, 1) }
            });

            testResult.Answers.Add(new OpenAnswerAnswer()
            {
                QuestionNumber = 3,
                Answer = "asd"
            });

            testResult.FinishTest();

            Console.WriteLine($"res: {testResult.Result}");

            dbContext.TestResults.Add(testResult);

            dbContext.SaveChanges();
        }
    }
}
