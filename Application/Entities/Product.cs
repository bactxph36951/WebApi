namespace Datas.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        
        public double? Price { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Comment>? Comments { get; set; }
    }
}
