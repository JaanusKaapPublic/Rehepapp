package CLI;

import Caches.ProjectsCache;
import Server.Server;
import Server.Database.Project;
import java.sql.Connection;

public class CliProject implements CliCommand
{
    Server server;
            
    CliProject(Server serverIn)
    {
        server = serverIn;
    }
    
    @Override
    public String handle(String input) throws Exception
    {
        String cmdType = input.split(" ", 2)[0];
        if(cmdType.equals("add-project"))
            return handleAdd(input);
        else if(cmdType.equals("activate"))
            return handleActivate(input, true);
        else if(cmdType.equals("deactivate"))
            return handleActivate(input, false);
        else if(cmdType.equals("default"))
            return handleDefault(input);
        return "WTF?";
    }
        
        
    public String handleAdd(String input) throws Exception
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
    
    public String handleActivate(String input, boolean activated) throws Exception
    {
        String[] parts = input.split(" ");
        if(parts.length != 2)
            return "Arguments needed: " + parts[0] + " {Project code}";
        
        Connection con = server.getDBconnection();
        Integer id = Project.getId(con, parts[1]);
        
        if(id == null)
            return "Did not find project";
        
        if(Project.setActive(con, id, (activated ? 1 : 0)))
            return "Project activated";
        return "Operation failed";
    }
    
    public String handleDefault(String input) throws Exception
    {
        String[] parts = input.split(" ");
        if(parts.length != 2)
            return "Arguments needed: " + parts[0] + " {Project code}";
        
        Connection con = server.getDBconnection();
        Integer id = Project.getId(con, parts[1]);
        
        if(id == null)
            return "Did not find project";
        
        if(Project.setDefault(con, id))
            return "Project set as default";
        return "Operation failed";
    }
}
