using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class PersistenceCoordinator : IPersistenceCoordinator
    {
        private readonly IEnumerable<IPersistenceStrategy> _strategies;

        public PersistenceCoordinator(IEnumerable<IPersistenceStrategy> strategies) => _strategies = strategies;

        public async Task SaveAsync(IExtractedData data)
        { 
            var strategy = _strategies.FirstOrDefault(s => s.CanHandle(data));

            if (strategy == null)
                throw new Exception($"No hay una estrategia de guardado para {data.GetType().Name}");
            
            await strategy.SaveAsync(data);
        }
    }
}
