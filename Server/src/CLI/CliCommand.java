package CLI;

public interface CliCommand 
{
    public String handle(String input) throws Exception;
}
