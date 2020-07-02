namespace ShopeeChat.WebApi.Data.Interfaces
{
    public interface ISwitchable
    {
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
    }
}