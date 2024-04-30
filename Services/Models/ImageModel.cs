namespace Services.Models
{
    public class ImageModel
    {
        public required string FileName {  get; set; }

        public required Byte[] Content { get; set; }
    }
}
