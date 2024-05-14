using QuanLiGaraOto.ViewModel;
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{

    
    public class RepairDetailDTO: BaseViewModel
    {
    
        public int ID { get; set; }
        public string Content { get; set; }

        public Nullable<int> WageId { get; set; }
        public Nullable<decimal> WagePrice { get; set; }

        private Nullable<decimal> _price;

        public Nullable<decimal> Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }
        public string WageName { get; set; }
        public List<RepairSuppliesDetailDTO> RepairSuppliesDetails { get; set; }
    }
}
