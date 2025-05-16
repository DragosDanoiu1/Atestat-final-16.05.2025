using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Atestat4.Models
{
    public class CompanyRevenue
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public DateTime Month { get; set; } // Store the first day of the month (e.g., 2025-04-01)

        public decimal RevenueAmount { get; set; }
    }
}
