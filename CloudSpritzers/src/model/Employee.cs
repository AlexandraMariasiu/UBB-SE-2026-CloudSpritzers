namespace CloudSpritzers.src.model
{
    public class Employee : IResponder
    {
        private int _employeeId;
        private string _name;
        private string _email;
        private int _groupId;

        public Employee(int employeeId, string name, string email, int groupId)
        {
            _employeeId = employeeId;
            _name = name;
            _email = email;
            _groupId = groupId;
        }

        public int EmployeeId => _employeeId;
        public int GroupId => _groupId;

        public string GetName() => _name;
        public string GetEmail() => _email;
    }
}
