namespace OmmitedDTOModel3WithCollections;

public class EntityDTO8 : BaseEntity
{
    public EntityDTO8()
    {
        Entities20 = new List<EntityDTO20>();
        Entities22 = new List<EntityDTO22>();
        Entities3 = new List<EntityDTO3>();
        Entities11 = new List<EntityDTO11>();
        Entities17 = new List<EntityDTO17>();
    }

    public ICollection<EntityDTO20> Entities20 { get; set; }
    public ICollection<EntityDTO17> Entities17 { get; set; }
    public ICollection<EntityDTO22> Entities22 { get; set; }
    public ICollection<EntityDTO3> Entities3 { get; set; }
    public ICollection<EntityDTO11> Entities11 { get; set; }
    public EntityDTO11 Entity11 { get; set; }
}
