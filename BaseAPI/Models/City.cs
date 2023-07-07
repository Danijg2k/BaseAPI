using System.ComponentModel.DataAnnotations;

namespace BaseAPI.Models
{
    public class City
    {
        // Id functions as the unique key in a relational database
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required, cannot be empty")]  // Requires a string value to be written (doesn't allow empty string)
        public string Name { get; set; }        // Name of the city
        public int Population { get; set; }     // Number of people living in the city
        public bool IsCapital { get; set; }     // If the city is capital of its country
        public string? citySecret { get; set; }  // Secret of the city (should be hidden to clients)
    }
}
