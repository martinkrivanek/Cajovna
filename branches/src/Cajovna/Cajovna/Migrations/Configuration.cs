namespace Cajovna.Migrations
{
    using Cajovna.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cajovna.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cajovna.Models.ApplicationDbContext context)
        {
            Surovina sur_pistacie = new Surovina
            {
                name = "Pistáciové oøíšky",
                desription = "Nìkdo zooložka už mj. daøí ledu oba sem, u pán. Výzkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovým a životních spuštìní o obdobu otiskli vlastnì náplní zájem už souèástí.",
                unit = Unit.g,
                number_of_units = 100,
                price = 20
            };

            context.Suroviny.AddOrUpdate(
                s => s.name,
                sur_pistacie
                );
        }
    }
}
