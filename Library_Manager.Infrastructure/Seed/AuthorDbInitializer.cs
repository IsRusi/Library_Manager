using Bogus;
using Library_Manager.Domain.Models;
using Library_Manager.Infrastructure.Data;
namespace Library_Manager.Infrastructure.Seed
{
    public class AuthorDbInitializer
    {
        private const int PAST_YEARS_TO_DATE = 90;
        public AuthorDbInitializer()
        {
        }

        public static void Seed(LibraryContext context)
        {
            if(!context.Authors.Any())
            {
                var authorsFaker = new Faker<Author>()
                    .RuleFor(a=>a.Name,f=>f.Name.FullName())
                    .RuleFor(a=>a.DateOfBirth,f=>f.Date.Past(PAST_YEARS_TO_DATE, DateTime.Now));

                var generatedFakeAuthors= authorsFaker.Generate(20);

                context.Authors.AddRange(generatedFakeAuthors);
                context.SaveChanges();
            }
        }
    }
}
