using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RepairDetailDTO
    {
    
        public int ID { get; set; }
        public string Content { get; set; }

        public Nullable<int> WageId { get; set; }
        public Nullable<decimal> WagePrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string WageName { get; set; }
        public List<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }
    }
}
