using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageTools
{
    public struct BBLblock
    {
        public UInt32 rva;
        public UInt32 offset;
        public byte value;
    }

    class BBL
    {
        Dictionary<String, Dictionary<UInt32, BBLblock>> data = new Dictionary<string, Dictionary<UInt32, BBLblock>>();
        Dictionary<String, uint> dataRanges = new Dictionary<string, uint>();

        public bool load(string filename)
        {
            data.Clear();
            dataRanges.Clear();
            BinaryReaderLittleIndian b = new BinaryReaderLittleIndian(File.Open(filename, FileMode.Open));
            if (b.ReadByte() != 0x42 || b.ReadByte() != 0x42 || b.ReadByte() != 0x4C)
                return false;
            while (b.PeekChar() != -1)
            {
                UInt32 fnameLen = b.ReadUInt32();
                String fname = System.Text.Encoding.ASCII.GetString(b.ReadBytes((int)fnameLen));
                fname = fname.ToUpper();
                Dictionary<UInt32, BBLblock> blocks = new Dictionary<UInt32, BBLblock>();
                dataRanges[fname] = b.ReadUInt32();

                while (true)
                {
                    BBLblock block;
                    block.rva = b.ReadUInt32();
                    block.offset = b.ReadUInt32();
                    block.value = b.ReadByte();

                    if (block.rva == 0)
                        break;
                    blocks[block.rva] = block;
                }
                data[fname] = blocks;
            }
            return true;
        }
        public string save(string filename)
        {
            using (BinaryWriter b = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                b.Write(new byte[] { 0x42, 0x42, 0x4C });
                foreach (KeyValuePair<String, Dictionary<UInt32, BBLblock>> entry in data)
                {
                    b.Write(BitConverter.GetBytes(entry.Key.Length));
                    b.Write(System.Text.Encoding.ASCII.GetBytes(entry.Key));
                    b.Write(BitConverter.GetBytes(dataRanges[entry.Key]));

                    foreach (KeyValuePair<UInt32, BBLblock> change in entry.Value)
                    {
                        b.Write(BitConverter.GetBytes(change.Value.rva));
                        b.Write(BitConverter.GetBytes(change.Value.offset));
                        b.Write(change.Value.value);
                    }
                    b.Write(new byte[] { 0, 0, 0, 0 });
                    b.Write(new byte[] { 0, 0, 0, 0 });
                    b.Write(new byte[] { 0 });
                }

            }
            return null;
        }

        public List<string> getModules()
        {
             return new List<string>(data.Keys);
        }

        public List<UInt32> getRVAs(string module)
        {
            module = module.ToUpper();
            if (!data.ContainsKey(module))
                return null;
            return new List<UInt32>(data[module].Keys);
        }

        public BBLblock? getBlock(string module, UInt32 rva)
        {
            module = module.ToUpper();
            if (!data.ContainsKey(module))
                return null;
            if (!data[module].ContainsKey(rva))
                return null;
            return data[module][rva];
        }

        public void removeBlock(string module, UInt32 rva)
        {
            module = module.ToUpper();
            if (!data.ContainsKey(module))
                return;
            if (!data[module].ContainsKey(rva))
                return;
            data[module].Remove(rva);
            if (data[module].Count == 0)
                data.Remove(module);
        }

        public void removeBlocks(BBC bbc)
        {
            List<string> modules = bbc.getModules();
            foreach(string module in modules)
            {
                List<uint> rvas = bbc.getRVAs(module);
                foreach(uint rva in rvas)
                    removeBlock(module, rva);
            }
        }

        public Dictionary<string, int> plant(string baseDir)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (KeyValuePair<String, Dictionary<UInt32, BBLblock>> entry in data)
            {
                string fname = baseDir + "\\" + entry.Key;
                try
                {
                    byte[] bytes = null;
                    if (!File.Exists(fname + "_backup"))
                    {
                        bytes = File.ReadAllBytes(fname);
                        File.WriteAllBytes(fname + "_backup", bytes);
                    }
                    else
                    {
                        bytes = File.ReadAllBytes(fname + "_backup");
                    }

                    foreach (KeyValuePair<UInt32, BBLblock> change in entry.Value)
                        bytes[change.Value.offset] = 0xCC;
                    File.WriteAllBytes(fname, bytes);
                    result.Add(fname, 0);
                }
                catch (FileNotFoundException e)
                {
                    result.Add(fname, 1);
                }
                catch (Exception e)
                {
                    result.Add(fname, 2);
                }
            }

            return result;
        }

        public Dictionary<string, int> restore(string baseDir)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (KeyValuePair<String, Dictionary<UInt32, BBLblock>> entry in data)
            {
                string fname = baseDir + "\\" + entry.Key;
                try
                {
                    byte[] bytes = null;
                    if (!File.Exists(fname + "_backup"))
                    {
                        bytes = File.ReadAllBytes(fname);
                        foreach (KeyValuePair<UInt32, BBLblock> change in entry.Value)
                            bytes[change.Value.offset] = change.Value.value;
                    }
                    else
                    {
                        bytes = File.ReadAllBytes(fname + "_backup");
                        File.Delete(fname + "_backup");
                    }
                    File.WriteAllBytes(fname, bytes);
                    result.Add(fname, 0);
                }
                catch (FileNotFoundException e)
                {
                    result.Add(fname, 1);
                }
                catch (Exception e)
                {
                    result.Add(fname, 2);
                }
            }

            return result;
        }
    }
}
