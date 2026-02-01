using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowPattern;

namespace Runner.ProcessingSteps
{
    public class DocumentVerificationStep : IProcessingStep
    {
        public string StepName => "DocumentVerificationStep";

        public async Task<bool> ExecuteAsync(WorkflowContext context)
        {
            try
            {
                Console.WriteLine("Verifying Documents...");
                await Task.Delay(5000);
                Console.WriteLine("Document Verification Completed.");
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
