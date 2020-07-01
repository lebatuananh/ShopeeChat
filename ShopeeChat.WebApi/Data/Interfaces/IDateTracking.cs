using System;

namespace ShopeeChat.WebApi.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreatedDate { get; set; }

        DateTime? LastUpdatedDate { get; set; }
    }
}