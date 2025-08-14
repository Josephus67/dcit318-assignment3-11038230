using System;
using FinanceManagement.Interfaces;
using FinanceManagement.Models;

namespace FinanceManagement.Processors
{
    // Concrete processor implementations
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Bank Transfer: Amount = ${transaction.Amount}, Category = {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Mobile Money: Amount = ${transaction.Amount}, Category = {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Processing Crypto Wallet: Amount = ${transaction.Amount}, Category = {transaction.Category}");
        }
    }
}