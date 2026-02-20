using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public class Workflow
    {
        private IReadOnlyList<Step> _steps;
        public Workflow(IEnumerable<Step> steps)
        {
            _steps = steps.ToList().AsReadOnly();
        }
        public IEnumerable<Step> GetSteps()
        {
            return _steps;
        }
        public Step GetStep(string stepName)
        {
            return _steps.FirstOrDefault(s => s._StepName == stepName);
        }
    }
}
