namespace Services.Implementations
{
    using Services.Interfaces;

    public class FileService(string basePath) : IFileService
    {
        private readonly string basePath = basePath;

        public byte[] GetFile(string relativePath)
        {
            var filePath = Path.Combine(basePath, relativePath);
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            return File.ReadAllBytes(filePath);
        }

        public void UploadFile(string relativePath, Stream fileContent)
        {
            using var fileStream = File.Create(Path.Combine(basePath, relativePath));
            fileContent.Seek(0, SeekOrigin.Begin);
            fileContent.CopyTo(fileStream);
        }
    }
}
