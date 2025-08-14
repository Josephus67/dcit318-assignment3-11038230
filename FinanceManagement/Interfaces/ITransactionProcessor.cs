using FinanceManagement.Models;

namespace FinanceManagement.Interfaces
{
    // Interface for transaction processing
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}