using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;
using Task = Log.Data.Task;

namespace Log.Wizard.Lib;

public class TaskUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Task>
{
    const string name = nameof(Task.Name);
    const string desc = nameof(Task.Description);
    const string id = nameof(Task.CategoryId);

    public TaskUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override string GetPropsSelectMenu()
    {
        return $"Select property number. 1-{name}, 2-{desc}, 3-{id}.";
    }

    protected override Task GetById(int id)
    {
        return UnitOfWork.Task.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Task model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, name));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, desc));
                break;
            case 3:
                var categoryId = RequiredTextReader.Read(
                    new ReadConfig(6, id));
                ArgumentNullException.ThrowIfNull(categoryId);
                model.CategoryId = int.Parse(categoryId);
                break;
        }
    }
}