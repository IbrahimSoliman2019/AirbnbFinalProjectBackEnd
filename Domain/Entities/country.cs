
    using System;
    using System.Collections.Generic;

namespace Domain.Entities
{
    
    public class country : BaseEntity
    {
        
       public int Id { get; set; }
    
        public string name { get; set; }
        public string code { get; set; }
        public Nullable<byte> status { get; set; }
    
        public virtual ICollection<property> properties { get; set; }
        public virtual ICollection<state> states { get; set; }
    }
}
