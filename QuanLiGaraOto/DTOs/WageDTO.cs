
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{
    
    public class WageDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public virtual ICollection<RepairDetailDTO> RepairDetails { get; set; }
    }
}
