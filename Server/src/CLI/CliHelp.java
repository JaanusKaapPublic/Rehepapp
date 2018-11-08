package CLI;

public class CliHelp implements CliCommand
{
    @Override
    public String handle(String input)
    {
        return "HELP";
    }
}
