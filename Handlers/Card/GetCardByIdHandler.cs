namespace Handlers.Card
{
    using DataAccess.Entities;
    using DataAccess.Interfaces;
    using Exceptions;
    using Mapster;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Requests.Card.Queries;
    using Services.Interfaces;
    using Services.Models;

    public class GetCardByIdHandler(IDataContext dataContext, IFileService fileService) : IRequestHandler<GetCardByIdQuery, CardModel>
    {
        readonly IDataContext dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        readonly IFileService fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

        public async Task<CardModel> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dataContext.Get<CardEntity>()
                .Include(x => x.Picture)
                .ProjectToType<CardModel>()
                .FirstAsync(t => t.Id == request.Id, cancellationToken) ?? throw new EntityNotFoundException();

            if (result.Picture != null)
            {
                result.Picture.Content = fileService.GetFile(result.Picture.FileName);
            }

            return result;
        }
    }
}
