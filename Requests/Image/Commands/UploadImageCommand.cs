namespace Requests.Image.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Services.Models;

    public class UploadImageCommand : IRequest<bool>
    {
        public required Guid CardId { get; set; }

        public required IFormFile FileContent { get; set; }
    }
}
