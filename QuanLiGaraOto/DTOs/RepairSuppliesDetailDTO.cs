
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{
    
    
    public class RepairSuppliesDetailDTO
    {
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> Price { get; set; }
        public SupplyDTO Supply { get; set; }
    }
}
