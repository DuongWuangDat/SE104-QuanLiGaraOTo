using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RevenueDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public ICollection<RevenueDetailDTO> RevenueDetails { get; set; }
    }
}
