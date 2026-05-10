using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Salies
    {
        public int Id { get; init; }
        public int ProductId { get; init; }
        public int? QuentityForSale { get; set; }
        public double? TotalPriceOnSale { get; set; }
        public bool? OnlyForTheClub { get; set; }
        public DateTime? CampaingStartDate { get; set; }
        public DateTime? CampaingEndDate { get; set; }

        public Salies ():this(-1,0,0,0.0,false,DateTime.Now, DateTime.Now) { }

        public Salies(int id, int pId, int qForSale, double totalPriceInSale, bool oftc, DateTime campaingStartDate, DateTime campaingEndDate)
        {
            Id = id;
            ProductId = pId;
            QuentityForSale = qForSale;
            TotalPriceOnSale = totalPriceInSale;
            OnlyForTheClub = oftc;
            CampaingStartDate = campaingStartDate;
            CampaingEndDate = campaingEndDate;
        }
        public override string ToString() => this.ToStringProperty();
    }

}



