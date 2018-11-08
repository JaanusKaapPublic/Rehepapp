package Server.API;

import Server.Conf;
import Server.Database.*;
import Server.MyUtils;
import com.sun.net.httpserver.HttpExchange;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;
import org.json.JSONObject;


public class Collecter  extends RestBase
{
    public static final String myClassBase = "Collecter";
    
    public Collecter(Database database, Conf configuration)
    {
        super(database, configuration);
        uriClassBase = myClassBase;
    }
    
    @Override
    protected String handleGetCustom(Connection con, HttpExchange req, String op)
    {
        Map<String, String> getParams = MyUtils.queryToMap(req.getRequestURI().getRawQuery());
        switch(op)
        {
            case "getName":
                return getName(con);
            case "registerBot":
                return registerBot(con, getParams);
            case "getRange":
                return getSearch(con, getParams);
            case "endRange":
                return endSearch(con, getParams);
        }
        return "Bad operation: " + op;
    }
    
    @Override
    protected String handlePostCustom(Connection con, HttpExchange req, String op)
    {
        Map<String, String> postParams = MyUtils.queryToMap(MyUtils.getEntirePostFromHttp(req));
        switch(op)
        {
            case "addTestcase":
                return addTestcase(con, postParams);
        }
        return "Bad operation";
    }    
    
    protected String getName(Connection con)
    {
        try
        {
            int nr = 1;
            String name = null;
            
            do
            {
                do
                {
                    name = "CollectorBot_" + nr;
                    nr++;
                }while(BotCollect.getBot(con, name) != null);
            }while(BotCollect.insert(con, name, Project.getDefaultId(con)) == null);
            
            
            JSONObject retOuter = new JSONObject();
            retOuter.put("error", 0);
            retOuter.put("data", name);
            return retOuter.toString();
        }   
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting bot_collect failed", e.getLocalizedMessage());
        }
    }
    
    protected String registerBot(Connection con, Map<String, String> getParams)
    {
        try
        {
            ResultSet rs;
            Integer project_id = null, bot_id = null;
            
            if(!getParams.containsKey("code"))
                return getErrorJSON("Inserting bot_collect failed", "No code parameter");
            
            if(getParams.containsKey("project"))
                project_id = Project.getId(con, getParams.get("project"));
            else
                project_id = Project.getDefaultId(con);            
                        
            rs = BotCollect.getBot(con, getParams.get("code"));
            if(rs != null)
            {
                if(project_id == null || project_id == rs.getInt("project_id"))
                    return getStandardGetJSON(con, "bot_collect", rs.getInt(1));
                else
                    bot_id = rs.getInt("id");
            }
            
            if(bot_id == null)
            {
                bot_id = BotCollect.insert(con, getParams.get("code"), project_id);
                if(bot_id != null)
                    return getStandardGetJSON(con, "bot_collect", bot_id);
                else
                    return getErrorJSON("Adding bot failed", "");
            }
            else
            {
                BotCollect.update(con, bot_id, project_id);
                BotCollect.setLastConnect(con, bot_id);
                return getStandardGetJSON(con, "bot_collect", bot_id);
            }
        }   
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting bot_collect failed", e.getLocalizedMessage());
        }
    }
    
    protected String getSearch(Connection con, Map<String, String> getParams)
    {
        try
        {
            ResultSet rs;
            Integer projectId = null, botId = null, searchId = null;
            
            if(!getParams.containsKey("code"))
                return getErrorJSON("Asking search data failed", "No code parameter");
           
            rs = BotCollect.getBot(con, getParams.get("code"));
            if(rs != null)
            {
                if(rs.getInt("project_id") == 0)
                    return getErrorJSON("Bot does not have any project connected to it", "");
                botId = rs.getInt("id");
                projectId = rs.getInt("project_id");
                BotCollect.setLastConnect(con, botId);
            }
            else
            {
                return getErrorJSON("Unknown bot", "");
            }
            
            rs = Search.getExistingSearch(con, botId, projectId);
            if(rs == null)
            {
                while(true)
                {
                    String searchStr = Search.getLatestStr(con, projectId);
                    if(searchStr != null)
                        searchStr = MyUtils.generateNextSearchString(searchStr);
                    else
                        searchStr = "A";
                
                    searchId = Search.insert(con, projectId, botId, searchStr);
                    if (searchId != null) 
                    {
                        rs = Search.getData(con, searchId);
                        break;
                    }
                }
            }
           
            JSONObject ret = new JSONObject();
            ret.put("project_id", rs.getInt("project_id"));
            ret.put("search_str", rs.getString("search_str"));
            ret.put("extension", rs.getString("extension"));
            ret.put("magic", rs.getString("magic"));
            JSONObject retOuter = new JSONObject();
            retOuter.put("error", 0);
            retOuter.put("data", ret);
            return retOuter.toString();
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting bot_collect failed", e.getLocalizedMessage());
        }
    }    
    
    protected String addTestcase(Connection con, Map<String, String> postParams)
    {
        try
        {            
            if(postParams.containsKey("code"))
                BotCollect.setLastConnect(con, postParams.get("code"));
            Integer projectId = Integer.parseInt((String)postParams.get("project_id"));
            Integer testCaseId = Testcase.insert(con, projectId, 
                    postParams.get("url"), 
                    postParams.get("hash"), 
                    Integer.parseInt(postParams.get("size")));
            if (testCaseId != null) 
            {
                Project.incrementTestcaseCount(con, projectId);
                return getStandardGetJSON(con, "testcase", testCaseId);
            }
            return getErrorJSON("Inserting file failed", "");
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting file failed", e.getLocalizedMessage());
        }
    } 
    
    protected String endSearch(Connection con, Map<String, String> getParams)
    {
        try
        {
            BotCollect.setLastConnect(con, getParams.get("code"));
            Search.endSearch(con, getParams.get("search_str"), Integer.parseInt(getParams.get("project_id")));
        
            JSONObject retOuter = new JSONObject();
            retOuter.put("error", 0);
            return retOuter.toString();
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Ending search range failed", e.getLocalizedMessage());
        }
    }
}
