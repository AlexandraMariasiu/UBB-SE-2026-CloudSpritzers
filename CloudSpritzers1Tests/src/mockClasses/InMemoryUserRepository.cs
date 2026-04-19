using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.repository.interfaces;

namespace CloudSpritzers1Tests.src.mockClasses
{
    public class InMemoryUserRepository : IUserRepository
    {
        private List<User> _users = new List<User>();

        public int Add(User userEntity)
        {
            if (userEntity == null) throw new ArgumentNullException();
            _users.Add(userEntity);
            return userEntity.GetId();
        }

        public void DeleteById(int id)
        {
            var user = _users.FirstOrDefault(u => u.GetId() == id);
            if (user == null) throw new KeyNotFoundException();
            _users.Remove(user);
        }

        public IEnumerable<User> GetAll() => _users;

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.GetId() == id);
            if (user == null) throw new KeyNotFoundException();
            return user;
        }

        public void UpdateById(int id, User userEntity)
        {
            var index = _users.FindIndex(u => u.GetId() == id);
            if (index == -1) throw new KeyNotFoundException();
            _users[index] = userEntity;
        }
    }
}