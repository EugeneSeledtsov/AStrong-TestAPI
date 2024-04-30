namespace Requests.Image.Queries
{
    using MediatR;
    using Services.Models;

    public class GetImageByCardIdQuery : IRequest<ImageModel>
    {
        public Guid Id { get; set; }
    }
}
