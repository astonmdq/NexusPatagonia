using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IPdfProcessingStrategy
    {
        string DocumentType { get; }

        Task<IExtractedData> ProcessAsync(Stream pdfStream);
    }
}
