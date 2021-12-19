using BusinessLibrary.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApi.Jobs
{
    public class MonthlyBillsJob : IJob
    {
        private readonly IFacturaService Service;

        public MonthlyBillsJob(IFacturaService Service)
        {
            this.Service = Service;
        }

        public Task Execute(IJobExecutionContext context)
        {
            this.Service.GenerateBills();

            return Task.CompletedTask;
        }
    }
}
