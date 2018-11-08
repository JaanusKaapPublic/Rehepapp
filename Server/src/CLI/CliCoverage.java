package CLI;

import Caches.ProjectsCache;
import Server.Database.CoverageCalc;
import Server.Server;
import Server.Database.Project;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.util.List;

public class CliCoverage implements CliCommand
{
    Server server;
            
    CliCoverage(Server serverIn)
    {
        server = serverIn;
    }
    
    @Override
    public String handle(String input) throws Exception
    {
        Connection con = server.getDBconnection();
        String result = "";
        String outputDir = null;
        
        String[] parts = input.split(" ");
        if(parts.length < 2 || parts.length > 3)
            return "Arguments needed: " + parts[0] + " {Project ID} [Files output dir (optional)]";
        if(parts.length == 3)
            outputDir = parts[2];
        
        System.out.println("Transfreing data");
        CoverageCalc.transferData(con, Integer.parseInt(parts[1]));
        CoverageCalc.updateTestcases(con);
        
        while(true)
        {
            int id = CoverageCalc.getBestCoverageId(con);
            if(id == 0)
                break;
            System.out.println("Selected testcase with id " + id + "");
            List<Integer> bbs = CoverageCalc.getBestCoverageBlocks(con, id);
            System.out.println("  " + bbs.size() + " blocks, removing them");
            CoverageCalc.removeBlocks(con, bbs);
            System.out.println("  blocks removed, updating testcases");
            CoverageCalc.updateTestcases(con);
        }
        
        con.close();
        System.out.println("Minimizing finished");
        return result;
    }
}
