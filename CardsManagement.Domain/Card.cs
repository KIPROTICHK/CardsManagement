using System;
using System.Collections.Generic;
using System.Text;

namespace CardsManagement.Domain
{
    public class Card
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public virtual AppUser OwnerNavigation { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
