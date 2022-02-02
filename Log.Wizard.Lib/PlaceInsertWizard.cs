using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;
public class PlaceInsertWizard 
    : InsertWizard<ILogUnitOfWork, Data.Place>
{
    public PlaceInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork, requiredTextReader, output)
    {
    }

    protected override Data.Place GetEntity()
    {
        return new Data.Place()
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Data.Place.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Data.Place.Description)))
        };
    }

    protected override void InsertEntity(Data.Place entity) =>
        UnitOfWork.Place.Insert(entity);
}