package Server.API;

import Caches.BasicblockCache;
import Caches.ModuleCache;
import Caches.ProjectCache;
import Caches.ProjectsCache;
import Server.Conf;
import Server.Database.*;
import Server.MyUtils;
import com.sun.net.httpserver.HttpExchange;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.Iterator;
import java.util.Map;
import java.util.Vector;
import org.json.JSONArray;
import org.json.JSONObject;


public class Coverage  extends RestBase
{
    public static final String myClassBase = "Coverage";
    
    public Coverage(Database database, Conf configuration)
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
            case "getTestcase":
                return getTestcase(con, getParams);
        }
        return "Bad operation: " + op;
    }
    
    @Override
    protected String handlePostCustom(Connection con, HttpExchange req, String op)
    {
        switch(op)
        {
            case "addCoverage":
                return addCoverage(con, MyUtils.getEntirePostFromHttp(req));
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
                }while(BotCoverage.getBot(con, name) != null);
            }while(BotCoverage.insert(con, name, Project.getDefaultId(con)) == null);
            
            
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
                        
            rs = BotCoverage.getBot(con, getParams.get("code"));
            if(rs != null)
            {
                if(project_id == null || project_id == rs.getInt("project_id"))
                    return getStandardGetJSON(con, "bot_collect", rs.getInt(1));
                else
                    bot_id = rs.getInt("id");
            }
            
            if(bot_id == null)
            {
                bot_id = BotCoverage.insert(con, getParams.get("code"), project_id);
                if(bot_id != null)
                    return getStandardGetJSON(con, "bot_collect", bot_id);
                else
                    return getErrorJSON("Adding bot failed", "");
            }
            else
            {
                BotCoverage.update(con, bot_id, project_id);
                return getStandardGetJSON(con, "bot_collect", bot_id);
            }
        }   
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting bot_collect failed", e.getLocalizedMessage());
        }
    }
    
    protected String getTestcase(Connection con, Map<String, String> getParams)
    {
        try
        {
            Integer testcaseId = null;
            
            if(!getParams.containsKey("code"))
                return getErrorJSON("Asking testcase data failed", "No code parameter");
            
            if(!getParams.containsKey("project"))
                return getErrorJSON("Asking testcase data failed", "No project parameter");
           
            BotCoverage.setLastConnect(con, getParams.get("code"));
            testcaseId = Testcase.getNewTestcaseId(con, Project.getId(con, getParams.get("project")));
            if(testcaseId == null)
                return getErrorJSON("No new testcases", "");
                           
            return getStandardGetJSON(con, "testcase", testcaseId);
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Asking testcase data failed", e.getLocalizedMessage());
        }
    }    
    
    protected String addCoverage(Connection con, String data)
    {
        try
        {
            Vector<Integer> basicBlocksToAdd = new Vector<Integer>();
            Vector<Integer> basicBlocksToConnect = new Vector<Integer>();
            int testcaseCount = 0;
            JSONObject dataObj = new JSONObject(data);
            Integer testcaseId = dataObj.getInt("id");
            int projectId = Testcase.getProjectIdFromTestcaseId(con, testcaseId);
            ProjectCache projectObj = ProjectsCache.getProject(con, projectId);
            JSONArray modules = dataObj.getJSONArray("coverage");
            
            for(int x=0; x<modules.length(); x++)
            {
                JSONObject module = modules.getJSONObject(x);                
                ModuleCache moduleObj = projectObj.getOrCreateModule(con, module.getString("name"));   
                synchronized(moduleObj)
                {
                    Module.insertConnection(con, testcaseId, moduleObj.getId());
                
                    JSONArray basicblocks = module.getJSONArray("basicblocks");
                    basicBlocksToAdd.clear();
                    basicBlocksToConnect.clear();
                    for (int y=0; y<basicblocks.length(); y++)
                    {
                        int rva = basicblocks.getInt(y);
                        BasicblockCache bb = moduleObj.getBasicblock(con, rva);
                        if(bb == null)
                            basicBlocksToAdd.add(rva);
                        else
                            basicBlocksToConnect.add(bb.getId());
                    }            
                    if(basicBlocksToAdd.size() > 0)
                    {
                        basicBlocksToAdd = Basicblock.inserts(con, moduleObj.getId(), basicBlocksToAdd);
                        Iterator<Integer> i = basicBlocksToAdd.iterator();
                        while (i.hasNext())
                            basicBlocksToConnect.add(i.next());
                        moduleObj.loadBasicblocks(con);
                    }      
                    if(basicBlocksToConnect.size() > 0)
                        Basicblock.insertConnections(con, testcaseId, basicBlocksToConnect);
                }
            }
            
            BotCoverage.setLastConnect(con, dataObj.getString("client"));
            Testcase.setTestcaseFinished(con, testcaseId, modules.length(), testcaseCount);
            Project.incrementCoverageCount(con, projectId);
            return getOkJSON("Ok");
        }
        catch(Exception e)
        {
            e.printStackTrace();
            return getErrorJSON("Inserting file failed", e.getLocalizedMessage());
        }
    }
}
