using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;

namespace Log.Wizard.Lib;

public class CategoryUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Category>
{
    public CategoryUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork
                , requiredTextReader
                , log)
    {
    }

    protected override Category GetById(int id)
    {
        return UnitOfWork.Category.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Category model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, nameof(Category.Name)));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, nameof(Category.Description)));
                break;
        }
    }
}