using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class CategoryUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Data.Category>
{
    public CategoryUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork
                , requiredTextReader
                , output)
    {
    }

    protected override Data.Category GetById(int id)
    {
        return UnitOfWork.Category.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Data.Category model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, nameof(Data.Category.Name)));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, nameof(Data.Category.Description)));
                break;
        }
    }
}