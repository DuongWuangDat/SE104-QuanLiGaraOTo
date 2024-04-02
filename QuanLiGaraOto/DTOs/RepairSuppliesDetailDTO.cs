
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
    
        public RepairDetailDTO RepairDetail { get; set; }
        public SupplyDTO Supply { get; set; }
    }
}
