
using System;
using System.Collections.Generic;
namespace QuanLiGaraOto.DTOs
{
    
    
    public class SuppliesInputDTO
    {
    
        public int ID { get; set; }
        public Nullable<System.DateTime> DateInput { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
    
        public ICollection<SuppliesInputDetailDTO> SuppliesInputDetails { get; set; }
    }
}
