using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class InventoryReportDetailDTO
    {
        public int SupplyID { get; set; }
        public Nullable<int> TonDau { get; set; }
        public Nullable<int> PhatSinh { get; set; }
        public Nullable<int> TonCuoi { get; set; }
        public string SupplyName { get; set; }
    }
}
