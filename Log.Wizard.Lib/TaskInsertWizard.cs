using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class TaskInsertWizard 
    : InsertWizard<ILogUnitOfWork, Data.Task>
{
    public TaskInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
        : base(unitOfWork, requiredTextReader, output)
    {
    }

    protected override Data.Task GetEntity()
    {
        return new Data.Task
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Data.Task.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Data.Task.Description))
            )
            ,
            CategoryId = int.Parse(
                RequiredTextReader.Read(
                    new ReadConfig(6, nameof(Data.Task.CategoryId))))
        };
    }

    protected override void InsertEntity(Data.Task model)
    {
        UnitOfWork.Task.Insert(model);
    }
}