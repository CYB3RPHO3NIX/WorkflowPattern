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
        public async Task<bool> ExecuteAsync(WorkflowContext context)
        {
            try
            {
                Console.WriteLine("Creating Account...");
                await Task.Delay(5000);

                string accountNumber = "DNF4356";
                context.Set("AccountNumber", accountNumber);
                Console.WriteLine($"Account Created: {accountNumber}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in {StepName}: {ex.Message}");
                return false;
            }
        }
    }
}
