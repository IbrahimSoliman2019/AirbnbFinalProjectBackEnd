namespace Api.DTOS
{
    public class PropertyReviewsDto
    {
         public int id { get; set; }
        public string comment { get; set; }
        public string rating { get; set; }
        public string image { get; set; }
    
        public virtual BookingDTO Booking { get; set; }
        public virtual PropertyDTo property { get; set; }
        public virtual UserDto User { get; set; }
    }
}