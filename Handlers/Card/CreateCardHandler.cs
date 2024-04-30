namespace Handlers.Card
{
    using DataAccess.Entities;
    using DataAccess.Interfaces;
    using Mapster;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Requests.Card.Commands;
    using Services.Interfaces;
    using Services.Models;
    using System.Transactions;

    internal class CreateCardHandler(IDataContext dataContext, IFileService fileService) : IRequestHandler<CreateCardCommand, CardModel>
    {
        readonly IDataContext dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        readonly IFileService fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

        public async Task<CardModel> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var newCard = request.Adapt<CardEntity>();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                dataContext.Create(newCard);
                if (request.FileContent != null)
                {
                    var newFile = new FileEntity() { FileName = request.FileContent.FileName, Id = Guid.NewGuid() };
                    dataContext.Create(newFile);

                    newCard.Picture = newFile;
                }

                await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                if (request.FileContent != null)
                {
                    fileService.UploadFile(request.FileContent.FileName, request.FileContent.OpenReadStream());
                }

                transactionScope.Complete();

            }
            await dataContext.SaveChangesAsync(cancellationToken);

            return await dataContext.Get<CardEntity>()
                                    .ProjectToType<CardModel>()
                                    .Where(t => t.Id == newCard.Id)
                                    .FirstAsync(cancellationToken);
        }
    }
}
