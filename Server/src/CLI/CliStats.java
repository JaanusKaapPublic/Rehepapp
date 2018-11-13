package CLI;

import Server.Server;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class CliStats implements CliCommand
{
    Server server;
            
    CliStats(Server serverIn)
    {
        server = serverIn;
    }
    
    @Override
    public String handle(String input) throws Exception
    {
        String cmdType = input.split(" ", 2)[0];
        if(cmdType.equals("stats-proj"))
            return handleProject(input);
        else if(cmdType.equals("stats-bot"))
            return handleBot(input);
        return "WTF?";
    }
    
    public String handleProject(String input) throws Exception
    {
        Connection con = server.getDBconnection();
        String result = "";
        
        PreparedStatement stmt = con.prepareStatement("SELECT id, name, code, extension, magic, testcase_count, coverage_count, active, isdefault FROM project");
        ResultSet rs = stmt.executeQuery();
        while(rs.next())
        {
            result += "--- " + rs.getString("name") + " ---\n";
            result += "ID: " + rs.getInt("id") + "\n";
            result += "Code: " + rs.getString("code") + "\n";
            result += "Ext: " + rs.getString("extension") + "\n";
            result += "Magic: " + rs.getString("magic") + "\n";
            result += "Testcases: " + rs.getInt("testcase_count") + "\n";
            result += "Coverage done: " + rs.getInt("coverage_count") + "\n";
            if(rs.getInt("active") > 0)
                result += "ACTIVATED\n";
            if(rs.getInt("isdefault") > 0)
                result += "DEFAULT\n";
        }
        
        return result;
    }
    
    public String handleBot(String input) throws Exception
    {
        Connection con = server.getDBconnection();
        String result = "";
        
        PreparedStatement stmt = con.prepareStatement("SELECT p.code as project, b.code as bot, b.last_connect FROM bot_collect b, project p WHERE b.project_id = p.id ORDER by b.last_connect ASC");
        ResultSet rs = stmt.executeQuery();
        result += "--- Collector projects ---\n";
        while(rs.next())
        {
            result += rs.getString("bot") + " (project '" + rs.getString("project") + "'): " + rs.getString("last_connect") + "\n";
        }
        result += "\n";
        
        return result;
    }
}
