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
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual RepairDTO Repair { get; set; }
        public virtual WageDTO Wage { get; set; }
        public virtual ICollection<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }
    }
}
