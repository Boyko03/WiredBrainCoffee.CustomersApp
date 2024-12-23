using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data
{
    public interface ICustomerDataProvider
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
    }
    public class CustomerDataProvider : ICustomerDataProvider
    {
        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            await Task.Delay(100); // Simulate a bit of server work

            return new List<Customer>()
            {
                new Customer() {Id=0, FirstName="Julia", LastName="Developer", IsDeveloper=true},
                new Customer() {Id=1, FirstName="Alex", LastName="Rider"},
                new Customer() {Id=2, FirstName="Thomas Claudius", LastName="Huber", IsDeveloper=true},
                new Customer() {Id=3, FirstName="Anna", LastName="Rockstar"},
                new Customer() {Id=4, FirstName="Sara", LastName="Metroid"},
                new Customer() {Id=5, FirstName="Ben", LastName="Ronaldo"},
            };
        }
    }
}
