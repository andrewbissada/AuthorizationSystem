// Define a Singleton class to manage Universal Fees Exchange (UFE)
public sealed class UFE
{
    // Lazy initialization of the Singleton instance
    // This ensures that only one instance of UFE is ever created
    private static readonly Lazy<UFE> instance = new Lazy<UFE>(() => new UFE());
    // Store the current fee value
    private decimal currentFee = 1;

    // Private constructor to ensure no more instances can be created
    private UFE() { }

    // Public method to access the Singleton instance
    public static UFE Instance => instance.Value;

    // Method to update the fee based on a random decimal
    public void UpdateFee()
    {
        decimal newMultiplier = new Random().Next(0, 200) / 100.0M;
        currentFee *= newMultiplier;
    }

    // Method to get the current fee value
    public decimal GetCurrentFee()
    {
        return currentFee;
    }
}
