public class Gift
{
    public float value;
    public bool targetPlayer; // true = text on rod, false = on enemy
    // public ImU32 textColour;
    public bool shouldDisplay;
    // Constructor (optional)
    public Gift(float val, bool target, bool display)
    {
        value = val;
        targetPlayer = target;
        // textColour = colour;
        shouldDisplay = display;
    }
}
