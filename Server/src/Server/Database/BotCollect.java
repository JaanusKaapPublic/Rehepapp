package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;


public class BotCollect 
{
    static public boolean setLastConnect(Connection connection, String code)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE bot_collect SET last_connect = now() WHERE code = ?");
            stmt.setString(1, code);
            if(stmt.executeUpdate() > 0)
                return true;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    }
    
    static public boolean setLastConnect(Connection connection, Integer id)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE bot_collect SET last_connect = now() WHERE id = ?");
            stmt.setInt(1, id);
            if(stmt.executeUpdate() > 0)
                return true;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    }
    
    static public ResultSet getBot(Connection connection, String code)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT id, project_id FROM bot_collect WHERE code = ?");
            stmt.setString(1, code);
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
                return rs;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }
    
    static public Integer insert(Connection connection, String code, Integer projectId)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO bot_collect(project_id, code) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            if(projectId == null)
                stmt.setString(1, null);
            else
                stmt.setInt(1, projectId);
            stmt.setString(2, code);
            stmt.executeUpdate();
            ResultSet rs = stmt.getGeneratedKeys();
            if(rs.next())
                return rs.getInt(1);
            else
                return null;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }
    
    static public boolean update(Connection connection, Integer botId, Integer projectId)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE bot_collect SET project_id = ? WHERE id = ?");
            stmt.setInt(1, projectId);
            stmt.setInt(2, botId);
            return (stmt.executeUpdate() > 0);
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    }
}
