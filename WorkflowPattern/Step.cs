using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public class Step
    {
        public string _StepName { get; set; }
        public string? _OnSuccessStepName { get; set; } = null;
        public string? _OnFailureStepName { get; set; } = null;

        public Step(string stepName)
        {
            _StepName = stepName;
        }
        public Step OnSuccess(string stepName)
        {
            _OnSuccessStepName = stepName;
            return this;
        }
        public Step OnFailure(string stepName)
        {
            _OnFailureStepName = stepName;
            return this;
        }
    }
}
