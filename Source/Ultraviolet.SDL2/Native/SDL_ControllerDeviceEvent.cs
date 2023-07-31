﻿using System;
using System.Runtime.InteropServices;
using SDL_JoystickID = System.Int32;

namespace Ultraviolet.SDL2.Native
{
#pragma warning disable 1591
    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_ControllerDeviceEvent
    {
        public UInt32 type;
        public UInt32 timestamp;
        public SDL_JoystickID which;
    }
#pragma warning restore 1591
}