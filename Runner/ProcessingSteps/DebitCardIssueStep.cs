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

        public void Execute(WorkflowContext context)
        {
            Console.WriteLine("Creating Debit Card...");
            Thread.Sleep(2000);
            Console.WriteLine("Activating Card...");
            Thread.Sleep(2000);
            Console.WriteLine("Card Issued...");
        }
    }
}
