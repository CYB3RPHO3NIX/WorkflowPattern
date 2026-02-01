using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public interface IProcessingStep
    {
        string StepName { get; }
        Task<bool> ExecuteAsync(WorkflowContext context);
    }
}
