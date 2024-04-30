namespace Requests.Card.Queries
{
    using MediatR;
    using Services.Models;

    public class GetCardsListRequest : IRequest<List<CardModel>>
    {
        public int Skip {  get; set; }

        public int Take {  get; set; }
    }
}
