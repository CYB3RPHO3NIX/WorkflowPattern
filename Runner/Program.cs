using Runner.ProcessingSteps;
using WorkflowPattern;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkflowContext postOfficeContext = new WorkflowContext();

            List<IProcessingStep> processingSteps = new List<IProcessingStep>()
            {
                new AccountCreationStep(),
                new ChequeBookIssue(),
                new DebitCardIssueStep(),
                new DocumentVerificationStep(),
            };
            var registry = new ProcessingStepRegistry(processingSteps);

            var steps = new List<string>()
            {
                "DocumentVerificationStep",
                "AccountCreationStep",
                "ChequeBookIssue",
                "DebitCardIssueStep"
            };
            WorkflowDefinition workflowDefinition = new WorkflowDefinition(steps);
            WorkflowExecutor executor = new WorkflowExecutor(registry);
            executor.ExecuteAsync(workflowDefinition).GetAwaiter().GetResult();
            Console.WriteLine("All Process Completed.");
            Console.ReadKey();
        }
    }
}
