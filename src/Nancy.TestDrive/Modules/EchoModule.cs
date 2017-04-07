namespace Nancy.TestDrive.Modules
{
    public class EchoModule : NancyModule
    {
        public EchoModule()
        {
            Get("/api/echo/{value}", x => x.value);
        }
    }
}
