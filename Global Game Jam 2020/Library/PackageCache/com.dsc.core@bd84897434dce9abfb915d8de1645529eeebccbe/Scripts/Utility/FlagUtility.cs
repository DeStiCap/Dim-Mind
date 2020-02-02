using System;

namespace DSC.Core
{
    public static class FlagUtility
    {
        public static bool HasFlagUnsafe<TEnum>(TEnum hFlags, TEnum hCheckFlag) where TEnum : unmanaged, Enum
        {

            unsafe
            {
                switch (sizeof(TEnum))
                {
                    case 1:
                        return (*(byte*)(&hFlags) & *(byte*)(&hCheckFlag)) > 0;
                    case 2:
                        return (*(ushort*)(&hFlags) & *(ushort*)(&hCheckFlag)) > 0;
                    case 4:
                        return (*(uint*)(&hFlags) & *(uint*)(&hCheckFlag)) > 0;
                    case 8:
                        return (*(ulong*)(&hFlags) & *(ulong*)(&hCheckFlag)) > 0;
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }

            }
        }

        public static bool HasAllFlagUnsafe<TEnum>(TEnum hFlags, TEnum hCheckFlag) where TEnum : unmanaged, Enum
        {

            unsafe
            {
                switch (sizeof(TEnum))
                {
                    case 1:
                        return ((*(byte*)(&hCheckFlag) & ~((*(byte*)(&hFlags))
                                    & ~(*(byte*)(&hFlags) & ~*(byte*)(&hCheckFlag)))) == 0);
                    case 2:
                        return ((*(ushort*)(&hCheckFlag) & ~((*(ushort*)(&hFlags))
                                    & ~(*(ushort*)(&hFlags) & ~*(ushort*)(&hCheckFlag)))) == 0);
                    case 4:
                        return ((*(uint*)(&hCheckFlag) & ~((*(uint*)(&hFlags))
                                    & ~(*(uint*)(&hFlags) & ~*(uint*)(&hCheckFlag)))) == 0);
                    case 8:
                        return ((*(ulong*)(&hCheckFlag) & ~((*(ulong*)(&hFlags))
                                    & ~(*(ulong*)(&hFlags) & ~*(ulong*)(&hCheckFlag)))) == 0);
                    default:
                        throw new Exception("Size does not match a known Enum backing type.");
                }

            }
        }
    }
}