namespace DSC.Template.Actor.SideScrolling2D
{
    [System.Flags]
    public enum ActorStateFlag
    {
        Walking         = 1 << 0,

        IsWalling       = 1 << 30,
        IsGrounding     = 1 << 31
    }
}