using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class DDJJPdfStrategy : IPdfProcessingStrategy
    {
        public string DocumentType => "DDJJ";

        public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
        {
            // Lógica específica para extraer datos de Maxirest
            // Usar PdfPig o iText7 aquí...
            await Task.CompletedTask;
            return null;
        }
    }
}
