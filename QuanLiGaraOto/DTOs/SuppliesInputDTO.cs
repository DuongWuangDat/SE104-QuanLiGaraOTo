
using System;
using System.Collections.Generic;
namespace QuanLiGaraOto.DTOs
{
    
    
    public class SuppliesInputDTO
    {
        public SuppliesInputDTO()
        {
            this.SuppliesInputDetails = new HashSet<SuppliesInputDetailDTO>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> DateInput { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<SuppliesInputDetailDTO> SuppliesInputDetails { get; set; }
    }
}
