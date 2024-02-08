using Infrastructure.Enums;
using Infrastructure.Models;

namespace UnitTests.Validators
{
    public class CategoryValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyCategories_WhenDoesNotExist_ReturnsFalse()
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
            var result = contact.ValidateCategories();
            Assert.True(!result);
        }
        [Test]
        public void VerifyCategories_WhenIsPrivate_SetSubcategoryToNull()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = CategoryEnum.Private.ToString(),
                Subcategory = "category",
                Password = "",
            };
            var result = contact.ValidateCategories();
            Assert.True(result);
            Assert.True(contact.Subcategory == null);
        }
        [Test]
        public void VerifyCategories_WhenIsBusinessAndSubcategoryIsNotInEnum_ReturnFalse()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = CategoryEnum.Business.ToString(),
                Subcategory = "category",
                Password = "",
            };
            var result = contact.ValidateCategories();
            Assert.True(!result);
        }
        [Test]
        public void VerifyCategories_WhenIsBusinessAndSubcategoryIsInEnum_ReturnTrue()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = CategoryEnum.Business.ToString(),
                Subcategory = SubcategoryEnum.Boss.ToString(),
                Password = "",
            };
            var result = contact.ValidateCategories();
            Assert.True(result);
        }
        [Test]
        public void VerifyCategories_WhenIsOther_ReturnTrue()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Surname = "user",
                Email = "user@contacts.com",
                PhoneNumber = "123456789",
                Category = CategoryEnum.Other.ToString(),
                Subcategory = "category",
                Password = "",
            };
            var result = contact.ValidateCategories();
            Assert.True(result);
        }
    }
}
