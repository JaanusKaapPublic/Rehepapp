package Caches;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.concurrent.ConcurrentHashMap;

public class ProjectCache 
{    
    private Integer id;
    private String code;    
    private ConcurrentHashMap<String, ModuleCache> modules = new ConcurrentHashMap<String, ModuleCache>();
   
    public ProjectCache(Integer id, String code)
    {
        this.id = id;
        this.code = code;
    }
    
    public void loadModules(Connection connection) throws SQLException
    {
        PreparedStatement stmt = connection.prepareStatement("SELECT id, name FROM module WHERE project_id = ?");
        stmt.setInt(1, id);
        ResultSet rs = stmt.executeQuery();
        while(rs.next())
        {
            ModuleCache module = new ModuleCache(this, rs.getInt(1), rs.getString(2));
            module.loadBasicblocks(connection);
            modules.put(module.getName(), module);
        }
    }
    
    public ModuleCache getOrCreateModule(Connection connection, String name) throws SQLException
    {
        if(modules.containsKey(name))
            return modules.get(name);
        synchronized(this) 
        {
            if(modules.containsKey(name))
                return modules.get(name);
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO module(project_id, name) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, id);
            stmt.setString(2, name);
            stmt.executeUpdate();
            ResultSet rs = stmt.getGeneratedKeys();
            if(rs.next()) 
            {
                ModuleCache module = new ModuleCache(this, rs.getInt(1), name);
                modules.put(module.getName(), module);
                return module;
            }
        }
        return null;
    }

    public Integer getId() 
    {
        return id;
    }

    public void setId(Integer id) 
    {
        this.id = id;
    }

    public String getCode() 
    {
        return code;
    }

    public void setCode(String code) 
    {
        this.code = code;
    }
}
