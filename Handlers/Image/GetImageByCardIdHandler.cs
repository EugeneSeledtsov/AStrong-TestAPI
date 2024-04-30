namespace Handlers.Image
{
    using DataAccess.Entities;
    using DataAccess.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Requests.Image.Queries;
    using Services.Interfaces;
    using Services.Models;

    public class GetImageByCardIdHandler(IDataContext dataContext, IFileService fileService)
        : IRequestHandler<GetImageByCardIdQuery, ImageModel>
    {
        readonly IDataContext dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

        readonly IFileService fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));

        public async Task<ImageModel> Handle(GetImageByCardIdQuery request, CancellationToken cancellationToken)
        {
            var fileName = await this.dataContext.Get<CardEntity>()
                .Where(t => t.Id == request.Id && t.Picture != null)
                .Select(t => t.Picture.FileName)
                .FirstAsync(cancellationToken);
            return new ImageModel() { Content = this.fileService.GetFile(fileName), FileName = fileName };
        }
    }
}