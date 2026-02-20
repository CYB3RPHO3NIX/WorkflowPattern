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

        public async Task ExecuteAsync(Workflow workflow)
        {
            var context = new WorkflowContext();
            context.Set("Target", 0);
            Dictionary<string, bool> executionResult = new Dictionary<string, bool>();
            var step = workflow.GetSteps().FirstOrDefault();
            while (true)
            {
                //execute
                IProcessingStep processingStep = _registry.Resolve(step._StepName);
                bool result = await processingStep.ExecuteAsync(context);
                executionResult[step._StepName] = result;
                //decide next step
                if(result)
                {
                    if (string.IsNullOrEmpty(step._OnSuccessStepName))
                    {
                        break;
                    }
                    step = workflow.GetStep(step._OnSuccessStepName);
                }else
                {
                    if (string.IsNullOrEmpty(step._OnFailureStepName))
                    {
                        break;
                    }
                    step = workflow.GetStep(step._OnFailureStepName);
                }
            }
        }
    }

}
