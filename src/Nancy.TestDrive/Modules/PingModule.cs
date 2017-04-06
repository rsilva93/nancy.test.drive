namespace Nancy.TestDrive.Modules
{
    public class PingModule : NancyModule
    {
        public PingModule()
        {
            Get("/api/ping", x => "pong");
            Get("/api/echo/{value}", x => x.value);
        }
    }
}
