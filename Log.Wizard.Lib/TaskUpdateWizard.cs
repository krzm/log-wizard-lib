using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class TaskUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Data.Task>
{
    const string name = nameof(Data.Task.Name);
    const string desc = nameof(Data.Task.Description);
    const string id = nameof(Data.Task.CategoryId);

    public TaskUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork, requiredTextReader, output)
    {
    }

    protected override string GetPropsSelectMenu()
    {
        return $"Select property number. 1-{name}, 2-{desc}, 3-{id}.";
    }

    protected override Data.Task GetById(int id)
    {
        return UnitOfWork.Task.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Data.Task model)
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
                model.CategoryId = int.Parse(RequiredTextReader.Read(
                    new ReadConfig(6, id)));
                break;
        }
    }
}