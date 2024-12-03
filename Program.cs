using System;
using Blog.Models;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Blog.Repositories;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true";

        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
            // ReadUser(connection);
            // CreateUser(connection);
            // UpdateUser(connection);
            // ReadUsers(connection);
            DeleteUser();
            connection.Close();
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.GetAll();

            foreach (var user in users)
                Console.WriteLine($"Id: {user.Id} - Nome: {user.Name} - Slug: {user.Slug} - Bio: {user.Bio}");
        }

        public static void ReadUser(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var user = repository.GetById(2);

            Console.WriteLine($"Id: {user.Id} - {user.Name} - {user.Email} - {user.Slug}");
        }

        public static void CreateUser(SqlConnection connection)
        {
            User user = new()
            {
                Name = "Renato",
                Email = "renatim@gmail.com",
                PasswordHash = "123123",
                Bio = "Renato Estuda React",
                Image = "renato_react_front.png",
                Slug = "https://renatu"
            };

            var repository = new UserRepository(connection);
            repository.Create(user);
            Console.WriteLine($"Cadastrado realizado com sucesso.");
        }

        public static void UpdateUser(SqlConnection connection)
        {
            User user = new()
            {
                Id = 11,
                Name = "Renato Suporte",
                Email = "renatimsuppoort@gmail.com",
                PasswordHash = "12",
                Bio = "Renato | Estuda Reactxxx",
                Image = "renato_react_front.png",
                Slug = "renatusrufux"
            };

            var repository = new UserRepository(connection);
            repository.Update(user);

            Console.WriteLine($"Atualização realizada com sucesso.");
        }

        public static void DeleteUser()
        {
            using var connection = new SqlConnection(CONNECTION_STRING);

            var user = connection.Get<User>(11);
            connection.Delete<User>(user);
            Console.WriteLine("Usuário excluído com sucesso.");
        }
    }
}
