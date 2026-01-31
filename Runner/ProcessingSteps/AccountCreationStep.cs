using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowPattern;

namespace Runner.ProcessingSteps
{
    public class AccountCreationStep : IProcessingStep
    {
        public string StepName => "AccountCreationStep";

        public void Execute(WorkflowContext context)
        {
            Console.WriteLine("Creating Account...");
            Thread.Sleep(5000);

            string accountNumber = "DNF4356";
            context.Set("AccountNumber", accountNumber);
            Console.WriteLine($"Account Created: {accountNumber}");
        }
    }
}
