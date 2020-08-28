using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiApp.Entities;
using WebApiApp.Models;

namespace WebApiApp.Logic
{
    public class UserService
    {
        private readonly WebApiAppEntities dbContext = new WebApiAppEntities();

        public User CreateUser(UserModel model)
        {
            try
            {
                if (model.date_of_birth == null)
                    throw new ArgumentException("Date of birth is required");

                User newUser = new User
                {
                    firstname = model.firstname,
                    lastname = model.lastname,
                    gender = model.gender,
                    date_of_birth = model.date_of_birth,
                    date_created = DateTime.Now,
                    date_updated = DateTime.Now
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                return newUser;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<User> SelectAllUsers(string sort_field, string sort_order_mode, string filter_field, string filter_value, int? page, int? page_size)
        {
            try
            {
                page = page.HasValue ? Convert.ToInt32(page) : 1;
                page_size = page_size.HasValue ? Convert.ToInt32(page_size) : 25;

                IEnumerable<User> users = dbContext.Users.ToList();

                if (!string.IsNullOrEmpty(filter_field) && !string.IsNullOrEmpty(filter_value))
                {
                    switch (filter_field.ToLower())
                    {
                        case "id":
                            users = users.Where(x => x.id == int.Parse(filter_value));
                            break;
                        case "firstname":
                            users = users.Where(x => x.firstname == filter_value);
                            break;
                        case "lastname":
                            users = users.Where(x => x.lastname == filter_value);
                            break;
                        case "gender":
                            users = users.Where(x => x.gender == filter_value);
                            break;
                        case "date_of_birth":
                            users = users.Where(x => x.date_of_birth == DateTime.Parse(filter_value));
                            break;
                        case "date_created":
                            users = users.Where(x => x.date_created == DateTime.Parse(filter_value));
                            break;
                        default:
                            users = users.Where(x => x.date_updated == DateTime.Parse(filter_value));
                            break;
                    }
                }                    

                if(!string.IsNullOrEmpty(sort_field) && (sort_order_mode == "asc" || string.IsNullOrEmpty(sort_order_mode)))
                {
                    switch (sort_field.ToLower())
                    {
                        case "id":
                            users = users.OrderBy(x => x.id);
                            break;
                        case "firstname":
                            users = users.OrderBy(x => x.firstname);
                            break;
                        case "lastname":
                            users = users.OrderBy(x => x.lastname);
                            break;
                        case "gender":
                            users = users.OrderBy(x => x.gender);
                            break;
                        case "date_of_birth":
                            users = users.OrderBy(x => x.date_of_birth);
                            break;
                        case "date_created":
                            users = users.OrderBy(x => x.date_created);
                            break;
                        default:
                            users = users.OrderBy(x => x.date_updated);
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(sort_field) && sort_order_mode == "desc")
                {
                    switch (sort_field.ToLower())
                    {
                        case "id":
                            users = users.OrderByDescending(x => x.id);
                            break;
                        case "firstname":
                            users = users.OrderByDescending(x => x.firstname);
                            break;
                        case "lastname":
                            users = users.OrderByDescending(x => x.lastname);
                            break;
                        case "gender":
                            users = users.OrderByDescending(x => x.gender);
                            break;
                        case "date_of_birth":
                            users = users.OrderByDescending(x => x.date_of_birth);
                            break;
                        case "date_created":
                            users = users.OrderByDescending(x => x.date_created);
                            break;
                        default:
                            users = users.OrderByDescending(x => x.date_updated);
                            break;
                    }
                }

                users = users.Skip((page.Value - 1) * page_size.Value)
                             .Take(page_size.Value)
                             .ToList();

                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User SelectUserById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentException("Id is required");

                var user = dbContext.Users.SingleOrDefault(x => x.id == id);

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User EditUser(int? id, EditUserModel editUserModel)
        {
            try
            {
                if (id == null)
                    throw new ArgumentException("Id is required in the url");
                if(editUserModel.id == null)
                    throw new ArgumentException("Id is required in the payload");
                if (editUserModel.date_of_birth == null)
                    throw new ArgumentException("Date of birth is required");

                var user = dbContext.Users.SingleOrDefault(x => x.id == id);
                if (user == null)
                    throw new ArgumentException($"User with the Id {id} does not exist");

                user.firstname = editUserModel.firstname;
                user.lastname = editUserModel.lastname;
                user.gender = editUserModel.gender;
                user.date_of_birth = editUserModel.date_of_birth;
                user.date_updated = DateTime.Now;

                dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string DeleteUser(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentException("Id is required");

                var user = dbContext.Users.SingleOrDefault(x => x.id == id);

                dbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();

                return "User record deleted";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}