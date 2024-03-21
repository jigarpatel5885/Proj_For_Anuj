using Dapper;
using Proj_Anuj.Models;
using Proj_Anuj.Models.ViewModels;
using Proj_Anuj.Services.Interface;
using System.Data.SqlClient;

namespace Proj_Anuj.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private  string _connectionString { get; set; }
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }
        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            List<ProductVM> productList = new ();
            
            var sql = "select * from tblproduct";

            using (var connection = new SqlConnection(_connectionString))
            {
                var products = await connection.QueryAsync<Product>(sql);

                foreach (var prod in products)
                {

                    var query = @"
                                    SELECT * FROM tblcolors WHERE Product_Id = @Id;
                                    SELECT * FROM tblimages WHERE Product_Id = @Id;
                                    ";
                    var result = await connection.QueryMultipleAsync(query, new { Id = prod.Id });
                    var colors = await result.ReadAsync<Colors>();
                    var images = await result.ReadAsync<Image>();

                    productList.Add(new ProductVM()
                    {

                        Id = prod.Id,
                        Name = prod.Name,
                        Company = prod.Company,
                        Colors = colors.ToList(),
                        Description = prod.Description,
                        Category = prod.Category,
                        Feature = prod.Feature,
                        Shipping = prod.Shipping,
                        Stock = prod.Stock,
                        Review = prod.Review,
                        Stars = prod.Stars,
                        Image = images.ToList()

                    });
                    
                }

            }
            return productList;
        }

        public Task<IEnumerable<Colors>> GetColorsByProdudctId(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Image>> GetImageByProdudctId(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductVM> GetProduct(int Id)
        {
            ProductVM ?product;
            var sql = "select * from tblproduct where id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
               var prod = await connection.QueryFirstOrDefaultAsync<Product>(sql,new {Id=Id});
                if (prod != null)
                {
                    var query = @"
                                    SELECT * FROM tblcolors WHERE Product_Id = @Id;
                                    SELECT * FROM tblimages WHERE Product_Id = @Id;
                                    ";
                    var result = await connection.QueryMultipleAsync(query, new { Id = prod.Id });
                    var colors = await result.ReadAsync<Colors>();
                    var images = await result.ReadAsync<Image>();

                   product =  new ProductVM()
                    {

                        Id = prod.Id,
                        Name = prod.Name,
                        Company = prod.Company,
                        Colors = colors.ToList(),
                        Description = prod.Description,
                        Category = prod.Category,
                        Feature = prod.Feature,
                        Shipping = prod.Shipping,
                        Stock = prod.Stock,
                        Review = prod.Review,
                        Stars = prod.Stars,
                        Image = images.ToList()

                    };

                    return product;
                }
                   
            }
            
            return new ProductVM();
        }
    }
}
