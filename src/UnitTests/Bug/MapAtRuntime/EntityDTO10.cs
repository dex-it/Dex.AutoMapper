namespace OmmitedDTOModel3;

public class EntityDTO10 : BaseEntity
{
    public EntityDTO10()
    {
        Entities11 = new EntityDTO11();
    }
    public EntityDTO11 Entities11 { get; set; }
}
