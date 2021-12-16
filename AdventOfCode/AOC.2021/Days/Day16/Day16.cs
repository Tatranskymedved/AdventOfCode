using AOC.Common.Days;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AOC._2021.Days
{
    class Day16 : ADay
    {
        Dictionary<string, int> testVersionSum = new Dictionary<string, int>()
        {
            { "8A004A801A8002F478", 16 },
            { "620080001611562C8802118E34", 12 },
            { "C0015000016115A2E0802F182340", 23 },
            { "A0016C880162017C3686B18A3D4780", 31 },
        };
        Dictionary<string, int> testCalculation = new Dictionary<string, int>()
        {
            { "C200B40A82", 3 },
            { "04005AC33890", 54 },
            { "880086C3E88112", 7 },
            { "CE00C43D881120", 9 },
            { "D8005AC2A8F0", 1 },
            { "F600BC2D8F", 0 },
            { "9C005AC2F8F0", 0 },
            { "9C0141080250320F1802104A08", 1 },
        };

        public override double Main_Double()
        {
            var path = @".\Days\Day16\input.txt";

            var str = System.IO.File.ReadAllLines(path);

            //testVersionSum.ToList().ForEach(a =>
            //{
            //    var test = new BitsPacketParser(a.Key);
            //    Console.WriteLine(test.TotalVersion == a.Value);
            //});
            //testCalculation.ToList().ForEach(a =>
            //{
            //    var test = new BitsPacketParser(a.Key);
            //    Console.WriteLine(test.CalculateValue() == a.Value);
            //});

            var pp = new BitsPacketParser(str.First());

            //return pp.TotalVersion;
            return pp.CalculateValue();
        }
    }

    class BitsPacketParser
    {
        BitsPacket root;
        BitArray ba;
        public int TotalVersion => root.SumOfVersions;
        public long CalculateValue() => root.CalculateValue();

        public BitsPacketParser(string line)
        {
            ba = ConvertHexToBitArray(line);
            root = GetBitsPacket(0);
        }

        public BitsPacket GetBitsPacket(int position)
        {
            var result = new BitsPacket();

            var posVersion = position;
            var posId = posVersion + 3;
            var posData = posId + 3;

            result.Version = 0;
            result.Version |= Convert.ToByte(ba[posVersion]) << 2;
            result.Version |= Convert.ToByte(ba[posVersion + 1]) << 1;
            result.Version |= Convert.ToByte(ba[posVersion + 2]) << 0;

            result.ID = 0;
            result.ID |= Convert.ToByte(ba[posId]) << 2;
            result.ID |= Convert.ToByte(ba[posId + 1]) << 1;
            result.ID |= Convert.ToByte(ba[posId + 2]) << 0;

            result.LiteralNumber = 0;
            result.Operator = 0;
            if (result.ID == 4) // Literal number
            {
                int posDataFirstBit = posData;
                int increment = -1;
                do
                {
                    increment++;
                    if (increment > 0)
                    {
                        posDataFirstBit += 5;
                        result.LiteralNumber = result.LiteralNumber << 4;
                    }

                    result.LiteralNumber |= Convert.ToByte(ba[posDataFirstBit + 1]) << 3;
                    result.LiteralNumber |= Convert.ToByte(ba[posDataFirstBit + 2]) << 2;
                    result.LiteralNumber |= Convert.ToByte(ba[posDataFirstBit + 3]) << 1;
                    result.LiteralNumber |= Convert.ToByte(ba[posDataFirstBit + 4]) << 0;

                }
                while (ba[posDataFirstBit]);



                result.NextPos = posDataFirstBit + 5;
            }
            else //Operator
            {
                result.LengthTypeID |= ba[posData];

                if (result.LengthTypeID)
                {
                    result.NumberOfSubpackets = 0;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 1]) << 10;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 2]) << 9;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 3]) << 8;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 4]) << 7;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 5]) << 6;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 6]) << 5;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 7]) << 4;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 8]) << 3;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 9]) << 2;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 10]) << 1;
                    result.NumberOfSubpackets |= Convert.ToByte(ba[posData + 11]) << 0;

                    int nextPos = posData + 1 + 11;
                    for (int i = 1; i <= result.NumberOfSubpackets; i++)
                    {
                        var subPacket = GetBitsPacket(nextPos);
                        nextPos = subPacket.NextPos;

                        result.subPackets.Add(subPacket);
                    }
                    result.NextPos = result.subPackets.Last().NextPos;
                }
                else
                {
                    result.TotalLengthDataBits = 0;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 1]) << 14;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 2]) << 13;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 3]) << 12;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 4]) << 11;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 5]) << 10;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 6]) << 9;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 7]) << 8;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 8]) << 7;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 9]) << 6;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 10]) << 5;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 11]) << 4;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 12]) << 3;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 13]) << 2;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 14]) << 1;
                    result.TotalLengthDataBits |= Convert.ToByte(ba[posData + 15]) << 0;

                    int currentPos = posData + 1 + 15;
                    int maxPos = posData + 1 + 15 + result.TotalLengthDataBits;
                    while (currentPos < maxPos)
                    {
                        var subPacket = GetBitsPacket(currentPos);
                        currentPos = subPacket.NextPos;
                        result.subPackets.Add(subPacket);
                    }

                    result.NextPos = result.subPackets.LastOrDefault()?.NextPos
                        ?? (currentPos);
                }

            }

            return result;
        }


        //From: https://stackoverflow.com/a/4270342/7167572
        public static BitArray ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
                return null; // or do something else, throw, ...

            BitArray ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }
            }
            return ba;
        }
    }

    class BitsPacket
    {
        public int Version;
        public int ID; //ID 4 == literal number, other than 4 is operator
        public int NextPos;

        public long LiteralNumber;

        public bool LengthTypeID; //0 - next 15 bits are number that represents total length in bits | 1 - next 11 bits are number that represents numbers of sub-packets immediately contained by this packet
        public int NumberOfSubpackets;
        public int TotalLengthDataBits;
        public int Operator;

        public List<BitsPacket> subPackets = new List<BitsPacket>();
        public int SumOfVersions => Version + subPackets.Sum(a => a.SumOfVersions);

        public long CalculateValue()
        {
            switch (ID)
            {
                case 0:
                    return subPackets.Sum(a => a.CalculateValue());
                case 1:
                    if (subPackets.Count() == 1) return subPackets.First().CalculateValue();
                    else
                    {
                        long product = 1;
                        subPackets.ForEach(a => product *= a.CalculateValue());
                        return product;
                    }
                case 2:
                    return subPackets.Min(a => a.CalculateValue());
                case 3:
                    return subPackets.Max(a => a.CalculateValue());
                case 4:
                    return LiteralNumber;
                case 5:
                    return (subPackets[0].CalculateValue() > subPackets[1].CalculateValue()) ? 1 : 0;
                case 6:
                    return (subPackets[0].CalculateValue() < subPackets[1].CalculateValue()) ? 1 : 0;
                case 7:
                    return (subPackets[0].CalculateValue() == subPackets[1].CalculateValue()) ? 1 : 0;
                default:
                    throw new Exception();
            }
        }

        public override string ToString()
        {
            return $"V{Version}: {ID}";
        }
    }
}
