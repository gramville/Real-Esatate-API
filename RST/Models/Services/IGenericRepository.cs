using RST.Models.Tables;

namespace RST.Models.Services
{
    public interface IGenericRepository<T>
    {
        Task<bool> Add(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
        Task<T> GetById(Guid id);
        Task<Agent> GetByPhoneNumber(string phone);
        Task<Agent> GetAgentById(Guid id);
        //Task<List<T>> GetAll();
        Task<bool> ExistsById(Guid id);
        Task<List<Customer>> GetAllCustomers();
        Task<List<Agent>> GetAllAgents();
        Task<List<Apartment>> GetAllApartments();
        Task<List<SoldApartments>> GetAllSoldApartments();

        //The function below returns -1 if the username does not exist 
        //The function below returns 0 if the username exists to the user with the id value
        //The function below returns 1 if the username exists but not to the id value passed in the parameter
        Task<int> CustomerPhoneNumberExists(string phone, Guid id = default);
        //The function below returns -1 if the username does not exist 
        //The function below returns 0 if the username exists to the user with the id value
        //The function below returns 1 if the username exists but not to the id value passed in the parameter
        Task<int> AgentPhoneNumberExists(string phone, Guid id = default);
        //The function below returns -1 if the username does not exist 
        //The function below returns 0 if the username exists to the user with the id value
        //The function below returns 1 if the username exists but not to the id value passed in the parameter

    }
}

