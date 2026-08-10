namespace Api.Enities
{
    public class Product(string name, string description, decimal price, string category)
    {
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string Category { get; private set; } = category;
        public decimal Price { get; private set; } = price;
        public bool IsActive { get; private set; } = true;
        public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        public void Update(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Category = product.Category;
            Price = product.Price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ToggleActive()
        {
            IsActive = !IsActive;
        }

    }
}
