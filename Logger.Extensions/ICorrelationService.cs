namespace Logger.Extensions
{
    public interface ICorrelationService
    {
        Correlation GetCurrentCorrelation();
        void SetCurrentCorrelation(Correlation correlation);
    }
}
