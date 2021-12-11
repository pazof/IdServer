namespace nuget_host.Entities
{
    public class SmtpSettings
    {
        public string Server {get; set;}
        public int Port {get; set;}
        public string SenderName {get; set;}
        public string SenderEMail {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public string ProtectionTitle {get; set;}
    }
}