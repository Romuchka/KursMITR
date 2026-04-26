using RepairShopIS.Interfaces;
using System;
using System.Collections.Generic;

namespace RepairShopIS.Models
{
    public class Order : IOrder
    {
        // Concrete properties are used by JSON serializer.
        public Client Client { get; set; }
        public Employee Executor { get; set; }
        public Television Television { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public List<string> FixedIssues { get; set; }
        public decimal Cost { get; set; }
        public int WarrantyMonths { get; set; }
        public bool IsFaulty { get; set; }

        IClient IOrder.Client
        {
            get { return Client; }
            set { Client = value as Client; }
        }

        IEmployee IOrder.Executor
        {
            get { return Executor; }
            set { Executor = value as Employee; }
        }

        ITelevision IOrder.Television
        {
            get { return Television; }
            set { Television = value as Television; }
        }

        public bool IsCompleted
        {
            get { return IssueDate.HasValue; }
        }

        public Order()
        {
            FixedIssues = new List<string>();
        }

        public Order(IClient client, IEmployee executor, ITelevision television,
                     DateTime receiptDate, IEnumerable<string> fixedIssues,
                     decimal cost, int warrantyMonths)
        {
            Client = client as Client ?? throw new ArgumentException("Client must be of type Client", nameof(client));
            Executor = executor as Employee ?? throw new ArgumentException("Executor must be of type Employee", nameof(executor));
            Television = television as Television ?? throw new ArgumentException("Television must be of type Television", nameof(television));
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

            Executor.RepairedTVs++;
            if (isFaulty) Executor.FaultyRepairs++;
        }

        public override string ToString()
        {
            return string.Format("#{0:dd.MM.yy} {1} – {2}", ReceiptDate, Client.FullName, Television.Brand);
        }
    }
}