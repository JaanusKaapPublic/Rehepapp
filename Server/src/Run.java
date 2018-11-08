import CLI.CLI;
import Server.Server;

public class Run 
{    
    public static void main(String[] args) throws Exception 
    {
        Server main = new Server();
        if(main.setup())
            main.start();
        CLI cli = new CLI(main);
        cli.start();
    }
}