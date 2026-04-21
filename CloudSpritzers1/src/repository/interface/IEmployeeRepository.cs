using System.Collections.Generic;
using CloudSpritzers1.src.model.employee;

namespace CloudSpritzers1.src.repository
{
    public interface IEmployeeRepository
    {
        int CreateNewEntity(Employee employeeEntity);
        void DeleteById(int identificationNumber);
        IEnumerable<Employee> GetAll();
        Employee GetById(int identificationNumber);
        void UpdateById(int identificationNumber, Employee employeeEntity);
    }
}