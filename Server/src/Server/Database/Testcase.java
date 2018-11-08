package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;


public class Testcase 
{
    static public Integer insert(Connection connection, Integer projectId, String url, String hash, Integer size)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO testcase(project_id, url, hash, size) VALUES(?, ?, ?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, projectId);
            stmt.setString(2, url);
            stmt.setString(3, hash);
            stmt.setInt(4, size);
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
    
    static public Integer getNewTestcaseId(Connection connection, Integer projectId)
    {
        try
        {
            while(true)
            {
                PreparedStatement stmt = connection.prepareStatement("SELECT * FROM testcase where project_id = ? AND started IS NULL");
                stmt.setInt(1, projectId);
                ResultSet rs = stmt.executeQuery();
                if(!rs.next())
                    return null;
                stmt = connection.prepareStatement("UPDATE testcase SET started = now() where id = ? AND started IS NULL");
                stmt.setInt(1, rs.getInt("id"));
                if(stmt.executeUpdate() > 0)
                    return rs.getInt("id");
            }
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
    
    static public Integer getProjectIdFromTestcaseId(Connection connection, Integer testcaseId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT project_id FROM testcase where id = ?");
            stmt.setInt(1, testcaseId);
            ResultSet rs = stmt.executeQuery();
            if(!rs.next())
               return null;
            return rs.getInt("project_id");
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
    
    static public boolean setTestcaseFinished(Connection connection, Integer testcaseId, Integer modulesCount, Integer basicblockCount)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE testcase SET ended = now(), module_count = ?, basicblock_count = ? where id = ?");
            stmt.setInt(1, modulesCount);
            stmt.setInt(2, basicblockCount);
            stmt.setInt(3, testcaseId);
            if(stmt.executeUpdate() > 0)
                return true;
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return false;
    }
}
