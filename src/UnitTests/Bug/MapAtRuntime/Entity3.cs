namespace OmmitedDatabaseModel3;

public class Entity3 : BaseEntity
{
    public Entity3()
    {
        Entities4 = new Entity4();
        Entities8 = new Entity8();
    }
    public Entity4 Entities4 { get; set; }
    public Entity8 Entities8 { get; set; }
}
