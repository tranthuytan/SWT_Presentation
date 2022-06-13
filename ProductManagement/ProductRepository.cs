using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement
{
    public class ProductRepository : IRepository<Product>
    {
        string cs;
        SqlConnection conn;
        public ProductRepository()
        {
            cs = @"Server=DESKTOP-HPMUS7S\SQLEXPRESS; Database=PRN_ProductDB; User Id=sa; Password=thuytan123";
        }
        public void Create(Product entity)
        {
            if (entity.Name.Trim().Equals("") || entity.Name.Length > 50)
                throw new ArgumentException("The name must have [1,50] characters");
            //if (entity.CreateDate > DateTime.UtcNow)
            //    throw new ArgumentException("The date must not exceed today");
            //if (entity.Price < 0)
            //    throw new ArgumentException("The price must larger than 0");
            //if (entity.CategoryId < 0)
            //    throw new ArgumentException("The category ID must larger than 0");
            try
            {
                conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("INSERT INTO Product(Name,CreateDate,Price,Status,CategoryId) VALUES (@Name,@CreateDate,@Price,1,@CategoryId)", conn);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@CreateDate", entity.CreateDate);
                cmd.Parameters.AddWithValue("@Price", entity.Price);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Status=0 WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("Delete successfully");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public List<Product> Get()
        {
            List<Product> ProList = new List<Product>();
            try
            {
                conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("SELECT Id,Name,CreadteDate,Price,Status,Category FROM Product WHERE Status=1", conn);
                conn.Open();
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Product Pd = new Product()
                    {
                        Id = (int)rd["Id"],
                        Name = (string)rd["Name"],
                        CreateDate = (DateTime)rd["CreateDate"],
                        Price = (double)rd["Price"],
                        Status = (int)rd["Status"],
                        CategoryId = (int)rd["CategoryId"]
                    };
                    //if (ProList.Count < n)
                    ProList.Add(Pd);
                    //else
                    //    break;
                }
                return ProList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("select Id,Name,CreateDate,Price,Status,CategoryId from Product WHERE Id=@id AND Status=1", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var rs = cmd.ExecuteReader();
                rs.Read();
                if (rs.HasRows)
                {
                    Product Pd = new Product()
                    {
                        Id = (int)rs["Id"],
                        Name = (string)rs["Name"],
                        CreateDate = (DateTime)rs["CreateDate"],
                        Price = (double)rs["Price"],
                        Status = (int)rs["Status"],
                        CategoryId = (int)rs["CategoryId"]
                    };
                    if (Pd != null)
                        return Pd;
                }
                conn.Close();
                return null;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Update(Product entity)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("UPDATE Product SET Name=@Name,CreateDate=@CreateDate,Price=@Price,Status=@Status,CategoryId=@CategoryId WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@CreateDate", entity.CreateDate);
                cmd.Parameters.AddWithValue("@Price", entity.Price);
                cmd.Parameters.AddWithValue("@Status", entity.Status);
                cmd.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("Update successfully");

            }
            catch (Exception)
            {

                throw;
            }
        }

        void IRepository<Product>.Create(Product entity)
        {
            throw new NotImplementedException();
        }

        private SqlConnection SqlConnection(string cs)
        {
            throw new NotImplementedException();
        }
    }
}
