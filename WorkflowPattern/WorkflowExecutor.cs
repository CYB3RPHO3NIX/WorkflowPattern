using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public class WorkflowExecutor
    {
        private readonly ProcessingStepRegistry _registry;

        public WorkflowExecutor(ProcessingStepRegistry registry)
        {
            _registry = registry;
        }

        public async Task ExecuteAsync(WorkflowDefinition workflow)
        {
            var context = new WorkflowContext();
            Dictionary<string, bool> executionResult = new Dictionary<string, bool>();
            foreach (var stepName in workflow.Steps)
            {
                var step = _registry.Resolve(stepName);
                bool result = await step.ExecuteAsync(context);
                executionResult[stepName] = result;
            }
            // show the execution results
            Console.WriteLine("Workflow Execution Results:");
            foreach (var kvp in executionResult)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{kvp.Key} - ");
                if (kvp.Value)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Success");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Failed");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }

}
