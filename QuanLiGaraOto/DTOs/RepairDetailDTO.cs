using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RepairDetailDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> RepairID { get; set; }
        public Nullable<int> WageID { get; set; }
        public Nullable<int> Content { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        public RepairDTO Repair { get; set; }
        public WageDTO Wage { get; set; }
        public ICollection<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }
    }
}
