namespace OmmitedDatabaseModel3WithCollections;

public class Entity14 : BaseEntity
{
    public Entity14()
    {
        Entities12 = new List<Entity12>();
        Entities1 = new List<Entity1>();
    }

    public ICollection<Entity12> Entities12 { get; set; }
    public ICollection<Entity1> Entities1 { get; set; }
}
