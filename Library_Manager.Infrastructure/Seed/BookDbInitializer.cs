using Bogus;
using Library_Manager.Domain.Models;
using Library_Manager.Infrastructure.Data;

namespace Library_Manager.Infrastructure.Seed
{
    public class BookDbInitializer
    {
        public static void Seed(LibraryContext context)
        {
            if (!context.Books.Any())
            {
                var authorIds = context.Authors.Select(a => a.Id).ToList();
                var booksFaker = new Faker<Book>()
                    .RuleFor(b => b.Title, f => f.Lorem.Sentence(4))
                    .RuleFor(b => b.PublishedYear, f => f.Date.Past(200, DateTime.Now).Year)
                    .RuleFor(b => b.AuthorId, f => f.PickRandom(authorIds));

                var generatedFakeBooks = booksFaker.Generate(20);

                context.Books.AddRange(generatedFakeBooks);
                context.SaveChanges();
            }
        }
    }
}
