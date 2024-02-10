using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public class SeedContacts
    {
        static string samplesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFullPath(@"..\Infrastructure\SeedData\Contacts.json"));
        public static List<Contact> SeedData()
        {
            var json = JsonConvert.DeserializeObject(File.ReadAllText(samplesDir));
            var model = new List<string>()
            {
                "Name","Surname","Password","Email","PhoneNumber", "Category","SubCategory"
            };
            var data = JsonToStringConverter.ToJson(json, model);
            var contacts = new List<Contact>();
            ContactCommand toContactCommand;
            Contact contactToAdd;

            foreach (var contact in data)
            {
                toContactCommand = new ContactCommand()
                {
                    Name = contact[0],
                    Surname = contact[1],
                    Password = contact[2],
                    Email = contact[3],
                    PhoneNumber = contact[4],
                    Category = contact[5],
                    SubCategory = contact[6],
                };
                contactToAdd = toContactCommand.ParseToEntityModel();
                contacts.Add(contactToAdd);
            }
            return contacts;
        }
    }
}
