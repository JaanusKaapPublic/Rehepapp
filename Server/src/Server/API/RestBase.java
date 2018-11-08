package Server.API;

import Server.Conf;
import Server.Database.Database;
import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.json.JSONObject;
import org.json.JSONArray;

public class RestBase implements HttpHandler 
{
    String uriApiBase = "";
    String uriClassBase = "";
    Conf conf;
    Database database;
    
    public RestBase(Database databaseIn, Conf configuration)
    {
        uriApiBase = "/API/";        
        database = databaseIn;
        conf = configuration;
    }    
    
    @Override
    public void handle(HttpExchange req) throws IOException 
    {
        Scanner uriScanner;
        Map<String, String> getParams = null;
        String response = "Could not handle it!";
        
        try 
        {
            if (!req.getRequestURI().getPath().startsWith(uriApiBase + uriClassBase)) 
                return;
            
            uriScanner = new Scanner(req.getRequestURI().getPath().replaceFirst(uriApiBase + uriClassBase, ""));
            uriScanner.useDelimiter("/");

            Connection con = database.connect();
            switch (req.getRequestMethod()) 
            {
                case "GET":
                    if (uriScanner.hasNextInt())
                        response = handleGetId(con, req, uriScanner.nextInt());
                    else if (uriScanner.hasNext())
                        response = handleGetCustom(con, req, uriScanner.next());
                    else
                        response = handleGet(con, req);
                    break;
                case "POST":
                    if (uriScanner.hasNextInt())
                        response = handlePostId(con, req, uriScanner.nextInt());
                    else if (uriScanner.hasNext())
                        response = handlePostCustom(con, req, uriScanner.next());
                    else
                        response = handlePost(con, req);
                    break;
                case "DELETE":
                    if (uriScanner.hasNextInt()) 
                        response = handleDelId(con, req, uriScanner.nextInt());
                    else if (uriScanner.hasNext())
                        response = handleDelCustom(con, req, uriScanner.next());
                    else
                        response = handleDel(con, req);
                    break;
            }

            if (response != null) 
            {
                req.sendResponseHeaders(200, response.length());
                OutputStream os = req.getResponseBody();
                os.write(response.getBytes());
                os.close();
            }
            con.close();
        } catch (Exception e) 
        {
            e.printStackTrace();
        }
    }
    
    protected String handleGetId(Connection con, HttpExchange req, int id)
    {
        return "'handleGetId' not implemented for " + this.getClass().toString();
    }
    
    protected String handleGetCustom(Connection con, HttpExchange req, String op)
    {
        return "'handleGetCustom' not implemented for " + this.getClass().toString();
    }
    
    protected String handleGet(Connection con, HttpExchange req)
    {
        return "'handleGet' not implemented for " + this.getClass().toString();
    }
    
    protected String handlePostId(Connection con, HttpExchange req, int id)
    {
        return "'handlePostId' not implemented for " + this.getClass().toString();
    }
    
    protected String handlePostCustom(Connection con, HttpExchange req, String op)
    {
        return "'handlePostCustom' not implemented for " + this.getClass().toString();
    }
    
    protected String handlePost(Connection con, HttpExchange req)
    {
        return "'handlePost' not implemented for " + this.getClass().toString();
    }
    
    protected String handleDelId(Connection con, HttpExchange req, int id)
    {
        return "'handleDelId' not implemented for " + this.getClass().toString();
    }
    
    protected String handleDelCustom(Connection con, HttpExchange req, String op)
    {
        return "'handleDelCustom' not implemented for " + this.getClass().toString();
    }
    
    protected String handleDel(Connection con, HttpExchange req)
    {
        return "'handleDel' not implemented for " + this.getClass().toString();
    }
   
    protected String getStandardGetJSON(Connection con, String tableName, int id)
    {        
        try {
            PreparedStatement stmt = con.prepareStatement("SELECT * from " + tableName + " WHERE id = ?");
            stmt.setInt(1, id);
            ResultSet rs = stmt.executeQuery();
            ResultSetMetaData rsmd = rs.getMetaData();            
                        
            JSONObject ret = new JSONObject();
            if(rs.next())
            {
                for(int x=1; x<=rsmd.getColumnCount(); x++)
                {
                    if(rsmd.getColumnType(x) == Types.INTEGER)
                        ret.put(rsmd.getColumnName(x), rs.getInt(x));
                    else
                        ret.put(rsmd.getColumnName(x), rs.getString(x));
                }
            }                        
            JSONObject retOuter = new JSONObject();
            retOuter.put("error", 0);
            retOuter.put("data", ret);
            return retOuter.toString();
        } catch (SQLException e) {
            return getErrorJSON("Problem with database connection", e.getLocalizedMessage());
        }
    }
    
    protected String getStandardGetParamsJSON(Connection con, String elements, String tableName, String query, Object[] params)
    {        
        try {
            PreparedStatement stmt = con.prepareStatement("SELECT " + elements + " from " + tableName + " " + query);
            for(int x=0; x<params.length; x++)
            {
                if(params[x] instanceof Integer)
                    stmt.setInt(x+1, (Integer)params[x]);
                if(params[x] instanceof String)
                    stmt.setString(x+1, (String)params[x]);
            }
            ResultSet rs = stmt.executeQuery();
            ResultSetMetaData rsmd = rs.getMetaData();            
                  
            JSONArray ret = new JSONArray();
            while(rs.next())
            {
                JSONObject element = new JSONObject();
                for(int x=1; x<=rsmd.getColumnCount(); x++)
                {
                    if(rsmd.getColumnType(x) == Types.INTEGER)
                        element.put(rsmd.getColumnName(x), rs.getInt(x));
                    else
                        element.put(rsmd.getColumnName(x), rs.getString(x));
                }
                ret.put(element);
            }  
            JSONObject retOuter = new JSONObject();
            retOuter.put("error", 0);
            retOuter.put("data", ret);
            return retOuter.toString();
        } catch (SQLException e) {
            return getErrorJSON("Problem with database connection", e.getLocalizedMessage());
        }
    }
    
    protected String getErrorJSON(String msg, String dbgData)
    {
        JSONObject ret = new JSONObject();
        ret.put("error", 1);
        ret.put("message", msg);
        if(conf.get("DEBUG").equals("1"))
            ret.put("debug", dbgData);
        return ret.toString();
    }    
    
    protected String getOkJSON(String data)
    {
        JSONObject ret = new JSONObject();
        ret.put("error", 0);
        ret.put("data", data);
        return ret.toString();
    }  
}
