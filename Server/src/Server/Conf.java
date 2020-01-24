package Server;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class Conf extends Properties
{
    public Conf()
    {
        super();
        setDefault();
    }
    
    private void setDefault()
    {
        setProperty("DEBUG", "1");
        
        setProperty("DB_USERNAME", "root");
        setProperty("DB_PASSWORD", "s3cr3t");
        setProperty("DB_HOST", "127.0.0.1");
        setProperty("DB_SCHEMA", "rehepapp");
        
        setProperty("PASSWORD_FULL", "ThisIsSparta");
        setProperty("PASSWORD_MONITOR", "ABC");
        setProperty("PASSWORD_COLLECTOR", "111");
        setProperty("PASSWORD_COVERAGE", "222");
    }
    
    public void load()
    {
        InputStream input = null;
        try
        {
            input = new FileInputStream("rehepapp.properties");
            load(input);
        } catch(IOException e){
            System.out.println("[WARNING] Could not load data from rehepapp.properties file");
        } finally {
		if(input != null)
                {
                    try
                    {
                        input.close();
                    }
                    catch(IOException e)
                    {
                        e.printStackTrace();
                    }
                }
	}
    }
}
