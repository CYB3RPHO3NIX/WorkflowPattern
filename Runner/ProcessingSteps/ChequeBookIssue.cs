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

        public void Execute(WorkflowContext context)
        {
            Console.WriteLine("Cheque Book Issued...");
        }
    }
}
