using System;
using System.Collections.Generic;
using FinanceManagement.Models;
using FinanceManagement.Processors;

namespace FinanceManagement
{
    public class FinanceApp
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {

            // Instantiate a SavingsAccount with account number and initial balance
            var savingsAccount = new SavingsAccount("SAV001", 1000m);
            Console.WriteLine($"Created savings account {savingsAccount.AccountNumber} with initial balance: ${savingsAccount.Balance}");

            // three Transaction records with sample values
            var transaction1 = new Transaction(1, DateTime.Now, 200m, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now, 150m, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 100m, "Entertainment");

            // processors
            var mobileProcessor = new MobileMoneyProcessor();
            var bankProcessor = new BankTransferProcessor();
            var cryptoProcessor = new CryptoWalletProcessor();

            // Process each transaction with assigned processor
            Console.WriteLine("\n Processing Transactions ");

            // Transaction 1 with MobileMoneyProcessor
            mobileProcessor.Process(transaction1);
            savingsAccount.ApplyTransaction(transaction1);
            _transactions.Add(transaction1);

            // Transaction 2 with BankTransferProcessor
            bankProcessor.Process(transaction2);
            savingsAccount.ApplyTransaction(transaction2);
            _transactions.Add(transaction2);

            // Transaction 3 with CryptoWalletProcessor
            cryptoProcessor.Process(transaction3);
            savingsAccount.ApplyTransaction(transaction3);
            _transactions.Add(transaction3);

            // Display final results
            Console.WriteLine($"\nFinal account balance: ${savingsAccount.Balance}");
            Console.WriteLine($"Total transactions processed: {_transactions.Count}");
            Console.WriteLine();
        }
    }
}