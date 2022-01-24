namespace Logger.Extensions
{
    public interface ICorrelationService
    {
        Correlation GetCurrentCorrelation();
    }
}
