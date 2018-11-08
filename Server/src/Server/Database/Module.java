package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;


public class Module 
{
    static public Integer insert(Connection connection, Integer projectId, String name)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO module(project_id, name) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, projectId);
            stmt.setString(2, name);
            stmt.executeUpdate();
            ResultSet rs = stmt.getGeneratedKeys();
            if(rs.next()) 
                return rs.getInt(1);
        }
        catch(SQLException e)
        {
        }
        catch(Exception e)
        {
            
            e.printStackTrace();
        }
        return null;
    }
    
    static public Integer getModuleId(Connection connection, Integer projectId, String name)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT id FROM module where project_id = ? AND name = ?");
            stmt.setInt(1, projectId);
            stmt.setString(2, name);
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
                return rs.getInt(1);
        }
        catch(SQLException e)
        {
            e.printStackTrace();
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return null;
    }
    
    static public Integer insertConnection(Connection connection, Integer testcaseId, Integer moduleId)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO testcase_module(testcase_id, module_id) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, testcaseId);
            stmt.setInt(2, moduleId);
            stmt.executeUpdate();
            ResultSet rs = stmt.getGeneratedKeys();
            if(rs.next()) 
                return rs.getInt(1);
        }
        catch(SQLException e)
        {
        }
        catch(Exception e)
        {
            
            e.printStackTrace();
        }
        return null;
    }
}
