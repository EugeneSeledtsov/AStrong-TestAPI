namespace Services.Models
{
    public  class CardModel
    {
        public Guid Id { get; set; }

        public required string Description { get; set; }

        public ImageModel? Picture { get; set; }
    }
}
