using ProductManagement;

namespace ProductManagementTest
{
    public class Tests
    {
        private Product product = null;
        private ProductRepository productRepo = null;
        [SetUp]
        public void Setup()
        {
            productRepo = new ProductRepository();
        }
        [Test]
        public void GetByIdTest()
        {
            //Assign
            product.Id = 12;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            var dbProduct = productRepo.GetById(product.Id);

            //Expectation: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
        [Test]
        public void GetByIdWithIdOutOfRange()
        {
            //Assign
            product.Id = 120;
            product.Name = "red bull";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 12000;
            product.Status = 1;
            product.CategoryId = 1;

            Assert.AreEqual(null,productRepo.GetById(product.Id));

        }
        [Test]
        public void CreateProductTest()
        {
            //Assign
            //database id: int identity
            product.Id = 14;
            product.Name = "revive lemon salt";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;
            //Act
            int id = product.Id;
            var dbProduct = productRepo.GetById(id);
            //Expectation 1: product is not null
            Assert.IsNotNull(dbProduct);
            //Expectation 2: dbProduct == product
            Assert.AreEqual(product.Id, dbProduct.Id);
            Assert.AreEqual(product.Name, dbProduct.Name);
            Assert.AreEqual(product.CreateDate, dbProduct.CreateDate);
            Assert.AreEqual(product.Price, dbProduct.Price);
            Assert.AreEqual(product.Status, dbProduct.Status);
            Assert.AreEqual(product.CategoryId, dbProduct.CategoryId);
        }
        [Test]
        public void CreateProductTest_NameException()
        {
            //Assign
            //database id: int identity
            product = new Product();
            product.Id = 14;
            product.Name = "revive lemon saltanksdlaskndlaksdnasldkansdlasndaksdnlasd asd as da";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That(ex.Message, Is.EqualTo("The name must have [1,50] characters"));
        }
        [Test]
        public void CreateProductTest_NameRegexException()
        {
            //Assign
            //database id: int identity
            product = new Product();
            product.Id = 14;
            product.Name = "01823hsevenup,.;324-";
            product.CreateDate = new DateTime(2022, 6, 13);
            product.Price = 15000;
            product.Status = 1;
            product.CategoryId = 1;

            var ex = Assert.Throws<ArgumentException>(() => productRepo.Create(product));
            Assert.That(ex.Message, Is.EqualTo("The name must not have special characters"));
        }
    }
}
