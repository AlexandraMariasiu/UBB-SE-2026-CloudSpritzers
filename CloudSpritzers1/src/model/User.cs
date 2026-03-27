using CloudSpritzers.src.model.message;

namespace CloudSpritzers.src.model
{
    public class User : IResponder
    {
        private int _userId;
        private string _name;
        private string _email;

        public User(int userId, string name, string email)
        {
            _userId = userId;
            _name = name;
            _email = email;
        }

        public int UserId => _userId;

        public string GetName() => _name;
        public string GetEmail() => _email;
    }
}
