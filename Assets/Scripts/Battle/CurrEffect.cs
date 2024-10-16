public class CurrEffect
{
    public string skill_name;
    public Effect effect;
    public float value;
    // Constructor (optional)
    public CurrEffect(string name, Effect effect, float val)
    {
        skill_name = name;
        this.effect = effect;
        value = val;
    }
}
