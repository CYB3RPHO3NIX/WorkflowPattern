# WorkflowPattern

A lightweight, extensible workflow execution engine for .NET that enables developers to build and orchestrate multi-step business processes with ease.

## 📖 Overview

WorkflowPattern is a C# library that implements a flexible workflow execution framework. It allows you to define, register, and execute a series of processing steps in a configurable order while maintaining shared state across all steps. This pattern is ideal for orchestrating complex business processes that involve multiple sequential operations.

### What Problem Does It Solve?

In many applications, you need to execute a series of operations in a specific order where:
- Each operation is independent and reusable
- Operations need to share data with each other
- The execution order should be configurable without changing code
- New operations can be added without modifying existing code (Open/Closed Principle)

## 🏗️ Architecture & Design Patterns

This library implements a combination of several design patterns:

| Pattern | Purpose | Implementation |
|---------|---------|----------------|
| **Chain of Responsibility** | Sequential execution of steps | Steps execute one after another, each processing the shared context |
| **Registry Pattern** | Decoupling step registration from execution | `ProcessingStepRegistry` stores and resolves steps by name |
| **Strategy Pattern** | Encapsulating algorithms | Each `IProcessingStep` implementation encapsulates different business logic |
| **Dependency Injection** | Loose coupling | Steps are injected into the registry; registry is injected into executor |

## 🧩 Core Components

### Component Overview

| Component | Type | Responsibility |
|-----------|------|----------------|
| `IProcessingStep` | Interface | Defines the contract for workflow steps |
| `WorkflowContext` | Class | Shared state container for passing data between steps |
| `WorkflowDefinition` | Class | Immutable configuration defining step execution order |
| `ProcessingStepRegistry` | Class | Registry for storing and resolving step implementations |
| `WorkflowExecutor` | Class | Orchestrator that executes workflow steps in sequence |

### Detailed Component Descriptions

#### `IProcessingStep`
```csharp
public interface IProcessingStep
{
    string StepName { get; }
    void Execute(WorkflowContext context);
}
```
- **Purpose**: Contract that all workflow steps must implement
- **StepName**: Unique identifier for the step
- **Execute**: Method that performs the step's business logic

#### `WorkflowContext`
```csharp
public class WorkflowContext
{
    public void Set<T>(string key, T value);
    public T Get<T>(string key);
    public bool Contains(string key);
}
```
- **Purpose**: Type-safe dictionary for sharing data between steps
- **Features**: 
  - Generic methods for type safety
  - Simple key-value storage
  - Used to pass information between steps in the workflow

#### `WorkflowDefinition`
```csharp
public class WorkflowDefinition
{
    public IReadOnlyList<string> Steps { get; }
    public WorkflowDefinition(IEnumerable<string> steps);
}
```
- **Purpose**: Defines the sequence of steps to execute
- **Immutable**: Once created, the step order cannot be changed
- **Flexible**: Different workflows can use different step orders

#### `ProcessingStepRegistry`
```csharp
public class ProcessingStepRegistry
{
    public ProcessingStepRegistry(IEnumerable<IProcessingStep> steps);
    public IProcessingStep Resolve(string stepName);
}
```
- **Purpose**: Central registry for all available steps
- **Registration**: Accepts all steps at construction time
- **Resolution**: Retrieves steps by name; throws `InvalidOperationException` if not found

#### `WorkflowExecutor`
```csharp
public class WorkflowExecutor
{
    public WorkflowExecutor(ProcessingStepRegistry registry);
    public void Execute(WorkflowDefinition workflow);
}
```
- **Purpose**: Executes workflow steps in the defined order
- **Creates**: New `WorkflowContext` for each workflow execution
- **Iterates**: Through each step name in the workflow definition
- **Executes**: Each step with the shared context

## 🔄 Execution Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                         APPLICATION START                        │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  1. Create Step Implementations                                 │
│     • DocumentVerificationStep                                  │
│     • AccountCreationStep                                       │
│     • ChequeBookIssue                                          │
│     • DebitCardIssueStep                                       │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  2. Register Steps in ProcessingStepRegistry                    │
│     Registry stores: StepName → IProcessingStep                 │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  3. Define Workflow (Execution Order)                           │
│     WorkflowDefinition with ordered step names                  │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  4. Create WorkflowExecutor with Registry                       │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  5. Execute Workflow                                            │
│     executor.Execute(workflowDefinition)                        │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│  6. WorkflowExecutor Creates WorkflowContext                    │
│     Shared state container for all steps                        │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                    ┌───────────┴───────────┐
                    │   For Each Step Name   │
                    └───────────┬───────────┘
                                │
                                ▼
        ┌───────────────────────────────────────────┐
        │  Registry.Resolve(stepName)               │
        │  Retrieves IProcessingStep implementation │
        └───────────────┬───────────────────────────┘
                        │
                        ▼
        ┌───────────────────────────────────────────┐
        │  step.Execute(context)                    │
        │  • Read from context                      │
        │  • Perform business logic                 │
        │  • Write to context (if needed)           │
        └───────────────┬───────────────────────────┘
                        │
                        └──────► Next Step
                                    │
                                    ▼
┌─────────────────────────────────────────────────────────────────┐
│  7. All Steps Completed                                         │
│     Workflow execution finishes                                 │
└─────────────────────────────────────────────────────────────────┘
```

### Step-by-Step Execution

1. **Initialization**: Create step implementations and register them in the registry
2. **Configuration**: Define the workflow by specifying step names in desired order
3. **Setup**: Create executor with the registry
4. **Execution Start**: Call `executor.Execute(workflowDefinition)`
5. **Context Creation**: Executor creates a new `WorkflowContext`
6. **Step Iteration**: For each step name in the workflow:
   - Resolve the step from the registry
   - Execute the step with the shared context
   - Step can read/write data to the context
7. **Completion**: All steps finish, workflow completes

## 💡 Usage Example

### Basic Implementation

```csharp
using WorkflowPattern;
using Runner.ProcessingSteps;

// 1. Create step implementations
List<IProcessingStep> processingSteps = new List<IProcessingStep>()
{
    new DocumentVerificationStep(),
    new AccountCreationStep(),
    new ChequeBookIssue(),
    new DebitCardIssueStep()
};

// 2. Register steps
var registry = new ProcessingStepRegistry(processingSteps);

// 3. Define workflow execution order
var steps = new List<string>()
{
    "DocumentVerificationStep",
    "AccountCreationStep",
    "ChequeBookIssue",
    "DebitCardIssueStep"
};
WorkflowDefinition workflowDefinition = new WorkflowDefinition(steps);

// 4. Create executor and run workflow
WorkflowExecutor executor = new WorkflowExecutor(registry);
executor.Execute(workflowDefinition);
```

### Example: Bank Account Opening Process

The included example demonstrates a bank account opening workflow:

```
Step 1: DocumentVerificationStep
├─ Verifies customer documents
└─ Takes 5 seconds to complete

Step 2: AccountCreationStep
├─ Creates new bank account
├─ Generates account number (e.g., "DNF4356")
└─ Stores account number in context for downstream steps

Step 3: ChequeBookIssue
└─ Issues cheque book for the account

Step 4: DebitCardIssueStep
├─ Creates debit card
├─ Activates the card
└─ Issues card to customer
```

### Creating Custom Steps

```csharp
public class CustomProcessingStep : IProcessingStep
{
    public string StepName => "CustomProcessingStep";
    
    public void Execute(WorkflowContext context)
    {
        // Read data from previous steps
        if (context.Contains("AccountNumber"))
        {
            var accountNumber = context.Get<string>("AccountNumber");
            Console.WriteLine($"Processing account: {accountNumber}");
        }
        
        // Perform your business logic
        // ...
        
        // Store data for subsequent steps
        context.Set("CustomData", "Some Value");
    }
}
```

## ✅ Advantages

| Advantage | Description |
|-----------|-------------|
| **Modularity** | Each step is independent and can be developed, tested, and maintained separately |
| **Reusability** | Steps can be reused across different workflows |
| **Flexibility** | Execution order is configurable at runtime without code changes |
| **Extensibility** | New steps can be added without modifying existing code (Open/Closed Principle) |
| **Testability** | Each step can be unit tested independently |
| **Maintainability** | Changes to one step don't affect others |
| **Type Safety** | Generic methods in `WorkflowContext` provide compile-time type checking |
| **Simplicity** | Clean, straightforward API that's easy to understand and use |
| **Separation of Concerns** | Business logic is separated into discrete, focused components |
| **Scalability** | Easy to add new workflows or modify existing ones |

## ⚠️ Disadvantages & Limitations

| Disadvantage | Description | Mitigation |
|--------------|-------------|------------|
| **Linear Execution Only** | Steps execute sequentially; no support for parallel execution or conditional branching | Consider adding orchestration logic or use workflow engines for complex scenarios |
| **No Built-in Error Handling** | Exceptions will halt the entire workflow | Implement try-catch within steps or wrap the executor |
| **No Transaction Support** | No automatic rollback if a step fails | Implement compensation logic in steps or use a saga pattern |
| **State Management** | Context is in-memory only; lost if process crashes | Persist context to database if durability is required |
| **No Step Skip/Retry Logic** | Cannot skip failed steps or retry automatically | Implement retry logic within individual steps |
| **Single Context Instance** | All steps share the same context; potential for key conflicts | Use namespaced keys or structured data objects |

## 🎯 When to Use This Pattern

### ✅ Good Use Cases

- **Onboarding Processes**: User registration, account setup, welcome emails
- **Order Processing**: Order validation, payment, inventory, shipping
- **Data Processing Pipelines**: Extract, transform, validate, load operations
- **Business Process Automation**: Document approval, workflow automation
- **Multi-Step Forms**: Wizard-like processes with sequential steps
- **Batch Operations**: Sequential processing of records or files

### ❌ When Not to Use

- **Complex Branching Logic**: If you need extensive if/else workflow paths
- **Parallel Processing**: When steps can/should run concurrently
- **Long-Running Workflows**: Multi-day processes requiring persistence
- **Event-Driven Systems**: When workflows react to external events
- **Distributed Systems**: When steps run on different machines/services

For these scenarios, consider:
- Azure Durable Functions
- AWS Step Functions
- Workflow engines (e.g., Elsa Workflows, Windows Workflow Foundation)
- State machines
- Message-based architectures (e.g., MassTransit, NServiceBus)

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or higher
- Visual Studio 2022 or Visual Studio Code

### Installation

1. Clone the repository:
```bash
git clone https://github.com/CYB3RPHO3NIX/WorkflowPattern.git
```

2. Open the solution:
```bash
cd WorkflowPattern
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the example:
```bash
cd Runner
dotnet run
```

## 🔧 Extending the Framework

### Adding Async Support (Example Enhancement)

```csharp
// Define async interface
public interface IAsyncProcessingStep
{
    string StepName { get; }
    Task ExecuteAsync(WorkflowContext context);
}

// Update executor for async
public class AsyncWorkflowExecutor
{
    private readonly ProcessingStepRegistry _registry;
    
    public async Task ExecuteAsync(WorkflowDefinition workflow)
    {
        var context = new WorkflowContext();
        foreach (var stepName in workflow.Steps)
        {
            var step = _registry.Resolve(stepName);
            if (step is IAsyncProcessingStep asyncStep)
                await asyncStep.ExecuteAsync(context);
            else
                step.Execute(context);
        }
    }
}
```

## 📝 License

This project is licensed under the MIT License - see the LICENSE.txt file for details.

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Report bugs
- Suggest new features
- Submit pull requests
- Improve documentation

## 👤 Author

CYB3RPHO3NIX

---

**Note**: This is a demonstration project showcasing workflow pattern implementation. For production use, consider adding error handling, logging, validation, and persistence as needed for your specific use case.