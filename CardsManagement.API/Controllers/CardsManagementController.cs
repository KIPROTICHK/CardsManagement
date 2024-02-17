using CardsManagement.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using static CardsManagement.Application.SharedDefinedValues;

namespace CardsManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag(description: "Cards Management Endpoint")]
    [Authorize]
    public class CardsManagementController : ControllerBase
    {
        private readonly ICardsManagementService _cardsManagementService;
        private readonly GeneralViewModel res;
        private readonly CardsViewModel vCards;
        private readonly IDataRepository _dataRepository;

        public CardsManagementController(ICardsManagementService cardsManagementService
            , IDataRepository dataRepository)
        {
            _cardsManagementService = cardsManagementService;
            res = new GeneralViewModel();
            vCards = new CardsViewModel();
            _dataRepository = dataRepository;
        }


        [HttpGet(nameof(GetCards))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        public async Task<ActionResult> GetCards([FromQuery] FilterCardsRequest request)
        {
            #region Validation
            if (request == null)
            {
                res.Message = "Parameters are required";
                return BadRequest(res);
            }

            if (!(ModelState.IsValid))
            {
                var modelError = ModelState.Where(x => x.Value.Errors.Count > 0)
                                               .Select(x => new
                                               {
                                                   x.Key,
                                                   x.Value.Errors[0].ErrorMessage

                                               }).FirstOrDefault();

                res.Message = modelError.ErrorMessage;
                return BadRequest(res);

            }

            #endregion

            Guid? Owner = null;
            if (HttpContext.User.Claims.Any(x => x.Value.Contains(UserType.Member)))
            {
                var id = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                bool success = Guid.TryParse(id, out Guid Owner1);
                if (success)
                    Owner = Owner1;
            }

            var data = await _cardsManagementService.GetCards(request, Owner);

            vCards.MetaData = data.MetaData;
            vCards.Cards = new();
            foreach (var d in data)
            {
                var i = d.CastMyObject<CardResponse>();
                i.OwnerEmail = d.OwnerNavigation?.Email;
                vCards.Cards.Add(i);
            }


            return Ok(vCards);
        }

        [HttpGet($"{nameof(GetCards)}/{{Id}}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        public async Task<ActionResult> GetCards(Guid? Id)
        {

            var data = await _dataRepository.GetCards()
                .Include(x => x.OwnerNavigation)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (data == null)
            {
                res.Message = "Card does not exists";
                return BadRequest(res);
            }

            var i = data.CastMyObject<CardResponse>();
            i.OwnerEmail = data.OwnerNavigation?.Email;
            vCards.Cards.Add(i); 

            return Ok(vCards);
        }

        [HttpPost(nameof(AddCard))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        public async Task<ActionResult> AddCard([FromBody] AddCardRequest request)
        {
            #region Validation
            if (request == null)
            {
                res.Message = "Parameters are required";
                return BadRequest(res);
            }

            if (!(ModelState.IsValid))
            {
                var modelError = ModelState.Where(x => x.Value.Errors.Count > 0)
                                               .Select(x => new
                                               {
                                                   x.Key,
                                                   x.Value.Errors[0].ErrorMessage

                                               }).FirstOrDefault();

                res.Message = modelError.ErrorMessage;
                return BadRequest(res);

            }
            #endregion

            var id = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            bool success = Guid.TryParse(id, out Guid Owner);
            

            var response = await _cardsManagementService.AddCard(request, Owner);
            res.Message = response.Message;

            if (response.Success)
            {
                vCards.Card = new();

                CardResponse i = response.Data.CastMyObject<CardResponse>();

                vCards.Card = i;

                vCards.Message = response.Message;

                return Ok(vCards);
            }
            return BadRequest(res);

        }

        [HttpPut($"{nameof(UpdateCard)}/{{Id}}")] 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardsViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        public async Task<ActionResult> UpdateCard([FromBody] EditCardRequest request,Guid? Id)
        {
            #region Validation
            if (request == null)
            {
                res.Message = "Parameters are required";
                return BadRequest(res);
            }

            if (!(ModelState.IsValid))
            {
                var modelError = ModelState.Where(x => x.Value.Errors.Count > 0)
                                               .Select(x => new
                                               {
                                                   x.Key,
                                                   x.Value.Errors[0].ErrorMessage

                                               }).FirstOrDefault();

                res.Message = modelError.ErrorMessage;
                return BadRequest(res);

            }

            #endregion 

            var response = await _cardsManagementService.UpdateCard(request,Id);
            res.Message = response.Message;
            if (response.Success)
            {
                vCards.Card = new();

                CardResponse i = response.Data.CastMyObject<CardResponse>();

                vCards.Card = i;

                vCards.Message = response.Message;
                return Ok(vCards);
            }
            return BadRequest(res);

        }


        [HttpDelete($"{nameof(RemoveCard)}/{{Id}}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralViewModel))]
        public async Task<ActionResult> RemoveCard([FromBody] EditCardRequest request, Guid? Id)
        {
            #region Validation
            if (request == null)
            {
                res.Message = "Parameters are required";
                return BadRequest(res);
            }

            if (!(ModelState.IsValid))
            {
                var modelError = ModelState.Where(x => x.Value.Errors.Count > 0)
                                               .Select(x => new
                                               {
                                                   x.Key,
                                                   x.Value.Errors[0].ErrorMessage

                                               }).FirstOrDefault();

                res.Message = modelError.ErrorMessage;
                return BadRequest(res);

            }

            #endregion 

            var response = await _cardsManagementService.RemoveCard(Id);
            res.Message = response.Message;
            if (response.Success)
            {
                
                return Ok(res);
            }
            return BadRequest(res);

        }
    }
}
