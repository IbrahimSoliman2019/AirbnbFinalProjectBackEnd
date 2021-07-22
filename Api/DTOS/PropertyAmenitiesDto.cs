namespace Api.DTOS
{
    public class PropertyAmenitiesDto
    {
         
        public int amenity_id { get; set; }
        public int property_id { get; set; }
        public string icon_image { get; set; }
        
    
        public virtual AmenityDto amenity { get; set; }
        public virtual PropertyDTo property { get; set; }
    }
}