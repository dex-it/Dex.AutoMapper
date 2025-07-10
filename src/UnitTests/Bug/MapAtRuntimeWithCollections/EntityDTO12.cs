namespace OmmitedDTOModel3WithCollections;

public class EntityDTO12 : BaseEntity
{
    public EntityDTO12()
    {
        Entities20 = new List<EntityDTO20>();
        Entities14 = new List<EntityDTO14>();
        Entities16 = new List<EntityDTO16>();
    }
    public ICollection<EntityDTO20> Entities20 { get; set; }
    public ICollection<EntityDTO16> Entities16 { get; set; }
    public ICollection<EntityDTO14> Entities14 { get; set; }
}
