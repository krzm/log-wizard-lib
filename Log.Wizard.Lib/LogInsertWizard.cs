using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Log.Data;

namespace Log.Wizard.Lib;

public class LogInsertWizard 
    : InsertWizard<ILogUnitOfWork, LogModel>
{
    private const string Format = "dd.MM.yyyy HH:mm";
    private readonly IReader<string> requiredTextReader;
    private readonly IReader<string> optionalTextReader;
    private readonly IReader<DateTime> requiredDateTimeReader;
    private readonly IReader<DateTime?> optionalDateTimeReader;

    public LogInsertWizard(
        ILogUnitOfWork unitOfWork
        , IReader<string> requiredTextReader
        , IReader<string> optionalTextReader
        , IOutput output
        , IReader<DateTime> requiredDateTimeReader
        , IReader<DateTime?> optionalDateTimeReader) 
            : base(unitOfWork, requiredTextReader, output)
    {
        this.requiredTextReader = requiredTextReader;
        this.optionalTextReader = optionalTextReader;
        this.requiredDateTimeReader = requiredDateTimeReader;
        this.optionalDateTimeReader = optionalDateTimeReader;

        ArgumentNullException.ThrowIfNull(this.requiredTextReader);
        ArgumentNullException.ThrowIfNull(this.optionalTextReader);
        ArgumentNullException.ThrowIfNull(this.requiredDateTimeReader);
        ArgumentNullException.ThrowIfNull(this.optionalDateTimeReader);
    }

    protected override LogModel GetEntity()
    {
        return new LogModel
        {
            TaskId = int.Parse(requiredTextReader.Read(
                new ReadConfig(6, nameof(LogModel.TaskId))))
            , Start = requiredDateTimeReader.Read(
                new ReadConfig(16, nameof(LogModel.Start), Format, DateTime.Now.ToString(Format)))
            , Description = optionalTextReader.Read(
                new ReadConfig(70, nameof(LogModel.Description)))
            , PlaceId = int.Parse(requiredTextReader.Read(
                new ReadConfig(6, nameof(LogModel.PlaceId), "", "1")))
            , End = optionalDateTimeReader.Read(
                new ReadConfig(16, nameof(LogModel.End), Format))
        };
    }

    protected override void InsertEntity(LogModel model)
    {
        UnitOfWork.Log.Insert(model);
    }
}