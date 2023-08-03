using System.ComponentModel.DataAnnotations;

namespace Medical.DAL.Entities
{
    public class Hospital
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
