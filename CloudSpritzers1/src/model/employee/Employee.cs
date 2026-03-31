using CloudSpritzers1.src.model.message;


namespace CloudSpritzers1.src.model.employee
{
    public class Employee : ISender
    {
        private int _employeeId;
        private string _name;
        private string _email;
        private GroupEnum _group;

        public Employee(int employeeId, string name, string email, GroupEnum groupEnum)
        {
            _employeeId = employeeId;
            _name = name;
            _email = email;
            _group = groupEnum;
        }

        public int EmployeeId => _employeeId;
        public string GetGroup() => _group.ToString();

        public string GetName() => _name;
        public string GetEmail() => _email;

        public int GetId() => _employeeId;
    }
}
