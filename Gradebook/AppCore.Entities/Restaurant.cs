using System.ComponentModel.DataAnnotations;

namespace AppCore.Entities
{

    public partial class Restaurant : EntityBase
    {

        public int ID { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        public int AddressId { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        public CuisineType Cuisine { get; set; }

        public Restaurant()
        {
            this.Address = new Address();
        }

        public override bool Validate()
        {
            return true;
        }
    }


}
