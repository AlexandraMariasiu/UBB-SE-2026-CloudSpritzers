using CloudSpritzers1.src.model.employee;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.service
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepo)
        {
            _employeeRepository = employeeRepo;
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public int Add(Employee employee)
        {
            return _employeeRepository.Add(employee);
        }

        public void UpdateById(int id, Employee employee)
        {
            _employeeRepository.UpdateById(id, employee);
        }

        public void DeleteById(int id)
        {
            _employeeRepository.DeleteById(id);
        }

        public List<Employee> GetAll()
        {
            return _employeeRepository.GetAll().ToList();
        }

        public void CreateEmployee(int id, string name, string email, string group)
        {
            GroupEnum groupEnum = (GroupEnum)Enum.Parse(typeof(GroupEnum), group);
            Employee employee = new Employee(id, name, email, groupEnum);
            ValidateEmployee(employee);
            Add(employee);
        }

        public void ValidateEmployee(Employee employee)
        {
            ArgumentNullException.ThrowIfNull(employee);
            if (this.GetAll().Contains(employee))
                throw new ArgumentException("Employee already exists");
            if (string.IsNullOrEmpty(employee.GetName()))
                throw new ArgumentException("Name cannot be null or empty");
            if (string.IsNullOrEmpty(employee.GetEmail()))
                throw new ArgumentException("Email cannot be null or empty");
            if(string.IsNullOrEmpty(employee.GetGroup()))
                throw new ArgumentException("Group cannot be null or empty");
            if (!Enum.IsDefined(typeof(GroupEnum), employee.GetGroup()))
                throw new ArgumentException("Invalid group");
        }
    }
}
