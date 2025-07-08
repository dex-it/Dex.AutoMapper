namespace AutoMapper.UnitTests.Bug;

public class GenericCreateMapsWithCircularReference : AutoMapperSpecBase
{
    protected override MapperConfiguration CreateConfiguration() => new(cfg =>
    {
        cfg.CreateMap(typeof(User<>), typeof(UserPoco<>));
        cfg.CreateMap(typeof(Role<>), typeof(RolePoco<>));
        cfg.CreateMap(typeof(UsersInRole<>), typeof(UsersInRolePoco<>));
        cfg.ForAllMaps((t, c) =>
        {
            c.PreserveReferences();
        });
    });

    [Fact]
    public void Main()
    {
        var role = new Role<int>();
        var user = new User<int>()
        {
            UsersInRoles = new List<UsersInRole<int>>()
        };
        user.UsersInRoles.Add(new UsersInRole<int>()
        {
            Role = role,
            User = user
        });

        var result = Mapper.Map<UserPoco<int>>(user);
    }

    public sealed partial class Role<T>
    {
        public Role()
        {
            UsersInRoles = new List<UsersInRole<T>>();
        }
        public IList<UsersInRole<T>> UsersInRoles { get; set; }
    }

    public sealed partial class RolePoco<T>
    {
        public RolePoco()
        {
            UsersInRoles = new List<UsersInRolePoco<T>>();
        }
        public IList<UsersInRolePoco<T>> UsersInRoles { get; set; }
    }

    public sealed partial class User<T>
    {
        public User()
        {
            UsersInRoles = new List<UsersInRole<T>>();
        }
        public IList<UsersInRole<T>> UsersInRoles { get; set; }
    }

    public sealed partial class UserPoco<T>
    {
        public UserPoco()
        {
            UsersInRoles = new List<UsersInRolePoco<T>>();
        }
        public IList<UsersInRolePoco<T>> UsersInRoles { get; set; }
    }

    public partial class UsersInRole<T>
    {
        public virtual Role<T> Role { get; set; }
        public virtual User<T> User { get; set; }
    }

    public partial class UsersInRolePoco<T>
    {
        public virtual RolePoco<T> Role { get; set; }
        public virtual UserPoco<T> User { get; set; }
    }

}