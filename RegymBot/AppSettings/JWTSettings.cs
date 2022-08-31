namespace RegymBot.AppSettings
{
    public class JWTSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Lifetime { get; set; }
        public string Secret { get; set; }
    }
}