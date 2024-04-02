using QuanLiGaraOto.Model;
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class ReceptionDTO
    {
    
        public int ID { get; set; }
        public string LicensePlate { get; set; }
        public Nullable<decimal> Debt { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public BrandCar BrandCar { get; set; }
        public Customer Customer { get; set; }
    }
}
