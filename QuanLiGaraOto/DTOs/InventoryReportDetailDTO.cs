using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class InventoryReportDetailDTO
    {
        public int InventoryReportID { get; set; }
        public int SuppliesID { get; set; }
        public Nullable<int> TonDau { get; set; }
        public Nullable<int> PhatSinh { get; set; }
        public Nullable<int> TonCuoi { get; set; }
    
        public InventoryReportDTO InventoryReport { get; set; }
        public SupplyDTO Supply { get; set; }
    }
}
