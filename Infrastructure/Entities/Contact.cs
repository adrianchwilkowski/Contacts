using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? Subcategory { get; set; }

        //when returns empty string, password is complex, otherwise returns reasons why is not safe
        public List<string> ValidatePassword()
        {
            var response = new List<string>();
            if (string.IsNullOrEmpty(Password))
            {
                response.Add("cannot be empty");
                return response;
            }

            int minimumLength = 8; 

            bool hasUppercase = Password.Any(char.IsUpper);
            bool hasLowercase = Password.Any(char.IsLower);
            bool hasDigit = Password.Any(char.IsDigit);
            bool hasSpecial = Password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c));

            
            if (Password.Length<minimumLength) {
                response.Add("must have at least 8 characters\n");
                return response; }
            if (!hasUppercase) { response.Add("must contain at least one uppercase\n"); }
            if (!hasLowercase) { response.Add("must contain at least one lowercase\n"); }
            if (!hasDigit) { response.Add("must contain at least one digit\n"); }
            if (!hasSpecial) { response.Add("must contain at least one special character\n"); }
            return response;
        }

        //verifies if given category exists and subcategory is correct
        //sets subcategory as null if category is Private
        public bool ValidateCategories()
        {
            if (Enum.TryParse<CategoryEnum>(Category, out _))
            {
                if(Category == CategoryEnum.Private.ToString()) { 
                    Subcategory = null;
                    return true;
                }
                else if(Category == CategoryEnum.Business.ToString())
                {
                    if (!Enum.TryParse<SubcategoryEnum>(Subcategory, out _))
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
            else { return false; }
        }
    }

}
