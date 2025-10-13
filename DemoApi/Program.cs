//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using DemoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Kết nối SQL Server (connection string lấy từ environment trong docker-compose)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Tự động migrate & seed dữ liệu khi chạy lần đầu
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Tự tạo database + bảng nếu chưa có
    SeedData.Initialize(db); // Thêm dữ liệu mẫu
}

// ✅ Chỉ bật Swagger khi ở môi trường Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Redirect "/" về Swagger (để khi bấm link http://localhost:5050/ tự mở Swagger)
app.MapGet("/", () => Results.Redirect("/swagger"));

// ✅ Lắng nghe trên cổng 5050 (đã expose trong Dockerfile và docker-compose.yml)
app.Urls.Add("http://*:5050");

app.MapControllers();

app.Run();
