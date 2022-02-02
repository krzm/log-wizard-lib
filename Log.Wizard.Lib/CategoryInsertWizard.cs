using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class CategoryInsertWizard 
    : InsertWizard<ILogUnitOfWork, Data.Category>
{
    public CategoryInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork, requiredTextReader, output)
    {
    }

    protected override Data.Category GetEntity()
    {
        return new Data.Category()
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Data.Category.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Data.Category.Description)))
        };
    }

    protected override void InsertEntity(Data.Category entity) =>
        UnitOfWork.Category.Insert(entity);
}