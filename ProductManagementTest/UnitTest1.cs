using NUnit.Framework;
using System;
using ConsoleApp6;
namespace TestProject5
{
    public class Tests
    {
       [Test]
        public void TestCreated( )
        {
            char[] invalidCharacters = "`~!@#$%^&*()_+=0123456789<>,.?/\\|{}[]'\"".ToCharArray();
            Boolean valid = true;        
            string s = "2022/01/11";
            
                DateTime dt = new DateTime();
                dt = DateTime.Parse(s);
            Product pro = new Product()
            {
                Name = "`~!@#$%^&*()_+=0123456789<>",
                    Price = 1000,
                    CreatedDate = dt,
                    Status = 1,
                    CategoryId = 0,

                };
            if (pro.Name.IndexOfAny(invalidCharacters) >= 0)
            {
                
             var argumentException  =    Assert.Throws<ArgumentException>(() => TestCreated());
                Assert.That(argumentException.Message, Is.EqualTo("Invalid characters"));
            }

        }
        }
    }
