
using BLL;
using BLL.Services;
using DLA.Data;
using DLA.Data.Interface;
using DLA.Data.Repositories;
using DLA.Data.Specific_interfaces;
using Microsoft.EntityFrameworkCore;

namespace simple_book_lib_generic_repo_pattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConStr")));
            builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
            builder.Services.AddScoped(typeof(IBook), typeof(BookRepository));
            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped(typeof(IAuthor), typeof(AuthorRepository));
            builder.Services.AddScoped<AuthorService>();
            builder.Services.AddScoped(typeof(IBorrower), typeof(BorrowerRepository));
            builder.Services.AddScoped<BorrowerService>();
            builder.Services.AddAutoMapper(typeof(MyMappingProfile));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
