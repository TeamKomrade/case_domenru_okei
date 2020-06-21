using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CaseDomenru.Data
{
# region контекст базы данных
    public class CaseDomenruDB : DbContext
    {
        public CaseDomenruDB(DbContextOptions<CaseDomenruDB> options)
            : base(options)
        {
            //создаём тестового пользователя
            if (Database.EnsureCreated())
            {
                Users.Add(new User()
                {
                    Login = Utils.GetHash("test"),
                    Password = Utils.GetHash("test"),
                    Email = NameValidation.idn.GetAscii("unnamed1@тестовая-зона.рф"),
                    UniqueCode = new UniqueKey()
                    {
                        UniqueKeyString = "test"
                    },
                    Person = new Person()
                    {
                        PersonID = 1,
                        FirstName = "Тест",
                        LastName = "Тестов",
                        Patronymic = "Модулевич",
                        DateOfBirth = DateTime.Now.AddYears(-33)
                    }
                });
                SaveChanges();
            };
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<UniqueKey> UniqueCodes { get; set; }
    }

    //Пользователь
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [ForeignKey("UniqueKey")]
        public int UniqueCodeID { get; set; }
        public virtual UniqueKey UniqueCode { get; set; }
        public virtual Person Person { get; set; }
        public Roles Role { get; set; }
    }

    //личные данные пользователя
    public class Person
    {
        [ForeignKey("User")]
        public int PersonID { get; set; }
        [Column(TypeName = "varchar(96)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(96)")]
        public string LastName { get; set; }
        [Column(TypeName = "varchar(96)")]
        public string Patronymic { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateOfBirth { get; set; }
        
    }

    //роли
    public enum Roles
    {
        Ученик,
        Родитель,
        Преподаватель
    }

    //уникальный ключ
    public class UniqueKey
    {
        public int UniqueKeyID { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string UniqueKeyString { get; set; }
        [ForeignKey("User")]
        public int KeyCreatorID { get; set; }
        public virtual User User { get; set; }
    }
    #endregion
}
