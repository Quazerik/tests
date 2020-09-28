using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TestsApp
{
    public class TestResultUpdating : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public TestResultUpdating(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Run(() => Update());
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private async Task Update()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var results = await db.TestResults
                    .Include(x => x.Answers)
                    .Include(x => x.Test)
                    .Include(x => x.Test.Questions)
                    .ToListAsync();

                foreach (var res in results)
                {
                    if (res.FinishTime == null &&
                        DateTime.Now > res.StartTime.AddSeconds(res.Test.TimeInSeconds))
                    {
                        res.FinishTest();
                    }
                }

                await db.SaveChangesAsync();
            }
        }
    }
}