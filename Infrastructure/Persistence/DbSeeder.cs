using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    /// <summary>
    /// Inserts a small set of sample activities when the table is empty.
    /// Does nothing if any activity already exists.
    /// </summary>
    /// <returns>The number of activities inserted, or 0 when seeding was skipped.</returns>
    public static async Task<int> SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.Activities.AnyAsync(cancellationToken))
        {
            return 0;
        }

        var today = DateTime.UtcNow.Date;

        var activities = new List<Activity>
        {
            new()
            {
                Id = "5f8b1c2a-9d3e-4a71-8c56-1e2f3a4b5c6d",
                Title = "Rooftop Photography Walk",
                Description = "Golden-hour walk across three rooftop viewpoints. Bring any camera, "
                    + "phones welcome. We finish with a short group review over coffee.",
                Category = "Photography",
                Date = today.AddDays(7).AddHours(17),
                MaxParticipants = 12,
                City = "London",
                Venue = "Southbank Centre, Belvedere Road",
                Latitude = 51.5055,
                Longitude = -0.1160
            },
            new()
            {
                Id = "a1c4e6b8-2f70-4d93-b5a1-7c8d9e0f1a2b",
                Title = "Saturday Trail Run",
                Description = "Relaxed 10K on mixed trails with two regroup points, so all paces "
                    + "are welcome. Expect mud after rain.",
                Category = "Sports",
                Date = today.AddDays(12).AddHours(8),
                MaxParticipants = 30,
                City = "Manchester",
                Venue = "Heaton Park, Main Gate",
                Latitude = 53.5405,
                Longitude = -2.2500
            },
            new()
            {
                Id = "b2d5f7c9-3a81-4e04-c6b2-8d9e0f1a2b3c",
                Title = "Intro to Neapolitan Pizza",
                Description = "Hands-on workshop covering dough hydration, cold fermentation and "
                    + "oven management. You take home two dough balls.",
                Category = "Culinary",
                Date = today.AddDays(18).AddHours(18).AddMinutes(30),
                MaxParticipants = 16,
                City = "Bristol",
                Venue = "The Cookery School, Wapping Wharf",
                Latitude = 51.4470,
                Longitude = -2.5990
            },
            new()
            {
                Id = "c3e6a8d0-4b92-4f15-d7c3-9e0f1a2b3c4d",
                Title = "Open Source Contribution Night",
                Description = "Pick an issue, pair with someone and open a pull request before you "
                    + "leave. Maintainers on hand for reviews. Laptop required.",
                Category = "Technology",
                Date = today.AddDays(25).AddHours(18),
                MaxParticipants = 40,
                City = "Edinburgh",
                Venue = "CodeBase, Argyle House",
                Latitude = 55.9464,
                Longitude = -3.1990
            },
            new()
            {
                Id = "d4f7b9e1-5ca3-4026-e8d4-0f1a2b3c4d5e",
                Title = "Coastal Sketching Day",
                Description = "A full day of outdoor drawing along the cliff path. Materials "
                    + "provided for beginners; bring waterproofs and a folding stool.",
                Category = "Art",
                Date = today.AddDays(33).AddHours(10),
                MaxParticipants = 20,
                City = "Brighton",
                Venue = "Brighton Marina, East Boardwalk",
                Latitude = 50.8109,
                Longitude = -0.1027
            }
        };

        await context.Activities.AddRangeAsync(activities, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return activities.Count;
    }
}
