package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class Project 
{
    static public Integer getId(Connection connection, String code)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT id FROM project WHERE code = ?");
            stmt.setString(1, code);
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
                return rs.getInt(1);
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return null;
    } 
    
    static public Integer getDefaultId(Connection connection)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT id FROM project WHERE isdefault = 1");
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
                return rs.getInt(1);
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return null;
    }    
    
    static public boolean incrementTestcaseCount(Connection connection, Integer projectId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE Project SET testcase_count = testcase_count + 1 WHERE id = ?");
            stmt.setInt(1, projectId);
            return (stmt.executeUpdate() > 0);
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    } 
    
    static public boolean incrementCoverageCount(Connection connection, Integer projectId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE Project SET coverage_count = coverage_count + 1 WHERE id = ?");
            stmt.setInt(1, projectId);
            return (stmt.executeUpdate() > 0);
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    } 
            
    static public boolean create(Connection connection, String name, String code, String ext, String magic, int isActive, int isDefault)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO Project(name, code, extension, magic, active, isdefault) VALUES(?, ?, ?, ?, ?, ?)");
            stmt.setString(1, name);
            stmt.setString(2, code);
            stmt.setString(3, ext);
            stmt.setString(4, magic);
            stmt.setInt(5, isActive);
            stmt.setInt(6, isDefault);
            return (stmt.executeUpdate() > 0);
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    } 
}
