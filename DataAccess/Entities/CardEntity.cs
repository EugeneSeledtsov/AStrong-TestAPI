namespace DataAccess.Entities
{
    public class CardEntity: BaseEntity
    {
        public required string Description { get; set; }

        public FileEntity? Picture { get; set; }
    }
}
