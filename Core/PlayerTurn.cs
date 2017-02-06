using System;

namespace Core
{
    [Flags]
    public enum PlayerTurn : uint
    {
        WAIT = 0x00000000,
        TURN = 0xFFFFFFFF
    }
}
