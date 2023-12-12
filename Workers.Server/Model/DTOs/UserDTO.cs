namespace Workers.Server.Model.DTOs
{
    public class UserDTO
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public string? Location { get; set; }

        public IList<string> Roles { get; set; }
    }
}
