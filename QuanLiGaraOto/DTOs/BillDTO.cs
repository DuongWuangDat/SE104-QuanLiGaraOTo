using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class BillDTO
    {
        public int ID { get; set; }
        public Nullable<int> ReceptionID { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> Proceeds { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ReceptionDTO Reception { get; set; }
    }
}
