using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{
    
    
    public class SupplyDTO
    {
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountInStock { get; set; }
        public Nullable<decimal> InputPrices { get; set; }
        public Nullable<decimal> OutputPrices { get; set; }
    
        public ICollection<InventoryReportDetailDTO> InventoryReportDetails { get; set; }

        public ICollection<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }

        public ICollection<SuppliesInputDetailDTO> SuppliesInputDetails { get; set; }
    }
}
