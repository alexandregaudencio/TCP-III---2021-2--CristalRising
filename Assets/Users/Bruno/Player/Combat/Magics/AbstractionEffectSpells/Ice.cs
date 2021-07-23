public class Ice : Spells, Effect
{
    public override void Use()
    {
        base.Use();
    }
    public void Apply()
    {
        this.Use();
        throw new System.NotImplementedException();
    }
}
