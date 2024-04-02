using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RepairDetailDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> Content { get; set; }
        public Nullable<decimal> Price { get; set; }
        public WageDTO Wage { get; set; }
        public List<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }
    }
}
