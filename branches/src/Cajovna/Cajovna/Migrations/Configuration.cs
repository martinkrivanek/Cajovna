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
                name = "Pist�ciov� o��ky",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
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
