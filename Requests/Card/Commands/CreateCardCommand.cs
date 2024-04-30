namespace Requests.Card.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Services.Models;

    public class CreateCardCommand : IRequest<CardModel>
    {
        public required string Description { get; set; }

        public IFormFile? FileContent { get; set; }
    }
}
