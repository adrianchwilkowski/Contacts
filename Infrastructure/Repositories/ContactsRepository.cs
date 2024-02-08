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
         Task Add(Contact command);
         Task Update(Contact command);
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
            return await _context.Contacts.Where(x => x.Id == contactId).FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetList()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task Add(Contact command)
        {
            await _context.Contacts.AddAsync(command);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Contact command)
        {
            _context.Contacts.Update(command);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid contactId)
        {
            _context.Attach(contactId);
            _context.Remove(contactId);
            await _context.SaveChangesAsync();
        }
    }
}
