using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RST.Models.Tables;
using RST.Configurations;

namespace RST.Models.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(T model)
        {
            try
            {
                await _context.Set<T>().AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Update(T model)
        {
            //try
            //{
            await _context.SaveChangesAsync();
            return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        public async Task<bool> Delete(T model)
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<T> GetById(Guid id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Agent> GetAgentById(Guid id)
        {
            return await _context.agents.FindAsync(id);
        }
        public async Task<Agent> GetByPhoneNumber(string phone)
        {
            return await _context.agents.FirstOrDefaultAsync(temp => temp.PhoneNumber == phone);
        }
        //public interface IDeletableEntity
        //{
        //    bool IsDeleted { get; set; }
        //}

        //    public async Task<List<T>> GetAll()
        //    {
        //        try
        //        {

        //            var model = await _context.Set<T>()
        //.Where(temp => !(temp.GetType().GetInterfaces().Contains(typeof(IDeletableEntity)) && ((IDeletableEntity)temp).IsDeleted))
        //.ToListAsync();
        //            return model;

        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }

        public async Task<int> CustomerPhoneNumberExists(string phone, Guid id = default)
        {
            //This function below returns -1 if the username does not exist 
            //This function below returns 0 if the username exists to the user with the id value
            //This function below returns 1 if the username exists but not to the id value passed in the parameter
            var account = await _context.customers.FirstOrDefaultAsync(temp => temp.PhoneNumber == phone);
            if (account == null)
            {
                return -1;
            }

            if (id != default && account.Id == id)
            {
                return 0;
            }
            return 1;

        }
        public async Task<int> AgentPhoneNumberExists(string phone, Guid id = default)
        {
            //This function below returns -1 if the username does not exist 
            //This function below returns 0 if the username exists to the user with the id value
            //This function below returns 1 if the username exists but not to the id value passed in the parameter
            var creditor = await _context.agents.FirstOrDefaultAsync(temp => temp.PhoneNumber == phone);
            if (creditor == null)
            {
                return -1;
            }

            if (id != default && creditor.Id == id)
            {
                return 0;
            }
            return 1;
        }
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.customers.Where(temp => !temp.IsDeleted).ToListAsync();
        }
        public async Task<List<Agent>> GetAllAgents()
        {
            return await _context.agents.Where(temp => !temp.IsDeleted).ToListAsync();
        }
        public async Task<List<Apartment>> GetAllApartments()
        {
            return await _context.apartments.Where(temp => !temp.IsDeleted).ToListAsync();
        }
        public async Task<List<SoldApartments>> GetAllSoldApartments()
        {
            return await _context.soldApartments.Where(temp => !temp.IsDeleted).ToListAsync();
        }
        public async Task<bool> ExistsById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id) != null;
        }
        public async Task<List<String>> GetCustomerPhoneNumbers()
        {
            return await _context.customers.Where(temp => !temp.IsDeleted).Select(temp => temp.PhoneNumber).ToListAsync();
        }
    }
}
