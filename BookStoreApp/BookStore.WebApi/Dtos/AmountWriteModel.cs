namespace BookStore.WebApi.Dtos
{
    public class AmountWriteModel
    {
        public Guid DetailId { get; set; }
        public int? NewAmount { get; set; }
    }
}
