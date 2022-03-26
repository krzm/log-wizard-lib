using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;
using Serilog;

namespace Log.Wizard.Lib;

public class PlaceUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Place>
{
    public PlaceUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , ILogger log) 
            : base(unitOfWork
                , requiredTextReader
                , log)
    {
    }

    protected override Place GetById(int id)
    {
        return UnitOfWork.Place.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Place model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, nameof(Place.Name)));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, nameof(Place.Description)));
                break;
        }
    }
}