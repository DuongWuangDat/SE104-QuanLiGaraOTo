using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class BrandCarDTO
    {
        public BrandCarDTO()
        {
            this.Receptions = new HashSet<ReceptionDTO>();
            this.RevenueDetails = new HashSet<RevenueDetailDTO>();
        }
        public BrandCarDTO(string name)
        {
            this.Name = name;
            this.Receptions = new HashSet<ReceptionDTO>();
            this.RevenueDetails = new HashSet<RevenueDetailDTO>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<ReceptionDTO> Receptions { get; set; }
        public virtual ICollection<RevenueDetailDTO> RevenueDetails { get; set; }
    }
}
