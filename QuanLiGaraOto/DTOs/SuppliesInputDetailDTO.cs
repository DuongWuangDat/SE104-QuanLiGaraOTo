using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class SuppliesInputDetailDTO
    {
        public int InputID { get; set; }
        public int SuppliesID { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> PriceItem { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual SupplyDTO Supply { get; set; }
        public virtual SuppliesInputDTO SuppliesInput { get; set; }
    }
}
