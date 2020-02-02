namespace GGJ2020
{
    public enum ActorStateFlag
    {
        Walking             = 1 << 0,
        FacingRight         = 1 << 1,
        HoldingJump         = 1 << 2,
        CanDoubleJump       = 1 << 3,
        GroundPounding      = 1 << 4,
        WallJumping         = 1 << 5,
        GroundPoundCasting  = 1 << 6,

        Freezing            = 1 << 26,
        Attacking           = 1 << 27,
        Fighting            = 1 << 28,
        IsDamage            = 1 << 29,
        IsWalling           = 1 << 30,
        IsGrounding         = 1 << 31
    }
}