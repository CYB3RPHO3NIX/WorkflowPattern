using Runner.ProcessingSteps;
using WorkflowPattern;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IProcessingStep> processingSteps = new List<IProcessingStep>()
            {
                new AccountCreationStep(),
                new ChequeBookIssue(),
                new DebitCardIssueStep(),
                new DocumentVerificationStep(),
            };
            var registry = new ProcessingStepRegistry(processingSteps);

            List<Step> steps = new List<Step>() 
            {
                new Step("DocumentVerificationStep").OnSuccess("AccountCreationStep"),
                new Step("AccountCreationStep").OnSuccess("ChequeBookIssue"),
                new Step("ChequeBookIssue").OnSuccess("DebitCardIssueStep"),
                new Step("DebitCardIssueStep").OnFailure("ChequeBookIssue")
            };

            
            Workflow workflowDefinition = new Workflow(steps);
            WorkflowExecutor executor = new WorkflowExecutor(registry);
            executor.ExecuteAsync(workflowDefinition).GetAwaiter().GetResult();
            Console.WriteLine("All Process Completed.");
            Console.ReadKey();
        }
    }
}
