package Server.Database;

import Server.Conf;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Database 
{
    Conf conf = new Conf();
    
    public Database(Conf confIn)
    {
        conf = confIn;
    }
    
    public Connection connect() throws SQLException
    {
        Connection con = DriverManager
                    .getConnection("jdbc:mysql://" + conf.getProperty("DB_HOST")
                            + "/" + conf.getProperty("DB_SCHEMA") + "?"
                            + "user=" + conf.getProperty("DB_USERNAME")
                            + "&password=" + conf.getProperty("DB_PASSWORD")
                            + "&rewriteBatchedStatements=true"
                            + "&useSSL=false");
        con.setAutoCommit(true);
        return con;        
    }    
}
