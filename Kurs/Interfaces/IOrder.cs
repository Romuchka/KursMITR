using System;
using System.Collections.Generic;

namespace RepairShopIS.Interfaces
{
    public interface IOrder
    {
        IClient Client { get; set; }
        IEmployee Executor { get; set; }
        ITelevision Television { get; set; }
        DateTime ReceiptDate { get; set; }
        DateTime? IssueDate { get; set; }
        List<string> FixedIssues { get; set; }
        decimal Cost { get; set; }
        int WarrantyMonths { get; set; }
        bool IsFaulty { get; set; }
        bool IsCompleted { get; }
        void Complete(DateTime issueDate, bool isFaulty = false);
    }
}