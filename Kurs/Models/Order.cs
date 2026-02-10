using RepairShopIS.Interfaces;
using System;
using System.Collections.Generic;

namespace RepairShopIS.Models
{
    public class Order : IOrder
    {
        public IClient Client { get; set; }
        public IEmployee Executor { get; set; }
        public ITelevision Television { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public List<string> FixedIssues { get; set; }
        public decimal Cost { get; set; }
        public int WarrantyMonths { get; set; }
        public bool IsFaulty { get; set; }

        public bool IsCompleted
        {
            get { return IssueDate.HasValue; }
        }

        public Order(IClient client, IEmployee executor, ITelevision television,
                     DateTime receiptDate, IEnumerable<string> fixedIssues,
                     decimal cost, int warrantyMonths)
        {
            Client = client;
            Executor = executor;
            Television = television;
            ReceiptDate = receiptDate;
            FixedIssues = new List<string>(fixedIssues ?? new string[0]);
            Cost = cost;
            WarrantyMonths = warrantyMonths;
        }

        public void Complete(DateTime issueDate, bool isFaulty = false)
        {
            if (IsCompleted) return;

            IssueDate = issueDate;
            IsFaulty = isFaulty;

            ((Employee)Executor).RepairedTVs++;  // Кастинг, так как internal set
            if (isFaulty) ((Employee)Executor).FaultyRepairs++;
        }

        public override string ToString()
        {
            return string.Format("#{0:dd.MM.yy} {1} – {2}", ReceiptDate, Client.FullName, Television.Brand);
        }
    }
}