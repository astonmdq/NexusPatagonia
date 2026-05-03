namespace NexusPatagonia.Domain.Interfaces
{
    public interface IExcelStrategy
    {
        bool CanHandle(string fileExtension);
        Task ProcessAsync(Stream stream, DateTime period, Guid companyId);
    }
}
