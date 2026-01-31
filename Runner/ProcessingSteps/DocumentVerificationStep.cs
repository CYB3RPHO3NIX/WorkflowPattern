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

        public void Execute(WorkflowContext context)
        {
            Console.WriteLine("Verifying Documents...");
            Thread.Sleep(5000);
            Console.WriteLine("Document Verification Completed.");
        }
    }
}
