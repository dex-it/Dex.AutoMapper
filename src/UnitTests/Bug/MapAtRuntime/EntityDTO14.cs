namespace OmmitedDTOModel3;

public class EntityDTO14 : BaseEntity
{
    public EntityDTO14()
    {
        Entities12 = new EntityDTO12();
        Entities1 = new EntityDTO1();
    }

    //public Address Address { get; set; }
    public EntityDTO12 Entities12 { get; set; }
    public EntityDTO1 Entities1 { get; set; }
}
