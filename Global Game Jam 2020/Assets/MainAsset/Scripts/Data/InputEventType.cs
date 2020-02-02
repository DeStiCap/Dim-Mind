namespace GGJ2020
{
    [System.Flags]
    public enum InputEventType
    {
        Jump    =   1 << 0,
        Attack  =   1 << 1
    }
}