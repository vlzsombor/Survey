using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Server.Model
{
    public class CardsSeeder
    {
        public static void Seed(SurveyDbContext context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.CardModel.Add(new CardModel()
            {
                Text="seeder1",
                Rating=7,
                Title="seeder Title apfelsaft ich mag lebenswurst"
            });
            
            context.CardModel.Add(new CardModel()
            {
                Text="seeder2",
                Rating=2,
                Title="öffnen sie bitte die tür"
            });

            context.SaveChanges();
        }

    }
}
