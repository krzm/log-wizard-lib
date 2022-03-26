using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;
using Task = Log.Data.Task;

namespace Log.Wizard.Lib;

public class TaskInsertWizard 
    : InsertWizard<ILogUnitOfWork, Task>
{
    public TaskInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
        : base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override Task GetEntity()
    {
        var categoryId = RequiredTextReader.Read(
            new ReadConfig(
                6
                , nameof(Task.CategoryId)));
        ArgumentNullException.ThrowIfNull(categoryId);
        return new Task
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Task.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Task.Description)))
            ,
            CategoryId = int.Parse(categoryId)
        };
    }

    protected override void InsertEntity(Task model)
    {
        UnitOfWork.Task.Insert(model);
    }
}