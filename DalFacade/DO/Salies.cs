
using System;

namespace DO
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
    }
}
