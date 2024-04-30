namespace Services.Interfaces
{
    public interface IFileService
    {
        void UploadFile(string relativePath, Stream stream);
        
        byte[] GetFile(string relativePath);
    }
}
