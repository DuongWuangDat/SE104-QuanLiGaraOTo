
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{
    
    
    public class RepairSuppliesDetailDTO
    {
        public int RepairDetailID { get; set; }
        public int SuppliesID { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual RepairDetailDTO RepairDetail { get; set; }
        public virtual SupplyDTO Supply { get; set; }
    }
}
