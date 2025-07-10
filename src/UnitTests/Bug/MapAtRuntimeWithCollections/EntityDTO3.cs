namespace OmmitedDTOModel3WithCollections;

public class EntityDTO3 : BaseEntity
{
    public EntityDTO3()
    {
        Entities4 = new List<EntityDTO4>();
        Entities8 = new List<EntityDTO8>();
    }
    public ICollection<EntityDTO4> Entities4 { get; set; }
    public ICollection<EntityDTO8> Entities8 { get; set; }
}
