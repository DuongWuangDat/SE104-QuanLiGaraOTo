using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class InventoryReportDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public ICollection<InventoryReportDetailDTO> InventoryReportDetails { get; set; }
    }
}
