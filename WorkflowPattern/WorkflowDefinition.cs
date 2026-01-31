using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowPattern
{
    public class WorkflowDefinition
    {
        public IReadOnlyList<string> Steps { get; }

        public WorkflowDefinition(IEnumerable<string> steps)
        {
            Steps = steps.ToList();
        }
    }

}
