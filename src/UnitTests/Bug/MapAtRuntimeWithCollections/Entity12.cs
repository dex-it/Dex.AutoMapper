namespace OmmitedDatabaseModel3WithCollections;

public class Entity12 : BaseEntity
{
    public Entity12()
    {
        Entities20 = new List<Entity20>();
        Entities14 = new List<Entity14>();
        Entities16 = new List<Entity16>();
    }
    public ICollection<Entity20> Entities20 { get; set; }
    public ICollection<Entity16> Entities16 { get; set; }
    public ICollection<Entity14> Entities14 { get; set; }
}
