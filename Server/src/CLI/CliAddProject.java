package CLI;

import Caches.ProjectsCache;
import Server.Server;
import Server.Database.Project;
import java.sql.Connection;

public class CliAddProject implements CliCommand
{
    Server server;
            
    CliAddProject(Server serverIn)
    {
        server = serverIn;
    }
    
    @Override
    public String handle(String input) throws Exception
    {
        String[] parts = input.split(" ");
        if(parts.length < 5)
            return "Arguments needed: " + parts[0] + " {Name} {Code} {Extension} {Magic number}";
        
        String name = parts[1];
        String code = parts[2];
        String ext = parts[3];
        String magic = parts[4];
        int isActive = 0;
        int isDefault = 0;
        
        for(int x = 5; x<parts.length; x++)
        {
            if(parts[x].equalsIgnoreCase("default"))
                isDefault = 1;
            if(parts[x].equalsIgnoreCase("active"))
                isActive = 1;
        }
        
        Connection con = server.getDBconnection();
        if(Project.create(con, name, code, ext, magic, isActive, isDefault))
        {
            ProjectsCache.loadProjects(con);
            con.close();
            return "New project created";
        }
        else
        {
            con.close();
            return "Failed";
        }
    }
}
