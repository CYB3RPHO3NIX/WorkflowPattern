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

        public void Execute(WorkflowDefinition workflow)
        {
            var context = new WorkflowContext();

            foreach (var stepName in workflow.Steps)
            {
                var step = _registry.Resolve(stepName);
                step.Execute(context);
            }
        }
    }

}
