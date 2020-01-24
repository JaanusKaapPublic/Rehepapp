using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageTools
{
    class BBC
    {
        Dictionary<string, Dictionary<UInt32, bool>> data = new Dictionary<string, Dictionary<UInt32, bool>>();
        Dictionary<UInt32, string> id2module = new Dictionary<uint, string>();
        Dictionary<string, UInt32> module2id = new Dictionary<string, UInt32>();
        uint count = 0;

        public string load(string filename)
        {
            BinaryReaderLittleIndian b = new BinaryReaderLittleIndian(File.Open(filename, FileMode.Open));
                if (b.ReadByte() != 0x42 || b.ReadByte() != 0x42 || b.ReadByte() != 0x43)
                    return "Missing magic bytes";
                Dictionary<UInt32, BBLblock> blocks = new Dictionary<UInt32, BBLblock>();

                while (b.PeekChar() != -1)
                {
                    byte type = b.ReadByte();
                    if (type == 0x1)
                    {
                        UInt32 pid = b.ReadUInt32();
                        UInt32 id = b.ReadByte() + (1000 * pid);
                        string module = "";
                        for (byte[] chrs = b.ReadBytes(1); chrs[0] != 0x00; chrs = b.ReadBytes(1))
                            module += System.Text.Encoding.ASCII.GetString(chrs);
                        module = module.ToUpper();
                        if(!data.ContainsKey(module))
                            data[module] = new Dictionary<uint, bool>();
                        id2module[id] = module;
                        module2id[module] = id;
                    }
                    if (type == 0x2)
                    {
                        UInt32 pid = b.ReadUInt32();
                        UInt32 id = b.ReadByte() + (1000 * pid);
                        UInt32 rva = b.ReadUInt32();
                        if (!id2module.ContainsKey(id))
                            return "RVA refers to ID " + id + " that is not before defined";
                        if (!data[id2module[id]].ContainsKey(rva))
                            count++;
                        data[id2module[id]][rva] = true;
                    }
                }
                return null;
        }

        public string save(string filename)
        {
            using (BinaryWriter b = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                b.Write(new byte[]{ 0x42, 0x42, 0x43 });
                List<string> modules = getModules();
                byte id = 0x00;
                foreach(string module in modules)
                {
                    b.Write(0x1);
                    b.Write(new byte[] { 0x0, 0x0, 0x0, 0x0 });
                    b.Write(id);
                    b.Write(System.Text.Encoding.ASCII.GetBytes(module));
                    b.Write(0x0);

                    List<UInt32> rvas = getRVAs(module);
                    foreach(UInt32 rva in rvas)
                    {
                        b.Write(0x2);
                        b.Write(new byte[] { 0x0, 0x0, 0x0, 0x0 });
                        b.Write(id);
                        b.Write(BitConverter.GetBytes(rva));
                    }
                }
            }
            return null;
        }


        public List<string> getModules()
        {
            return new List<string>(module2id.Keys);
        }

        public List<UInt32> getRVAs(string module)
        {
            module = module.ToUpper();
            if (!module2id.ContainsKey(module))
                return null;
            return new List<UInt32>(data[module].Keys);
        }

        public void removeBlock(string module, UInt32 rva)
        {
            module = module.ToUpper();
            if (!module2id.ContainsKey(module))
                return;
            if (!data[module].ContainsKey(rva))
                return;
            count--;
            data[module].Remove(rva);
            if (data[module].Count == 0)
            {
                data.Remove(module);
                id2module.Remove(module2id[module]);
                module2id.Remove(module);
            }
        }

        public void removeBlocks(BBC bbc)
        {
            List<string> modules = bbc.getModules();
            foreach (string module in modules)
            {
                List<uint> rvas = bbc.getRVAs(module);
                foreach (uint rva in rvas)
                    removeBlock(module, rva);
            }
        }

        public bool contains(string module, UInt32 rva)
        {
            module = module.ToUpper();
            if (!module2id.ContainsKey(module))
                return false;
            return data[module].ContainsKey(rva);
        }

        public void commons(BBC other)
        {
            List<string> modules = getModules();
            foreach(string module in modules)
            {
                List<UInt32> rvas = getRVAs(module);
                foreach (UInt32 rva in rvas)
                {
                    if (!other.contains(module, rva))
                        removeBlock(module, rva);
                }
            }
        }

        public uint getCount()
        {
            return count;
        }
    }
}
