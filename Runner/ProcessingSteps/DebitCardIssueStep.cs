using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowPattern;

namespace Runner.ProcessingSteps
{
    public class DebitCardIssueStep : IProcessingStep
    {
        public string StepName => "DebitCardIssueStep";
        public async Task<bool> ExecuteAsync(WorkflowContext context)
        {
            try
            {
                Console.WriteLine("Creating Debit Card...");
                await Task.Delay(2000);
                Console.WriteLine("Activating Card...");
                await Task.Delay(2000);
                Console.WriteLine("Card Issued...");
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
