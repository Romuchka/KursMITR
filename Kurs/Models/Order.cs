using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RepairShopIS.Models
{
    public class Order
    {
        public Client Client { get; set; }
        public Employee Executor { get; set; }
        public Television Television { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public List<string> FixedIssues { get; set; } // Изменено на List<string> для сериализации (было IReadOnlyList)
        public decimal Cost { get; set; }
        public int WarrantyMonths { get; set; }
        public bool IsFaulty { get; set; }

        public bool IsCompleted
        {
            get { return IssueDate.HasValue; }
        }

        public Order(Client client, Employee executor, Television television,
                     DateTime receiptDate, IEnumerable<string> fixedIssues,
                     decimal cost, int warrantyMonths)
        {
            Client = client;
            Executor = executor;
            Television = television;
            ReceiptDate = receiptDate;
            FixedIssues = new List<string>(fixedIssues ?? new string[0]); // Изменено на List
            Cost = cost;
            WarrantyMonths = warrantyMonths;
        }

        public void Complete(DateTime issueDate, bool isFaulty = false)
        {
            if (IsCompleted) return;

            IssueDate = issueDate;
            IsFaulty = isFaulty;

            Executor.RepairedTVs++;
            if (isFaulty) Executor.FaultyRepairs++;
        }

        public override string ToString()
        {
            return string.Format("#{0:dd.MM.yy} {1} – {2}", ReceiptDate, Client.FullName, Television.Brand);
        }
    }
}