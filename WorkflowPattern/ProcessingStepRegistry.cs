using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public class ProcessingStepRegistry
    {
        private readonly Dictionary<string, IProcessingStep> _steps;

        public ProcessingStepRegistry(IEnumerable<IProcessingStep> steps)
        {
            _steps = steps.ToDictionary(s => s.StepName);
        }

        public IProcessingStep Resolve(string stepName)
        {
            if(_steps.TryGetValue(stepName, out var step))
            {
                return step;
            }
            else
            {
                throw new InvalidOperationException($"ProcessingStep '{stepName}' is not registered.");
            }
        }
    }

}
