namespace NexusPatagonia.Application.Interfaces
{
    public interface IExcelImportService
    {
        Task ImportFileAsync(string fileName, Stream stream,DateTime period, Guid companyId);
    }
}
