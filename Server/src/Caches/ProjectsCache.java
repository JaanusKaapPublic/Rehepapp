package Caches;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.concurrent.ConcurrentHashMap;


public class ProjectsCache 
{
    static private ConcurrentHashMap<Integer, ProjectCache> projectsById = new ConcurrentHashMap<Integer, ProjectCache>();
    static private ConcurrentHashMap<String, ProjectCache> projectsByCode = new ConcurrentHashMap<String, ProjectCache>();
    
    
    static public void loadProjects(Connection connection) throws SQLException
    {
        PreparedStatement stmt = connection.prepareStatement("SELECT id, code FROM project WHERE active = 1");
        ResultSet rs = stmt.executeQuery();
        while(rs.next())
        {
            ProjectCache project = new ProjectCache(rs.getInt(1), rs.getString(2));
            project.loadModules(connection);
            projectsByCode.put(project.getCode(), project);
            projectsById.put(project.getId(), project);
        }
    }
    
    static public ProjectCache getProject(Connection connection, String code) throws SQLException
    {
        if(projectsByCode.containsKey(code))
            return projectsByCode.get(code);
        
        PreparedStatement stmt = connection.prepareStatement("SELECT id, code FROM project WHERE code = ?");
        stmt.setString(1, code);
        ResultSet rs = stmt.executeQuery();
        if(rs.next())
        {
            ProjectCache project = new ProjectCache(rs.getInt(1), rs.getString(2));
            project.loadModules(connection);
            projectsByCode.put(project.getCode(), project);
            projectsById.put(project.getId(), project);
            return project;
        }
        return null;
    }
    
    static public ProjectCache getProject(Connection connection, int id) throws SQLException
    {
        if(projectsById.containsKey(id))
            return projectsById.get(id);
        
        PreparedStatement stmt = connection.prepareStatement("SELECT id, code FROM project WHERE id = ?");
        stmt.setInt(1, id);
        ResultSet rs = stmt.executeQuery();
        if(rs.next())
        {
            ProjectCache project = new ProjectCache(rs.getInt(1), rs.getString(2));
            project.loadModules(connection);
            projectsByCode.put(project.getCode(), project);
            projectsById.put(project.getId(), project);
            
            return project;
        }
        return null;
    }    
}
