using System.ComponentModel.DataAnnotations;

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
        
        [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage ="Hex color code must start with HASH character(#) e.g. #000000")] 
        public string Color { get; set; }
            
       
    }
    public class FilterCardsRequest : CustomParameters
    {
        public string Name { get; set; }

        public string Color { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
