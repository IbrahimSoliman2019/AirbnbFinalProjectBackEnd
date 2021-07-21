using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Api.DTOS
{
    public class BookingDTO
    {
          
        public int Id { get; set; }

        
        public DateTime check_in_date { get; set; }
        public DateTime check_out_date { get; set; }
        public decimal price_per_day { get; set; }
        public decimal price_per_stay { get; set; }
        public decimal tax_paid { get; set; }
        public decimal site_fees { get; set; }
        public decimal amount_paid { get; set; }
        public string is_refund { get; set; }
        public DateTime cancel_date { get; set; }
        public decimal refund_paid { get; set; }
        public Nullable<int> transaction_id_fk { get; set; }
        public decimal effective_amount { get; set; }
        public Nullable<System.DateTime> booking_date { get; set; }
        
    
      


    }
}