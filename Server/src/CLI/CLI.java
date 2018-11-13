package CLI;

import Server.Server;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.HashMap;

public class CLI extends Thread
{
    Server server;
    HashMap<String, CliCommand> commands;
    
    public CLI(Server serverIn)
    {
        server = serverIn;
        
        commands = new HashMap<String, CliCommand>();
        commands.put("help", new CliHelp());
        
        CliProject project = new CliProject(server);
        commands.put("add-project", project);
        commands.put("activate", project);
        commands.put("deactivate", project);
        commands.put("default", project);
                
        CliStats stats = new CliStats(server);
        commands.put("stats-proj", stats);
        commands.put("stats-bot", stats);
        
        commands.put("coverage", new CliCoverage(server));
    }
    
    public void run()
    {
        System.out.println("CLI started");
        BufferedReader lineReader=new BufferedReader(new InputStreamReader(System.in));
        while(true)
        {
            try
            {
                System.out.print(">");
                String cmd = lineReader.readLine();
                String cmdType = cmd.split(" ", 2)[0];
           
                if(commands.containsKey(cmdType))
                   System.out.println(commands.get(cmdType).handle(cmd));
                else
                    System.out.println("Unknown command");
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
        }
    }    
}
