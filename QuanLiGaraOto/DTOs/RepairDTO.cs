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
    
        public ReceptionDTO Reception { get; set; }
        public ICollection<RepairDetailDTO> RepairDetails { get; set; }
    }
}
