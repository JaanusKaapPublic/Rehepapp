package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;


public class Search 
{
    static public boolean endSearch(Connection connection, String searchStr, int projectId)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE search SET ended = now() WHERE project_id = ? AND search_str = ?");
            stmt.setInt(1, projectId);
            stmt.setString(2, searchStr);
            if(stmt.executeUpdate() > 0)
                return true;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    }
    
    static public String getLatestStr(Connection connection, Integer projectId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT search_str FROM search WHERE project_id = ? ORDER BY id DESC LIMIT 1");
            stmt.setInt(1, projectId);
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
            {
                return rs.getString(1);
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }   
    
    static public Integer insert(Connection connection, Integer projectId, Integer botId, String searchStr)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO search(project_id, bot_id, search_str) VALUES(?, ?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, projectId);
            stmt.setInt(2, botId);
            stmt.setString(3, searchStr);
            stmt.executeUpdate();
            ResultSet rs = stmt.getGeneratedKeys();
            if(rs.next()) 
                return rs.getInt(1);
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }
    
    static public ResultSet getData(Connection connection, Integer searchId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT p.id as project_id, p.extension, s.search_str, p.magic FROM search s, project p WHERE p.id = s.project_id AND s.id = ?");
            stmt.setInt(1, searchId);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) 
                return rs;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }
    
    static public ResultSet getExistingSearch(Connection connection, Integer botId, Integer projectId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT p.id as project_id, p.extension, s.search_str, p.magic FROM search s, project p WHERE p.id = s.project_id AND p.id = ? AND s.bot_id = ? AND s.ended IS NULL");
            stmt.setInt(1, projectId);
            stmt.setInt(2, botId);
            ResultSet rs = stmt.executeQuery();
            if (rs.next()) 
                return rs;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }
}
