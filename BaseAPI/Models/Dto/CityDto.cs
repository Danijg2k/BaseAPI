using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Models.Dto
{
    public class CityDto    // Same data as entity, without the secret (hide it to the client)
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public int Population { get; set; }  
        public bool IsCapital { get; set; }     
    }
}
