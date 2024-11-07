namespace Datas.Entities
{
    public class Comment : BaseEntity
    {
        public string? Description { get; set; }
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
