public class Stats
{
    public int attack;
    public int healing_bonus;
    public int speed;
    public float critical_rate = 0.1f;
    public float critical_value = 1.5f; //default crit values, 10% chance to do 1.5x damage
    // Constructor (optional)
    public Stats(int atk, int heal, int spd, float crit_rate, float crit_val)
    {
        attack = atk;
        healing_bonus = heal;
        speed = spd;
        critical_rate = crit_rate;
        critical_value = crit_val;
    }
}
