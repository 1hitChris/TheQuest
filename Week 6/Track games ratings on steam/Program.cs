using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Track_games_ratings_on_steam
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the html source
            var httpClient = new HttpClient();
            string[] steamLink = { "https://store.steampowered.com/app/413150/Stardew_Valley/", "https://store.steampowered.com/app/751780/Forager/", "https://store.steampowered.com/app/566540/Labyrinth_of_Refrain_Coven_of_Dusk/"};

            foreach (var link in steamLink)
            {
                string htmlCode = httpClient.GetStringAsync(link).Result;

                //Take out the name of the game
                string namePattern = @"<title>(.+) on Steam<\/title>";
                Match names = Regex.Match(htmlCode, namePattern);
                string gameName = names.Groups[1].ToString().ToUpper();

                //Take out all the reviews
                string allReviewPattern = @"<span class=""game_review_summary \w+"" itemprop=""description"">(\w+ ?\w+)<\/span>";
                Match allReviews = Regex.Match(htmlCode, allReviewPattern);
                string allReview = allReviews.Groups[1].ToString();

                //Take out the recent reviews
                string recentReviewPattern = @"<span class=""game_review_summary \w+"">(\w+ ?\w+)<\/span>";
                Match recentReviews = Regex.Match(htmlCode, recentReviewPattern);
                string recentReview = recentReviews.Groups[1].ToString();

                Console.WriteLine(gameName);

                if (allReview == "")
                    Console.WriteLine($"There are no reviews available.");

                else if (allReview == recentReview)
                {
                    Console.WriteLine($"All reviews: {allReview}.");
                }

                else
                {
                    Console.WriteLine($"Recent reviews: {recentReview}.");
                    Console.WriteLine($"All reviews: {allReview}.");
                }

                Console.WriteLine();
            }
        }
    }
}
