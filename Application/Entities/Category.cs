using Datas.Enums;

namespace Datas.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public Status Status { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}
