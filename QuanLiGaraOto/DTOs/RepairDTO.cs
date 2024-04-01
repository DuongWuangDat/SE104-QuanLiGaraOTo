using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RepairDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> ReceptionID { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<System.DateTime> RepairDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ReceptionDTO Reception { get; set; }
        public virtual ICollection<RepairDetailDTO> RepairDetails { get; set; }
    }
}
