namespace Requests.Card.Queries
{
    using MediatR;
    using Services.Models;

    public class GetCardByIdQuery : IRequest<CardModel>
    {
        public Guid Id { get; set; }
    }
}
