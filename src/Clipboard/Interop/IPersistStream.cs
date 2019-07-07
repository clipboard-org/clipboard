using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Clipboard.Interop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010c-0000-0000-C000-000000000046")]
    public interface IPersist
    {
        void GetClassID(out Guid pClassID);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000109-0000-0000-C000-000000000046")]
    public interface IPersistStream : IPersist
    {
        new void GetClassID(out Guid pClassID);

        [PreserveSig]
        int IsDirty();

        void Load([In] IStream pStm);

        void Save(
            [In] IStream pStm,
            [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

        void GetSizeMax(out long pcbSize);
    }
}

