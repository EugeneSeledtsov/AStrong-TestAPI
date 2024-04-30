namespace DataAccess.Entities
{
    public class FileEntity : BaseEntity
    {
        public required string FileName {  get; set; }

        public Guid CardId { get; set; }

        public virtual CardEntity? Card { get; set; }
    }
}
