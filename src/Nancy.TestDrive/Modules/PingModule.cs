namespace Nancy.TestDrive.Modules
{
    public class PingModule : NancyModule
    {
        public PingModule()
        {
            Get("/api/ping", x => "pong");
        }
    }
}
