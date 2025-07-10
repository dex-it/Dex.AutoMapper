namespace OmmitedDatabaseModel3WithCollections;

public class Entity5 : BaseEntity
{
    public Entity5()
    {
        Entities6 = [];
        TimeSlots = new List<Entity23>();
        Entities5 = new List<Entity5>();
    }

    public Guid? Entity8Id { get; set; }
    public Entity8 Entity8 { get; set; }
    public Guid? Entity17Id { get; set; }
    public Entity17 Entity17 { get; set; }
    public Guid? Entity5Id { get; set; }
    public Entity5 Entity5Exception { get; set; }
    public ICollection<Entity5> Entities5 { get; set; }
    public List<Entity6> Entities6 { get; set; }
    public ICollection<Entity23> TimeSlots { get; set; }
}
