// Thread-safe version of CardService
public class ThreadSafeCardService : CardService
{
    // // Define a lock object to synchronize access
    // private readonly object _lock = new object();

    // // Override the Pay method to make it thread-safe
    // public override void Pay(string id, decimal amount)
    // {
    //     // Use 'lock' to ensure only one thread can enter this block at a time
    //     lock (_lock)
    //     {
    //         // Call the base class's Pay method
    //         // 'base.Pay' refers to the Pay method of the parent class, CardService
    //         base.Pay(id, amount);
    //     }
    // }
}
