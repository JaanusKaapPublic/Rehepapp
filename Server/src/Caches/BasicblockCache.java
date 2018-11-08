package Caches;

public class BasicblockCache 
{
    ModuleCache parent;
    int id;
    int rva;
    
    public BasicblockCache(ModuleCache parent, int id, int rva)
    {
        this.parent = parent;
        this.id = id;
        this.rva = rva;
    }

    public ModuleCache getParent() 
    {
        return parent;
    }

    public void setParent(ModuleCache parent) 
    {
        this.parent = parent;
    }

    public int getId() 
    {
        return id;
    }

    public void setId(int id) 
    {
        this.id = id;
    }

    public int getRva() 
    {
        return rva;
    }

    public void setRva(int rva) 
    {
        this.rva = rva;
    }
    
    
}
