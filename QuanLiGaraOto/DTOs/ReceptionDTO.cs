using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class ReceptionDTO
    {
    
        public int ID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string LicensePlate { get; set; }
        public Nullable<int> BrandID { get; set; }
        public Nullable<decimal> Debt { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public virtual ICollection<BillDTO> Bills { get; set; }
        public virtual BrandCarDTO BrandCar { get; set; }
        public virtual CustomerDTO Customer { get; set; }
        public virtual ICollection<RepairDTO> Repairs { get; set; }
    }
}
