package Server;

import com.sun.net.httpserver.HttpExchange;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.Map;

public class MyUtils 
{
    static public Integer parseInt(String input, int defaultValue)
    {
        try
        {
            return Integer.parseInt(input);
        }
        catch(Exception e)
        {
            return defaultValue;
        }
    }
    
    static public String getEntirePostFromHttp(HttpExchange req)
    {
        InputStreamReader isr = null;
        BufferedReader br = null;
        try
        {
            isr = new InputStreamReader(req.getRequestBody(),"utf-8");
            br = new BufferedReader(isr);
            int b;
            StringBuilder buf = new StringBuilder(512);
            while ((b = br.read()) != -1) {
                buf.append((char) b);
            }
            br.close();
            isr.close();
            return buf.toString();
        }catch(IOException e)
        {
        }
        finally
        {
            try
            {
                if(isr != null)
                    isr.close();
            }
            catch(Exception e)
            {
            }
            try
            {
                if(isr != null)
                    isr.close();
            }
            catch(Exception e)
            {
            }
        }
        
        return null;
    }
    
    static public Map<String, String> queryToMap(String query) 
    {
        Map<String, String> result = new HashMap<>();
        if(query == null)
            return result;
        for (String param : query.split("&")) 
        {
            String[] entry = param.split("=");
            if (entry.length > 1) {
                result.put(entry[0], java.net.URLDecoder.decode(entry[1]));
            }else
            {
                result.put(entry[0], "");
            }
        }
        return result;
    }
    
    static public String generateNextSearchString(String searchStr)
    {
        for (int x = searchStr.length() - 1; x >= -1; x--) 
        {
            if (x == -1) 
            {
                int strLen = searchStr.length();
                searchStr = "A";
                for (int y = 0; y < strLen; y++) 
                {
                    searchStr += "A";
                }
                break;
            }
            if (searchStr.charAt(x) != 'Z') 
            {
                searchStr = searchStr.substring(0, x) + (char) (searchStr.charAt(x) + 1) + searchStr.substring(x + 1);
                break;
            }
            else 
            {
                searchStr = searchStr.substring(0, x) + 'A' + searchStr.substring(x + 1);
            }
        }
        return searchStr;
    }
}
