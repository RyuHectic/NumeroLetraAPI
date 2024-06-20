//Create a folder called API_setup and add the below files to register the APIs dynamically during run time.
namespace NumeroLetraAPI.API_Setup;

public interface IWebApi
{
    void Register(WebApplication app);
}

public interface IWebApiAsync
{
    Task RegisterAsync(WebApplication app);
}
