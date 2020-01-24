using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageTools
{
    class BinaryReaderLittleIndian : BinaryReader
    {
        public BinaryReaderLittleIndian(System.IO.Stream stream) : base(stream) { }

        public override Int16 ReadInt16()
        {
            var data = base.ReadBytes(2);
            if(!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        public override Int32 ReadInt32()
        {
            var data = base.ReadBytes(4);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public override Int64 ReadInt64()
        {
            var data = base.ReadBytes(8);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        public override UInt16 ReadUInt16()
        {
            var data = base.ReadBytes(2);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToUInt16(data, 0);
        }

        public override UInt32 ReadUInt32()
        {
            var data = base.ReadBytes(4);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        public override UInt64 ReadUInt64()
        {
            var data = base.ReadBytes(8);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(data);
            return BitConverter.ToUInt64(data, 0);
        }
    }
}
