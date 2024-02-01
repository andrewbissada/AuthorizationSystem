public class FeesService
{
    public decimal GetPaymentFee()
    {
        return UFE.Instance.GetCurrentFee();         // Calls the GetCurrentFee method from UFE to retrieve the current fee
    }
}
