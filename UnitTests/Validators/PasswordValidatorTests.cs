using Infrastructure.Models;

namespace UnitTests.Validators
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyPassword_WhenIsEmpty_ReturnsGivenMessage()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = "category",
                Subcategory = "category",
                Password = "",
            };
            var result = contact.VerifyPassword();
            Assert.True(result[0] == "cannot be empty");
            Assert.True(result.Count == 1);
        }
        [Test]
        public void VerifyPassword_WhenShorterThanMinLength_ReturnsGivenMessage()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = "category",
                Subcategory = "category",
                Password = "ambkdc",
            };
            var result = contact.VerifyPassword();
            Assert.True(result[0] == "must have at least 8 characters\n");
            Assert.True(result.Count == 1);
        }
        [Test]
        public void VerifyPassword_DoesNotContainUppercaseAndDigit_ReturnsGivenMessage()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = "category",
                Subcategory = "category",
                Password = "abcdo*#ml",
            };
            var result = contact.VerifyPassword();
            Assert.True(result[0] == "must contain at least one uppercase\n");
            Assert.True(result[1] == "must contain at least one digit\n");
            Assert.True(result.Count == 2);
        }
    }
}