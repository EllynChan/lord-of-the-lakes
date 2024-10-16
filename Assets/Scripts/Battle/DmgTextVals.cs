public class DmgTextVals
{
    public float value;
    public bool targetPlayer; // true = text on rod, false = on enemy
    // public ImU32 textColour;
    public bool shouldDisplay;
    // Constructor (optional)
    public DmgTextVals(float val, bool target, bool display)
    {
        value = val;
        targetPlayer = target;
        // textColour = colour;
        shouldDisplay = display;
    }
}
