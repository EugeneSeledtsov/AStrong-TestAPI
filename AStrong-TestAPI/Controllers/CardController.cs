namespace AStrong_TestAPI.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Requests.Card.Commands;
    using Requests.Card.Queries;
    using Requests.Image.Commands;
    using Requests.Image.Queries;
    using Services.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class CardController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        /// <summary>
        /// Get list of cards
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<CardModel>), 200)]
        [HttpGet]
        public async Task<IEnumerable<CardModel>> GetAsync([FromQuery] GetCardsListRequest request, CancellationToken token)
        {
            return await this.mediator.Send(request, token);
        }

        /// <summary>
        /// Get card by id
        /// </summary>
        [ProducesResponseType(typeof(CardModel), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CardModel>> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await this.mediator.Send(new GetCardByIdQuery() {  Id = id }, token);
        }

        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImageAsync(Guid id, CancellationToken token)
        {
            var imageModel =  await this.mediator.Send(new GetImageByCardIdQuery() { Id = id }, token);
            return File(imageModel.Content, "image/jpeg");
        }

        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(404)]
        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImageAsync(Guid id, IFormFile file,  CancellationToken token)
        {
            await this.mediator.Send(new UploadImageCommand() { CardId = id, FileContent = file }, token);
            return Created();
        }

        [ProducesResponseType(typeof(IActionResult), 201)]
        [HttpPost]
        public async Task<ActionResult<CardModel>> Post([FromForm] CreateCardCommand request, CancellationToken token)
        {
            var result = await this.mediator.Send(request, token);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
