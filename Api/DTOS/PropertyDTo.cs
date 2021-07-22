using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.IdentityEntities;

namespace Api.DTOS
{
    public class PropertyDTo
    {
        
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string logitude { get; set; }
        public string bedroom_count { get; set; }
        public string bed_count { get; set; }
        public string bathroom_count { get; set; }
        public string accomodates_count { get; set; }
        public Nullable<byte> availability_tybe { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<byte> price_tybe { get; set; }
        public byte[] minimum_stay { get; set; }
        public Nullable<byte> minimum_stay_tybe { get; set; }
        public Nullable<byte> refund_tybe { get; set; }
        public  ICollection<BookingDTO> Bookings { get; set; }
        public  string CityName { get; set; }
        public  string countryName { get; set; }
        public  string currencyName { get; set; }
        public  string propertybeName { get; set; }
        public  string stateName { get; set; }
        public  UserDto User { get; set; }

        public  List<PropertyAmenitiesDto> property_amenities { get; set; }

        public  List<PropertyImagesDto> property_images { get; set; }

        public  List<PropertyReviewsDto> property_reviews { get; set; }

        public  List<TransactionDto> transactions { get; set; }
    }
}