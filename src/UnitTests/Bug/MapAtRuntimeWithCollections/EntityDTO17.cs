namespace OmmitedDTOModel3WithCollections;

public class EntityDTO17 :BaseEntity
{
    public EntityDTO17()
    {
        Entities20 = new List<EntityDTO20>();
        Entities8 = new List<EntityDTO8>();
        Entities5 = new List<EntityDTO5>();
        Entities18 = new List<EntityDTO18>();
    }

    public ICollection<EntityDTO20> Entities20 { get; set; }
    public ICollection<EntityDTO8> Entities8 { get; set; }
    public ICollection<EntityDTO5> Entities5 { get; set; }
    public ICollection<EntityDTO18> Entities18 { get; set; }
}
