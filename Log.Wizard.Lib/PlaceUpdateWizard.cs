using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class PlaceUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, Data.Place>
{
    public PlaceUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output) 
            : base(unitOfWork
                , requiredTextReader
                , output)
    {
    }

    protected override Data.Place GetById(int id)
    {
        return UnitOfWork.Place.GetByID(id);
    }

    protected override void UpdateEntity(int nr, Data.Place model)
    {
        switch (nr)
        {
            case 1:
                model.Name = RequiredTextReader.Read(
                    new ReadConfig(25, nameof(Data.Place.Name)));
                break;
            case 2:
                model.Description = RequiredTextReader.Read(
                    new ReadConfig(70, nameof(Data.Place.Description)));
                break;
        }
    }
}