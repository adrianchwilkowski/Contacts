using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IContactsRepository
    {
         Task<Contact> Get(Guid contactId);
         Task<List<Contact>> GetList();
         Task Add(Contact contact);
         Task AddRange(List<Contact> contacts);
         Task Update(Contact contact);
         Task Delete(Guid contactId);
    }
        public class ContactsRepository : IContactsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public ContactsRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Contact> Get(Guid contactId)
        {
            var result = await _context.Contacts.Where(x => x.Id == contactId).FirstOrDefaultAsync();
            if (result == null) { throw new Exception(); };
            return result;
        }

        public async Task<List<Contact>> GetList()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task Add(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid contactId)
        {
            var entityToDelete = await _context.Contacts.FindAsync(contactId);

            if (entityToDelete != null)
            {
                _context.Remove(entityToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task AddRange(List<Contact> contacts)
        {
                //foreach (Contact contact in contacts)
                //{
                //    await _context.Contacts.AddAsync(contact);
                //}
            
            await _context.AddRangeAsync(contacts);
            _context.SaveChanges();
        }
    }
}
