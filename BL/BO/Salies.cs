using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public record Salies

        (int Id,
         int ProductId,
         int QuentityForSale,
         double TotalPriceOnSale,
         bool OnlyForTheClub,
         DateTime CampaingStartDate,
         DateTime CampaingEndDate
         )
    {
        public Salies() : this(0, 0, 1, 100, true, DateTime.Now, DateTime.Now)
        {
        }

        public int SaleId { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}



