using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;

namespace Log.Wizard.Lib;

public class CategoryInsertWizard 
    : InsertWizard<ILogUnitOfWork, Category>
{
    public CategoryInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override Category GetEntity()
    {
        return new Category()
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Category.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Category.Description)))
        };
    }

    protected override void InsertEntity(Category entity) =>
        UnitOfWork.Category.Insert(entity);
}