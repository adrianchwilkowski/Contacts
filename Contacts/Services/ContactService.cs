using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Contacts.Services
{
    public interface IContactService
    {
        Task<Contact> Get(Guid contactId);
        Task<List<Contact>> GetList(Guid contactId);
        Task Add(ContactCommand command);
        Task Update(Contact command);
        Task Delete(Guid contactId);
    }
    public class ContactService : IContactService
    {
        private IContactsRepository _contactsRepository { get; set; }
        public ContactService(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        public async Task Add(ContactCommand command)
        {
            var contact = command.ParseToEntityModel();
            ValidateContact(contact);
            try
            {
                await _contactsRepository.Add(contact);
            }
            catch(Exception) { throw new BadHttpRequestException("Contact with given data cannot be added."); }
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

        public async Task<List<Contact>> GetList(Guid contactId)
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
    }
}
