package Server.Database;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;


public class CoverageCalc 
{
    static public boolean transferData(Connection connection, Integer projectId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("DELETE FROM coverage_calc_bb");
            stmt.executeUpdate();
            stmt = connection.prepareStatement("DELETE FROM coverage_calc_file");
            stmt.executeUpdate();
            stmt = connection.prepareStatement("INSERT INTO coverage_calc_file(testcase_id, bb_count, url) (SELECT id, basicblock_count, url FROM testcase WHERE project_id = ? AND basicblock_count IS NOT NULL)");
            stmt.setInt(1, projectId);
            if(stmt.executeUpdate() == 0)
                return false;            
            stmt = connection.prepareStatement("INSERT INTO coverage_calc_bb(coverage_calc_file_id, basicblock_id) (SELECT t.id, b.basicblock_id FROM testcase t, testcase_basicblock b WHERE t.id = b.testcase_id AND t.project_id = ? AND t.basicblock_count IS NOT NULL)");
            stmt.setInt(1, projectId);
            if(stmt.executeUpdate() == 0)
                return false;
            return true;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return false;
    }

    static public int getBestCoverageId(Connection con)
    {
        try
        {
            PreparedStatement stmt = con.prepareStatement("SELECT testcase_id FROM coverage_calc_file WHERE selected = 0 and bb_count > 0 ORDER BY bb_count DESC LIMIT 1");
            ResultSet rs = stmt.executeQuery();
            if(rs.next())
            {
                int id = rs.getInt("testcase_id");
                stmt = con.prepareStatement("UPDATE coverage_calc_file SET selected = 1 WHERE testcase_id = ?");
                stmt.setInt(1, id);
                stmt.executeUpdate();
                return id;
            }
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return 0;
    }
   
    static public List<Integer> getBestCoverageBlocks(Connection con, int testcaseId)
    {
        List<Integer> blocks = new ArrayList<Integer>();
        try
        {
            PreparedStatement stmt = con.prepareStatement("SELECT basicblock_id FROM coverage_calc_bb WHERE coverage_calc_file_id = ?");
            stmt.setInt(1, testcaseId);
            ResultSet rs = stmt.executeQuery();
            while(rs.next())
                blocks.add(rs.getInt("basicblock_id"));
            return blocks;
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
        return null;
    }    
    
    static public void removeBlocks(Connection connection, List<Integer> blocks)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("DELETE FROM coverage_calc_bb WHERE basicblock_id = ?");            
            Iterator<Integer> i = blocks.iterator();
            while (i.hasNext()) 
            {
                stmt.setInt(1, i.next());
                stmt.addBatch();
            }
            stmt.clearParameters();
            stmt.executeBatch();
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }
    
    static public void updateTestcases(Connection connection)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("UPDATE coverage_calc_file f SET f.bb_count = (SELECT count(1) FROM coverage_calc_bb b WHERE b.coverage_calc_file_id = f.testcase_id) WHERE selected = 0");
            stmt.executeUpdate();
            stmt = connection.prepareStatement("DELETE FROM coverage_calc_file WHERE bb_count = 0 AND selected = 0");
            stmt.executeUpdate();
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }
    
    static public String getUrl(Connection connection, Integer testcaseId)
    {
        try
        {
            PreparedStatement stmt = connection.prepareStatement("SELECT url FROM coverage_calc_file where testcase_id = ?");
            stmt.setInt(1, testcaseId);
            ResultSet rs = stmt.executeQuery();
            if(!rs.next())
               return null;
            return rs.getString("url");
        }
        catch(Exception e)
        {            
            e.printStackTrace();
        }
        return null;
    }
}
