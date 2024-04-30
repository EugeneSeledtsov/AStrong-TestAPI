namespace Handlers.Image
{
    using DataAccess.Entities;
    using DataAccess.Interfaces;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Requests.Image.Commands;
    using Services.Interfaces;
    using System.Transactions;

    public class UploadImageHandler(IDataContext dataContext, IFileService fileService) : IRequestHandler<UploadImageCommand, bool>
    {
        readonly IDataContext dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        readonly IFileService fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

        public async Task<bool> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var cardEntity = await this.dataContext.Get<CardEntity>()
                .Where(t => t.Id == request.CardId)
                .Include(t => t.Picture)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException();

            if (cardEntity.Picture != null)
            {
                throw new EntityAlreadyExistsException();
            }

            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var newFile = new FileEntity() { FileName = request.FileContent.FileName, Id = Guid.NewGuid() };
            dataContext.Create(newFile);
            cardEntity.Picture = newFile;
            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            fileService.UploadFile(request.FileContent.FileName, request.FileContent.OpenReadStream());

            transactionScope.Complete();


            return true;
        }
    }
}
