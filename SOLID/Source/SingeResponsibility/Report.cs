namespace Infotecs.SOLID.SingeResponsibility
{
    internal struct Report
    {
        public Report(string header, string body) : this()
        {
            Header = header;
            Body = body;
        }

        public string Header { get; private set; }
        public string Body { get; private set; }
    }
}