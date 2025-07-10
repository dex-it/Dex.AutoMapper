namespace OmmitedDatabaseModel3WithCollections;

public class Entity10 : BaseEntity
{
    public Entity10()
    {
        Entities11 = new List<Entity11>();
    }
    public ICollection<Entity11> Entities11 { get; set; }
}
