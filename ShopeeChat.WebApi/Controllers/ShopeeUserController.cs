using ShopeeChat.WebApi.Data;

namespace ShopeeChat.WebApi.Controllers
{
    public class ShopeeUserController : V1Controller
    {
        private readonly ShopeeChatDbContext _shopeeChatDbContext;

        public ShopeeUserController(ShopeeChatDbContext shopeeChatDbContext)
        {
            _shopeeChatDbContext = shopeeChatDbContext;
        }
    }
}