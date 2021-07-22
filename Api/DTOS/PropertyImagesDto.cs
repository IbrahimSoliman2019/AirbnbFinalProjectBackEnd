namespace Api.DTOS
{
    public class PropertyImagesDto
    {
        
        public int id { get; set; }
        public string image { get; set; }
        
  
        public int property_id { get; set; }
     
        public virtual PropertyDTo property { get; set; }
        public virtual UserDto User { get; set; }
    }
}