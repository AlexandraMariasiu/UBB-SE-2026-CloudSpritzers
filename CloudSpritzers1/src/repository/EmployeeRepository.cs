using CloudSpritzers1.src.model.employee;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.repository
{
    public class EmployeeRepository : DBRepository<int, Employee>, IRepository<int, Employee>
    {
        public int Add(Employee elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Employee cannot be null.");

            string query = "INSERT INTO Employee " +
                "(name, email, group) " +
                "OUTPUT INSERTED.employee_id " +
                "VALUES (@name, @email, @group)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@name", elem.GetFullName());
            command.Parameters.AddWithValue("@email", elem.GetEmailAddress());
            command.Parameters.AddWithValue("@group", elem.GetDepartmentName());

            int id = base.Add(command, elem);
            return id;
        }

        public void DeleteById(int id)
        {
            string query = "DELETE FROM Employee WHERE employee_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            base.DeleteById(id, command);
        }

        public IEnumerable<Employee> GetAll()
        {
            string query = "SELECT * FROM Employee";
            SqlCommand command = new SqlCommand(query);
            return base.GetAll(command);
        }

        public Employee GetById(int id)
        {
            string query = "SELECT * FROM Employee WHERE employee_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            Employee employee = base.GetById(id, command);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with id {id} was not found.");

            return employee;
        }

        public void UpdateById(int id, Employee elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Employee cannot be null.");

            string query = "UPDATE Employee SET " +
                "name = @name, " +
                "email = @email " +
                "group = @group " +
                "WHERE employee_id = @id";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", elem.GetFullName());
            command.Parameters.AddWithValue("@email", elem.GetEmailAddress());
            command.Parameters.AddWithValue("@group", elem.GetDepartmentName());


            base.UpdateById(id, command, elem);
        }

        protected override int GetEntityId(Employee entity)
        {
            return entity.EmployeeId;
        }

        protected override Employee MapRowToEntity(SqlDataReader reader)
        {
            int employeeId = reader.GetInt32(reader.GetOrdinal("employee_id"));
            string name = reader.GetString(reader.GetOrdinal("name"));
            string email = reader.GetString(reader.GetOrdinal("email"));
            string groupString = reader.GetString(reader.GetOrdinal("group"));

            EmployeeDepartment groupEnum = (EmployeeDepartment)Enum.Parse(typeof(EmployeeDepartment), groupString);

            return new Employee(employeeId, name, email, groupEnum);
        }
    }
}
