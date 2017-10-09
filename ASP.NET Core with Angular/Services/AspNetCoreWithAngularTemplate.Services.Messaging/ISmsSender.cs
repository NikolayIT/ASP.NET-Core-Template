namespace AspNetCoreWithAngularTemplate.Services.Messaging
{
    using System.Threading.Tasks;

    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
