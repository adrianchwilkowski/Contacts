using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.SeedData;
using Newtonsoft.Json;

namespace Contacts.Services
{
    public interface IContactService
    {
        Task<Contact> Get(Guid contactId);
        Task<List<ListContactContract>> GetList();
        Task<Guid> Add(AddContactCommand command);
        Task Update(Contact command);
        Task Delete(Guid contactId);
        Task SeedData();
        Task SeedEnums();
    }
    public class ContactService : IContactService
    {
        private IContactsRepository _contactsRepository { get; set; }
        private ICategoriesRepository _categoriesRepository { get; set; }

        public ContactService(IContactsRepository contactsRepository, ICategoriesRepository categoriesRepository)
        {
            _contactsRepository = contactsRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<Guid> Add(AddContactCommand command)
        {
            var contact = command.ParseToEntityModel();
            ValidateContact(contact);
            try
            {
                await _contactsRepository.Add(contact);
            }
            catch(Exception) { throw new BadHttpRequestException("Contact with given data cannot be added."); }
            return contact.Id;
        }

        public async Task Delete(Guid contactId)
        {
            try
            {
                await _contactsRepository.Delete(contactId);
            }
            catch (Exception) { throw new BadHttpRequestException("Contact with given ID doesn't exist."); }
        }

        public async Task<Contact> Get(Guid contactId)
        {
            try
            {
                return await _contactsRepository.Get(contactId);
            }
            catch (Exception) { throw new BadHttpRequestException("Contact with given ID doesn't exist."); }
        }

        public async Task<List<ListContactContract>> GetList()
        {
            var response = await _contactsRepository.GetList();
            List<ListContactContract> result = new List<ListContactContract>();
            foreach (var contact in response)
            {
                result.Add(ListContactContract.FromContact(contact));
            }
            return result;
        }

        public async Task Update(Contact contact)
        {
            ValidateContact(contact);
            await _contactsRepository.Update(contact);
        }
        private void ValidateContact(Contact contact)
        {
            var validatePassword = contact.ValidatePassword();
            if (validatePassword.Any())
            {
                string fullMessage = "Password: ";
                foreach(var message in validatePassword)
                {
                    fullMessage += message;
                }
                fullMessage += ".";
                throw new BadHttpRequestException(fullMessage);
            }
            if (!contact.ValidateCategories())
            {
                throw new BadHttpRequestException("Wrong combination of categories.");
            }
        }

        public async Task SeedData()
        {
            var data = SeedContacts.SeedData();
            try
            {
                await _contactsRepository.AddRange(data);
            }
            catch (Exception) { throw new Exception("error"); }
        }
        public async Task SeedEnums()
        {
            var categories = new List<Category>() {
                new Category(){ Id = Guid.NewGuid(), Name = CategoryEnum.Private.ToString()},
                new Category(){ Id = Guid.NewGuid(), Name = CategoryEnum.Business.ToString()},
                new Category(){ Id = Guid.NewGuid(), Name = CategoryEnum.Other.ToString()},
            };
            var subcategories = new List<Subcategory>() {
                new Subcategory(){ Id = Guid.NewGuid(), Name = SubcategoryEnum.Boss.ToString()},
                new Subcategory(){ Id = Guid.NewGuid(), Name = SubcategoryEnum.Client.ToString()},
                new Subcategory(){ Id = Guid.NewGuid(), Name = SubcategoryEnum.Employee.ToString()},
                new Subcategory(){ Id = Guid.NewGuid(), Name = SubcategoryEnum.Manager.ToString()},
            };
            foreach (var category in categories)
            {
                await _categoriesRepository.AddCategory(category);
            }
            foreach (var subcategory in subcategories)
            {
                await _categoriesRepository.AddSubcategory(subcategory);
            }
        }
    }
}
