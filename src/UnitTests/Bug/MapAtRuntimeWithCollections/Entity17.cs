namespace OmmitedDatabaseModel3WithCollections;

public class Entity17 :BaseEntity
{
    public Entity17()
    {
        Entities20 = new List<Entity20>();
        Entities8 = new List<Entity8>();
        Entities5 = new List<Entity5>();
        Entities18 = new List<Entity18>();
    }

    public ICollection<Entity20> Entities20 { get; set; }
    public ICollection<Entity8> Entities8 { get; set; }
    public ICollection<Entity5> Entities5 { get; set; }
    public ICollection<Entity18> Entities18 { get; set; }
}
