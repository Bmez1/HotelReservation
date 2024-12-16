namespace HotelReservation.Infraestructure.Configurations
{
    public class SmtpConfiguration
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
    }
}
