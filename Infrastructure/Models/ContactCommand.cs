using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ContactCommand
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string SubCategory { get; set; } = null!;
        public Contact ParseToEntityModel()
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Surname = Surname,
                Password = Password,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Category = Category,
                Subcategory = SubCategory
            };
        }
    }
}
