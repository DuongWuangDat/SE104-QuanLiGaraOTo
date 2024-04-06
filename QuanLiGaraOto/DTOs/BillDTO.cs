using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class BillDTO
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> Proceeds { get; set; }
    
        public ReceptionDTO Reception { get; set; }
    }
}
