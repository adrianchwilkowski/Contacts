using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
