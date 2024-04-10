namespace RSMEnterpriseIntegrationsAPI.Domain.Models
{
    //Some properties that represent foreign keys are hardcoded as default values
    //becuase I would need to add more models than allowed to check if those records
    //before adding new records to the table.

    //I know that I can make raw SQL queries with EF Core, howerver I think that it is
    // too forced and I don't think that that's the goal of the challenge.

    public class SalesOrderHeader
    {
        public int SalesOrderId { get; set; }
        public byte RevisionNumber { get; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public byte Status { get; set; }
        public bool? OnlineOrderFlag { get; set; }
        public string? SalesOrderNumber { get; }
        public string? PurchaseOrderNumber { get; set; }
        public string? AccountNumber { get; set; }
        public int CustomerId { get; set; } = 29825;
        public int? SalesPersonId { get; set; }
        public int? TerritoryId { get; set; }
        public int BillToAddressId { get; set; } = 985;
        public int ShipToAddressId { get; set; } = 985;
        public int ShipMethodId { get; set; } = 5;
        public int? CreditCardId { get; set; }
        public string? CreditCardApprovalCode { get; set; }
        public int? CurrencyRateId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; }
        public string? Comment { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
