namespace NexusPatagonia.Domain.Interfaces
{
    public interface IProfitabilityReportRepository
    {
        Task<Tuple<decimal, decimal>> GetPreviousFeeFc(Guid companyId, DateTime currentPeriod);
    }
}
