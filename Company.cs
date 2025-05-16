using SQLite;

namespace Atestat4.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public decimal AnnualRevenueBeforeCollaboration { get; set; }
        public decimal AnnualProfitBeforeCollaboration { get; set; }
        public DateTime CollaborationStartDate { get; set; }
        public string ServicesProvided { get; set; } = string.Empty;
        public decimal FixedFee { get; set; }
        public int NumberOfOwners { get; set; }
        public string OwnerFullName { get; set; } = string.Empty;

        // poți adăuga câmpuri suplimentare:
        // public DateTime PartnershipStart { get; set; }
        // public IList<string> Owners      { get; set; }
    }
}
