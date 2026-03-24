namespace NexusPatagonia.Interfaces
{
    public interface IPdfProcessingStrategy
    {
        string DocumentType { get; }

        Task ProcessAsync(Stream pdfStream);
    }
}
