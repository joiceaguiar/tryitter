using TryitterAPI.Models;
using TryitterAPI.Models.Entities;
using TryitterAPI.Repository;

namespace Tryitter.Test
{
    public class TryitterRepositoryTest
    {

        [Trait("Estudante", "1 - Endpoint para criar nova pessoa estudante")]
        [Theory(DisplayName = "Cria um novo input de estudante e retorna um token")]
        [MemberData(nameof(TestCreateNewStudentData))]
        public void TestCreateNewStudent(TryitterContext context, string name, string email, string password)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Student student = new() { Name = name, Email = email, Password = password };
            var token = _tryitterRepository.CreateStudent(student);

            token.Should().NotBeNull();
            Student response = context.Students.Where(s => s.Email == email).FirstOrDefault()!;
            response.Email.Should().Be(email);

            var responseFail = _tryitterRepository.CreateStudent(student);
            responseFail.Should().BeNull();

        }
        public readonly static TheoryData<TryitterContext, string, string, string> TestCreateNewStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestCreateNewStudent"),
                "Usera",
                "usertesta@email.com",
                "123456789"
            },
        };

        [Trait("Estudante", "2 - Endpoint para logar como pessoa estudante")]
        [Theory(DisplayName = "Gera um Token a partir do login")]
        [MemberData(nameof(TestLoginStudentData))]
        public void TestLoginStudent(TryitterContext context, string email, string password, string fakeEmail)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Entities.Login login = new() { Email = email, Password = password };
            var token = _tryitterRepository.StudentLogin(login);

            token.Should().NotBeNull();

            Entities.Login fakeLogin = new() { Email = fakeEmail, Password = password };
            var response = _tryitterRepository.StudentLogin(fakeLogin);
            response.Should().BeNull();


        }
        public readonly static TheoryData<TryitterContext, string, string, string> TestLoginStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestLoginStudent"),
                "usertesta@email.com",
                "123456789",
                "usertestb@email.com"
            },
        };

        [Trait("Post", "3 - Endpoint para criar uma postagem")]
        [Theory(DisplayName = "Cria um novo Post")]
        [MemberData(nameof(TestNewPostData))]
        public void TestNewPost(TryitterContext context, Post post)
        {
            TryitterRepository? _tryitterRepository = new(context);
            _tryitterRepository.AddPost(post);


            var response = context.Post.Where(p => p.Id == 5).FirstOrDefault()!;
            response.Should().BeEquivalentTo(post);


        }
        public readonly static TheoryData<TryitterContext, Post> TestNewPostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestNewPost"),
                new Post {
                Id = 5,
                Title =  "Titulo a",
                Text = "Texto a",
                StudentId = 1,
                }
            },
        };

        [Trait("Post", "4 - Endpoint para buscar pessoa estudante")]
        [Theory(DisplayName = "Busca pessoa estudante")]
        [MemberData(nameof(TestGetStudentData))]
        public void TestGetStudent(TryitterContext context, int id)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Student student = _tryitterRepository.GetStudent(id)!;


            student.Id.Should().Be(id);
            student.Email.Should().Be("usertesta@email.com");


        }
        public readonly static TheoryData<TryitterContext, int> TestGetStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestGetStudent"),
                3
            },
        };

        [Trait("Post", "5 - Endpoint para buscar postagem")]
        [Theory(DisplayName = "Busca postagem")]
        [MemberData(nameof(TestGetPostData))]
        public void TestGetPost(TryitterContext context, int id)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Post post = _tryitterRepository.GetPost(id)!;


            post.Id.Should().Be(id);
            post.Text.Should().Be("Texto do título a");


        }
        public readonly static TheoryData<TryitterContext, int> TestGetPostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestGetPost"),
                3
            },
        };

        [Trait("Estudante", "6 - Endpoint para remover pessoa estudante")]
        [Theory(DisplayName = "Remover pessoa estudante pelo ID")]
        [MemberData(nameof(TestRemoveStudentData))]
        public void TestRemoveStudent(TryitterContext context, int id)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Student student = context.Students.Find(id)!;

            student.Id.Should().Be(id);
            _tryitterRepository.RemoveStudent(student);

            Student? nullStudent = context.Students.Where(s => s.Id == id).FirstOrDefault()!;
            nullStudent.Should().BeNull();

        }
        public readonly static TheoryData<TryitterContext, int> TestRemoveStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestRemoveStudent"),
                3
            },
        };

        [Trait("Post", "7 - Endpoint para remover post")]
        [Theory(DisplayName = "Remover post pelo ID")]
        [MemberData(nameof(TestRemovePostData))]
        public void TestRemovePost(TryitterContext context, int id)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Post post = context.Post.Find(id)!;

            post.Id.Should().Be(id);
            _tryitterRepository.RemovePost(post);

            Post? nullPost = context.Post.Where(s => s.Id == id).FirstOrDefault()!;
            nullPost.Should().BeNull();

        }
        public readonly static TheoryData<TryitterContext, int> TestRemovePostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestRemovePost"),
                1
            },
        };

        [Trait("Post", "8 - Endpoint para buscar todos os posts")]
        [Theory(DisplayName = "Buscar todas as postagems")]
        [MemberData(nameof(TestGetAllPostsData))]
        public void TestGetAllPosts(TryitterContext context, int id, List<Post> expect)
        {
            TryitterRepository? _tryitterRepository = new(context);
            List<Post> posts = _tryitterRepository.AllPosts(id)!;

            posts.Should().BeAssignableTo<List<Post>>();
            posts.Should().BeEquivalentTo(expect);

        }
        public readonly static TheoryData<TryitterContext, int, List<Post>> TestGetAllPostsData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestGetAllPosts"),
                2,
                new List<Post>() {
                new Post {
                    Id = 3,
                    Title =  "Titulo do texto a",
                    Text = "Texto do título a",
                    StudentId = 2,
                },
                new Post {
                    Id = 4,
                    Title =  "Titulo final",
                    Text = "Ultimo post",
                    StudentId = 2,
                }
                }
            }
        };

        [Trait("Post", "9 - Endpoint para buscar ultimo post")]
        [Theory(DisplayName = "Buscar ultimo post")]
        [MemberData(nameof(TestGetLastPostData))]
        public void TestGetLastPost(TryitterContext context, int id, Post expect)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Post post = _tryitterRepository.LastPost(id)!;

            post.Should().BeAssignableTo<Post>();
            post.Should().BeEquivalentTo(expect);
        }
        public readonly static TheoryData<TryitterContext, int, Post> TestGetLastPostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestGetLastPost"),
                2,
                new Post {
                    Id = 4,
                    Title =  "Titulo final",
                    Text = "Ultima postagem",
                    StudentId = 2,
                }
            }
        };

        [Trait("Estudante", "10 - Endpoint para editar dados da pessoa estudante")]
        [Theory(DisplayName = "Editar pessoa estudante")]
        [MemberData(nameof(TestEditStudentData))]
        public void TestEditStudent(TryitterContext context, int id, string password)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Entities.UpdateStudent update = new() { Password = password, Name = "teste" };
            Student student = context.Students.Find(id)!;
            _tryitterRepository.EditStudent(student, update);

            Student response = context.Students.Find(student.Id)!;
            response.Password.Should().Be(password);
        }
        public readonly static TheoryData<TryitterContext, int, string> TestEditStudentData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestEditStudent"),
               1,
               "senha123456789"
            }
        };

        [Trait("Post", "11 - Endpoint para editar dados da postagem")]
        [Theory(DisplayName = "Editar postagem")]
        [MemberData(nameof(TestEditPostData))]
        public void TestEditPost(TryitterContext context, int id, string title)
        {
            TryitterRepository? _tryitterRepository = new(context);
            Entities.UpdatePost update = new() { Title = title };
            Post post = context.Post.Find(id)!;
            _tryitterRepository.EditPost(post, update);

            Post response = context.Post.Find(post.Id)!;
            response.Title.Should().Be(title);
            response.Title.Should().NotBe("Titulo claramente muito deveras bem pensado");
        }
        public readonly static TheoryData<TryitterContext, int, string> TestEditPostData =
        new()
        {
            {
                Helper.GetContextInstanceForTests("TestEditPost"),
               2,
               "Titulo do último texto"
            }
        };
    }
}