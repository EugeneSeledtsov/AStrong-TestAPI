namespace Handlers.Card
{
    using DataAccess.Entities;
    using DataAccess.Interfaces;
    using Mapster;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Requests.Card.Queries;
    using Services.Models;

    public class GetCardsListHandler(IDataContext dataContext) : IRequestHandler<GetCardsListRequest, List<CardModel>>
    {
        readonly IDataContext dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        public Task<List<CardModel>> Handle(GetCardsListRequest request, CancellationToken cancellationToken)
        {
            return dataContext.Get<CardEntity>()
                                .Skip(request.Skip)
                                .Take(request.Take)
                                .ProjectToType<CardModel>()
                                .ToListAsync(cancellationToken);
        }
    }
}
