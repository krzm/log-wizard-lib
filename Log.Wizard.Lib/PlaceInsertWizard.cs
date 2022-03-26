using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;

namespace Log.Wizard.Lib;
public class PlaceInsertWizard 
    : InsertWizard<ILogUnitOfWork, Place>
{
    public PlaceInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override Place GetEntity()
    {
        return new Place()
        {
            Name = RequiredTextReader.Read(
                new ReadConfig(25, nameof(Place.Name)))
            ,
            Description = RequiredTextReader.Read(
                new ReadConfig(70, nameof(Place.Description)))
        };
    }

    protected override void InsertEntity(Place entity) =>
        UnitOfWork.Place.Insert(entity);
}