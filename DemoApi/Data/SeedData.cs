using DemoApi.Models;

namespace DemoApi.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Products.Any()) return;

            context.Products.AddRange(
                new Product { Name = "Nhẫn Vàng PNJ", Price = 1200000 },
                new Product { Name = "Dây Chuyền Bạc PNJ", Price = 850000 },
                new Product { Name = "Vòng Tay Ngọc Trai", Price = 1450000 }
            );

            context.SaveChanges();
        }
    }
}
