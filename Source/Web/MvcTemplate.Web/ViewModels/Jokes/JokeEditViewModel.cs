namespace MvcTemplate.Web.ViewModels.Jokes
{
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Web;

    public class JokeEditViewModel : IMapFrom<Joke>, IMapTo<Joke>
    {
        public int Id { get; set; }

        public string EncodedId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"{identifier.EncodeId(this.Id)}";
            }
        }

        public string Content { get; set; }
    }
}