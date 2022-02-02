using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class LogUpdateWizard 
    : UpdateWizard<ILogUnitOfWork, LogModel>
{
    const string desc = nameof(LogModel.Description);
    const string id = nameof(LogModel.TaskId);
    const string start = nameof(LogModel.Start);
    const string end = nameof(LogModel.End);
    private const string Format = "dd.MM.yyyy HH:mm";
    private readonly IReader<string> optionalTextReader;
    private readonly IReader<DateTime> requiredDateTimeReader;
    private readonly IReader<DateTime?> optionalDateTimeReader;

    public LogUpdateWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IOutput output
        , IReader<string> optionalTextReader
        , IReader<DateTime> requiredDateTimeReader
        , IReader<DateTime?> optionalDateTimeReader) 
            : base(unitOfWork, requiredTextReader, output)
    {
        ArgumentNullException.ThrowIfNull(optionalTextReader);
        ArgumentNullException.ThrowIfNull(requiredDateTimeReader);
        ArgumentNullException.ThrowIfNull(optionalDateTimeReader);

        this.optionalTextReader = optionalTextReader;
        this.requiredDateTimeReader = requiredDateTimeReader;
        this.optionalDateTimeReader = optionalDateTimeReader;
    }

    protected override string GetPropsSelectMenu()
    {
        return $"Select property number. 1-{desc}, 2-{id}, 3-{start}, 4-{end}.";
    }

    protected override LogModel GetById(int id)
    {
        return UnitOfWork.Log.GetByID(id);
    }

    protected override void UpdateEntity(int nr, LogModel model)
    {
        switch (nr)
        {
            case 1:
                model.Description = optionalTextReader.Read(new ReadConfig(70, desc));
                break;
            case 2:
                model.TaskId = int.Parse(RequiredTextReader.Read(new ReadConfig(6, id)));
                break;
            case 3:
                model.Start = requiredDateTimeReader.Read(
                    new ReadConfig(16, start, Format, DateTime.Now.ToString(Format)));
                break;
             case 4:
                model.End = optionalDateTimeReader.Read(
                    new ReadConfig(16, end, Format, DateTime.Now.ToString(Format)));
                break;
        }
    }
}