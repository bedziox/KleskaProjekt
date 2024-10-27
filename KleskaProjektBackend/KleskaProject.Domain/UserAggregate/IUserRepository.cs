﻿using KleskaProject.Domain.Common.Shared;

namespace KleskaProject.Domain.UserAggregate;

public interface IUserRepository
{
    User GetById(Guid id);
    Task<User> GetByEmail(string email);
    Result DeleteById(Guid id);
    IEnumerable<User> GetAll();
    Task<IEnumerable<User>> GetAllAsync();
    void Add(User user);
    bool IsEmailTaken(string email);
}