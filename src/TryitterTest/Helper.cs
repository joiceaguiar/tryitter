using Microsoft.EntityFrameworkCore;
using TryitterAPI.Models;
using TryitterAPI.Repository;

namespace Tryitter.Test
{
    public class Helper
    {
        public static TryitterContext GetContextInstanceForTests(string inMemoryDbName)
        {
            var contextOptins = new DbContextOptionsBuilder<TryitterContext>()
                .UseInMemoryDatabase(inMemoryDbName)
                .Options;

            var context = new TryitterContext(contextOptins);
            Console.WriteLine(context);

            context.Students.AddRange(GetStudentListForTests());
            context.SaveChanges();

            context.Post.AddRange(GetSPostListForTests());
            context.SaveChanges();


            return context;
        }

        public static List<Student> GetStudentListForTests() =>
        new() {
            new Student {
                Id = 1,
                Name =  "Usera",
                Email = "usertesta@email.com",
                Password = "123456789"
            },
            new Student {
                Id = 2,
                Name =  "Userb",
                Email = "usertestb@email.com",
                Password = "123456789"
            },
            new Student {
                Id = 3,
                Name =  "Userc",
                Email = "usertestc@email.com",
                Password = "123456789"
            },
        };

        public static List<Post> GetSPostListForTests() =>
        new() {
            new Post {
                Id = 1,
                Title =  "Este é o título a",
                Text = "Texto do título a",
                StudentId = 1,
            },
            new Post {
                Id = 2,
                Title =  "Este é o título b",
                Text = "Texto do título b",
                StudentId = 3,
            },
            new Post {
                Id = 3,
                Title =  "Este é o título c",
                Text = "Texto do título c",
                StudentId = 2,
            },
            new Post {
                Id = 4,
                Title =  "Titulo d",
                Text = "Texto do título c",
                StudentId = 2,
            },
        };
    }
}