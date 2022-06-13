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
            product = new Product();
            productRepo = new ProductRepository();
        }

        [Test]
        public void Test1()
        {
            //Assign

            //Act

            //Assert

            Assert.Pass();
        }
    }
}