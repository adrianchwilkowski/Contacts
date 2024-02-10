using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.SeedData;
using Newtonsoft.Json;

namespace Contacts.Services
{
    public interface IContactService
    {
        Task<Contact> Get(Guid contactId);
        Task<List<Contact>> GetList();
        Task<Guid> Add(ContactCommand command);
        Task Update(Contact command);
        Task Delete(Guid contactId);
        Task SeedData();
    }
    public class ContactService : IContactService
    {
        private IContactsRepository _contactsRepository { get; set; }
        public ContactService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        public async Task<Guid> Add(ContactCommand command)
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

        public async Task<List<Contact>> GetList()
        {
            return await _contactsRepository.GetList();
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
    }
}
