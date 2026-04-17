using CloudSpritzers1.src.model;
using CloudSpritzers1.src.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public int Add(User user)
        {
            return _userRepository.Add(user);
        }

        public void UpdateById(int id, User user)
        {
            _userRepository.UpdateById(id, user);
        }

        public void DeleteById(int id)
        {
            _userRepository.DeleteById(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public void CreateUser(int id, string name, string email)
        {
            User user = new User(id, name, email);  
            ValidateUser(user);
            Add(user);
        }

        public void ValidateUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            if (this.GetAll().Contains(user))
                throw new ArgumentException("User already exists");
            if (string.IsNullOrEmpty(user.GetFullName()))
                throw new ArgumentException("Name cannot be null or empty");
            if (string.IsNullOrEmpty(user.GetEmailAddress()))
                throw new ArgumentException("Email cannot be null or empty");
        }
    }
}
