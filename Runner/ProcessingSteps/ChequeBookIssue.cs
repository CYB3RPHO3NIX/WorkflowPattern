using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowPattern;

namespace Runner.ProcessingSteps
{
    public class ChequeBookIssue : IProcessingStep
    {
        public string StepName => "ChequeBookIssue";
        public async Task<bool> ExecuteAsync(WorkflowContext context)
        {
            try
            {
                await Task.Delay(2000);
                Console.WriteLine("Cheque Book Issued...");
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
