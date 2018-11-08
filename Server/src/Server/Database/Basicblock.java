package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.Iterator;
import java.util.Vector;


public class Basicblock 
{
    static public Integer insert(Connection connection, Integer moduleId, Integer rva)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO basicblock(module_id, rva) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, moduleId);
            stmt.setInt(2, rva);
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
    
    static public Vector<Integer> inserts(Connection connection, Integer moduleId, Vector<Integer> Rvas)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO basicblock(module_id, rva) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            Iterator<Integer> i = Rvas.iterator();
            while (i.hasNext()) 
            {
                stmt.setInt(1, moduleId);
                stmt.setInt(2, i.next());
                stmt.addBatch();
            }
            stmt.clearParameters();
            stmt.executeBatch();
            
            ResultSet rs = stmt.getGeneratedKeys();            
            Vector<Integer> result = new Vector<Integer>();
            while(rs.next()) 
                result.add(rs.getInt(1));
            return result;
        }
        catch(Exception e)
        {
            
            e.printStackTrace();
        }
        return null;
    }
    
    static public Integer getBasicblockId(Connection connection, Integer projectId, Integer moduleId, Integer rva)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT id FROM basicblock where project_id = ? AND module_id = ? AND rva = ?");
            stmt.setInt(1, projectId);
            stmt.setInt(2, moduleId);
            stmt.setInt(3, rva);
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
    
    static public Integer insertConnection(Connection connection, Integer testcaseId, Integer basicblockId)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO testcase_basicblock(testcase_id, basicblock_id) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            stmt.setInt(1, testcaseId);
            stmt.setInt(2, basicblockId);
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
    
    
    static public Vector<Integer> insertConnections(Connection connection, Integer testcaseId, Vector<Integer> basicblockIds)
    {       
        try
        {
            PreparedStatement stmt = connection.prepareStatement("INSERT INTO testcase_basicblock(testcase_id, basicblock_id) VALUES(?, ?)", PreparedStatement.RETURN_GENERATED_KEYS);
            Iterator<Integer> i = basicblockIds.iterator();
            while (i.hasNext()) 
            {
                stmt.setInt(1, testcaseId);
                stmt.setInt(2, i.next());
                stmt.addBatch();
            }
            stmt.clearParameters();
            stmt.executeBatch();
            
            ResultSet rs = stmt.getGeneratedKeys();            
            Vector<Integer> result = new Vector<Integer>();
            while(rs.next()) 
                result.add(rs.getInt(1));
            return result;
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return null;
    }
}
