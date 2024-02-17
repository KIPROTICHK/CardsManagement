using CardsManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsManagement.Application.ViewModels
{
    public class CardsViewModel : MetaDataViewModel
    {
        public string Message { get; set; }
        public CardResponse Card { get; set; }
        public List<CardResponse> Cards { get; set; }
    }

    public class  CardResponse : EditCardRequest
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string OwnerEmail { get; set; }
        public DateTime DateCreated { get; set; }
    }
 
    public class EditCardRequest : AddCardRequest
    {
        [Required]
        public string Status { get; set; }

    }
    public class AddCardRequest
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$",ErrorMessage ="color code must start with HASH character(#) e.g. #000000")] 
        public string Color { get; set; }
            
       
    }
    public class FilterCardsRequest : CustomParameters
    {
        public string Name { get; set; }

        [Range(0,7)]
        public string Color { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
