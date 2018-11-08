package Caches;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.concurrent.ConcurrentHashMap;


public class ModuleCache
{
    ProjectCache parent;
    int id;
    String name;
    private ConcurrentHashMap<Integer, BasicblockCache> basicblocks = new ConcurrentHashMap<Integer, BasicblockCache>();
    
    ModuleCache(ProjectCache parent, int id, String name)
    {
        this.parent = parent;
        this.id = id;
        this.name = name;
    }
    
    public void loadBasicblocks(Connection connection) throws SQLException
    {
        basicblocks.clear();
        PreparedStatement stmt = connection.prepareStatement("SELECT id, rva FROM basicblock WHERE module_id = ?");
        stmt.setInt(1, id);
        ResultSet rs = stmt.executeQuery();
        while(rs.next())
        {
            BasicblockCache basicblock = new BasicblockCache(this, rs.getInt(1), rs.getInt(2));
            basicblocks.put(basicblock.getRva(), basicblock);
        }        
    }
    
    public BasicblockCache getBasicblock(Connection connection, int rva) throws SQLException
    {
        if(basicblocks.containsKey(rva))
            return basicblocks.get(rva);
        return null;
    }

    public ProjectCache getParent() 
    {
        return parent;
    }

    public void setParent(ProjectCache parent) 
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

    public String getName() 
    {
        return name;
    }

    public void setName(String name) 
    {
        this.name = name;
    }
    
    
}
