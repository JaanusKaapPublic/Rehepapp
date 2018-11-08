package Server;

import Server.Database.Database;
import Caches.ProjectCache;
import Caches.ProjectsCache;
import com.sun.net.httpserver.HttpServer;
import java.sql.DriverManager;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.sql.Connection;
import java.sql.SQLException;

import Server.API.*;
import java.util.concurrent.ConcurrentHashMap;

public class Server 
{
    Conf conf = new Conf();
    Database database = null;
    
    public Server()
    {
    }
    
    public boolean setup()
    {       
        try
        {
            conf.load();        
            database = new Database(conf);  
            ProjectsCache.loadProjects(database.connect());
            return true;
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return false;
        }
    }
    
    public void start()
    {        
        try {
            HttpServer server;
            server = HttpServer.create(new InetSocketAddress(8000), 0);
            server.createContext("/API/" + Collecter.myClassBase, new Collecter(database, conf));
            server.createContext("/API/" + Coverage.myClassBase, new Coverage(database, conf));
            server.setExecutor(null); 
            System.out.println("STARTING SERVER");
            server.start();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    public Connection getDBconnection() throws SQLException 
    {
        return database.connect();
    }
}