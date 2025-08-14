using System;

namespace FinanceManagement.Models
{
    // Define Transaction record - immutable data structure
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
}