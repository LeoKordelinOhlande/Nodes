using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PcapDotNet.TestUtils
{
    [ExcludeFromCodeCoverage]
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random)
        {
            return random.NextByte() >= 128;
        }

        public static byte NextByte(this Random random, int minValue, int maxValue)
        {
            return (byte)random.NextInt(minValue, maxValue);
        }

        public static byte NextByte(this Random random, int maxValue)
        {
            return (byte)random.NextInt(0, maxValue);
        }

        public static byte NextByte(this Random random)
        {
            return random.NextBytes(1)[0];
        }

        public static byte[] NextBytes(this Random random, int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }

        public static char NextChar(this Random random, char minValue, char maxValue)
        {
            return (char)random.NextInt(minValue, maxValue);
        }

        public static ushort NextUShort(this Random random, int maxValue)
        {
            return (ushort)random.NextInt(0, maxValue);
        }

        public static ushort NextUShort(this Random random, ushort minValue, int maxValue)
        {
            return (ushort)random.NextInt(minValue, maxValue);
        }

        public static ushort NextUShort(this Random random)
        {
            return random.NextUShort(ushort.MaxValue + 1);
        }

        public static int NextInt(this Random random)
        {
            return random.Next(int.MinValue, int.MaxValue);
        }

        public static int NextInt(this Random random, int maxValue)
        {
            return random.Next(int.MinValue, maxValue);
        }

        public static int NextInt(this Random random, int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public static uint NextUInt(this Random random)
        {
            return unchecked((uint)random.NextInt());
        }

        public static uint NextUInt(this Random random, uint maxValue)
        {
            return random.NextUInt() % maxValue;
        }

        public static uint NextUInt(this Random random, uint minvalue, uint maxValue)
        {
            return random.NextUInt() % (maxValue - minvalue + 1) + minvalue;
        }

        public static long NextLong(this Random random, long minValue, long maxValue)
        {
            return minValue + unchecked((long)random.NextULong(unchecked((ulong)(maxValue - minValue))));
        }

        public static long NextLong(this Random random, long maxValue)
        {
            return random.NextLong(0, maxValue);
        }

        public static long NextLong(this Random random)
        {
            return unchecked((long)random.NextULong());
        }

        public static ulong NextULong(this Random random, ulong maxValue)
        {
            return random.NextULong() % maxValue;
        }

        public static ulong NextULong(this Random random)
        {
            return ((unchecked((ulong)random.NextUInt())) << 32) + random.NextUInt();
        }
    }
}