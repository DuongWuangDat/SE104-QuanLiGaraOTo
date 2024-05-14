
using QuanLiGaraOto.ViewModel;
using System;
using System.Collections.Generic;

namespace QuanLiGaraOto.DTOs
{
    
    
    public class RepairSuppliesDetailDTO: BaseViewModel
    {
        private Nullable<int> _count;

        public Nullable<int> Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }
        public Nullable<decimal> PriceItem { get; set; }
        public SupplyDTO Supply { get; set; }
    }
}
