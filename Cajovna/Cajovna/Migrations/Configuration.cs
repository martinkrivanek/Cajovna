namespace Cajovna.Migrations
{
    using Cajovna.Models;
    using System;
    using System.Collections.Generic;
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
            #region suroviny
            Surovina sur_pistacie = new Surovina
            {
                name = "Pistáciové oøíšky",
                desription = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí.",
                unit = Unit.g,
                number_of_units = 100,
                price = 30
            };

            Surovina sur_cukr = new Surovina
            {
                name = "Cukr krystal",
                desription = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí.",
                unit = Unit.g,
                number_of_units = 1,
                price = 0.1
            };

            Surovina sur_caj_poustniDuna = new Surovina
            {
                name = "Èaj pouštní duna",
                desription = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí.",
                unit = Unit.g,
                number_of_units = 1,
                price = 1
            };

            Surovina sur_arasidy = new Surovina
            {
                name = "Arašídy solené",
                desription = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí.",
                unit = Unit.g,
                number_of_units = 100,
                price = 20
            };

            Surovina sur_voda = new Surovina
            {
                name = "Voda",
                desription = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí.",
                unit = Unit.l,
                number_of_units = 1,
                price = 1
            };

            List<Surovina> suroviny = new List<Surovina>{
                sur_pistacie,
                sur_arasidy,
                sur_voda,
                sur_cukr,
                sur_caj_poustniDuna
            };
            suroviny.ForEach(a => context.Suroviny.Add(a));
            context.SaveChanges();
            #endregion

            #region PolozkyMenu
            PolozkaMenu polMenu_pistacie = new PolozkaMenu 
            {
                name = "Pistácie",
                price_sell = 35,
                avalible = true,
                description = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí."
            };

            PolozkaMenu polMenu_arasidy = new PolozkaMenu
            {
                name = "Arašídy",
                price_sell = 25,
                avalible = true,
                description = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí."
            };

            PolozkaMenu polMenu_poustDuna = new PolozkaMenu
            {
                name = "Èaj - Pouštní Duna",
                price_sell = 25,
                avalible = true,
                description = "Nìkdo zooloka u mj. daøí ledu oba sem, u pán. Vızkumného migrují pojmenování øady citoval napíná váš. Bylo tedy prùlomovım a ivotních spuštìní o obdobu otiskli vlastnì náplní zájem u souèástí."
            };

            context.PolozkyMenu.AddOrUpdate(
                s => s.name,
                polMenu_pistacie,
                polMenu_arasidy,
                polMenu_poustDuna
                );
            List<PolozkaMenu> polMenu = new List<PolozkaMenu>{
                polMenu_pistacie,
                polMenu_arasidy,
                polMenu_poustDuna
            };
            polMenu.ForEach(a => context.PolozkyMenu.Add(a));
            context.SaveChanges();
            #endregion

            #region Slozeni
            Slozeni slo_polMenu_pistacie = new Slozeni
            {
                surovina = sur_pistacie,
                quantity = 1,
                polozkaMenu = polMenu_pistacie
            };

            Slozeni slo_polMenu_arasidy = new Slozeni
            {
                surovina = sur_arasidy,
                quantity = 1,
                polozkaMenu = polMenu_arasidy
            };

            Slozeni slo_polMenu_poustDuna_cukr = new Slozeni
            {
                surovina = sur_cukr,
                quantity = 20,
                polozkaMenu = polMenu_poustDuna
            };

            Slozeni slo_polMenu_poustDuna_voda = new Slozeni
            {
                surovina = sur_voda,
                quantity = 1,
                polozkaMenu = polMenu_poustDuna
            };

            Slozeni slo_polMenu_poustDuna_caj = new Slozeni
            {
                surovina = sur_caj_poustniDuna,
                quantity = 20,
                polozkaMenu = polMenu_poustDuna
            };

            List<Slozeni> slozeni = new List<Slozeni>{
                slo_polMenu_pistacie,                
                slo_polMenu_arasidy,                
                slo_polMenu_poustDuna_cukr,
                slo_polMenu_poustDuna_voda,
                slo_polMenu_poustDuna_caj
            };
            slozeni.ForEach(a => context.Slozeni.Add(a));
            context.SaveChanges();
            #endregion

            #region stoly
            Stul stul_1 = new Stul { };
            Stul stul_2 = new Stul { };
            Stul stul_bar = new Stul { name = "Bar" };
            Stul stul_dluhy = new Stul { name = "Dluhy" };

            List<Stul> stoly = new List<Stul>{
                stul_1,
                stul_2,
                stul_bar,
                stul_dluhy
            };
            stoly.ForEach(a => context.Stoly.Add(a));
            context.SaveChanges();
            #endregion

            #region ucty
            Ucet ucet_bar_vlevo = new Ucet { name = "vlevo od pípy", stul = stul_bar };
            Ucet ucet_bar_vpravo = new Ucet { name = "vpravo od pípy", stul = stul_bar };
            Ucet ucet_1 = new Ucet { stul = stul_1 };
            Ucet ucet_2 = new Ucet { stul = stul_2 };
            Ucet ucet_dluhy_jarda = new Ucet { name = "Jarda Petarda", stul = stul_dluhy };

            List<Ucet> ucty = new List<Ucet>{
                ucet_bar_vlevo,
                ucet_bar_vpravo,
                ucet_1,
                ucet_2,
                ucet_dluhy_jarda
            };
            ucty.ForEach(a => context.Ucty.Add(a));
            context.SaveChanges();
            #endregion

            #region polozkyUctu
            PolozkaUctu polUctu_bar_1 = new PolozkaUctu { polozkaMenu = polMenu_pistacie, ucet = ucet_bar_vlevo };
            PolozkaUctu polUctu_bar_2 = new PolozkaUctu { polozkaMenu = polMenu_pistacie, ucet = ucet_bar_vpravo };
            PolozkaUctu polUctu_bar_3 = new PolozkaUctu { polozkaMenu = polMenu_arasidy, ucet = ucet_bar_vlevo };
            PolozkaUctu polUctu_bar_4 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_bar_vlevo };
            PolozkaUctu polUctu_bar_5 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_bar_vpravo };
            PolozkaUctu polUctu_bar_6 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_bar_vpravo };

            PolozkaUctu polUctu_1_1 = new PolozkaUctu { polozkaMenu = polMenu_pistacie, ucet = ucet_1 };
            PolozkaUctu polUctu_1_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_1 };
            PolozkaUctu polUctu_1_3 = new PolozkaUctu { polozkaMenu = polMenu_arasidy, ucet = ucet_1 };
            PolozkaUctu polUctu_1_4 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_1 };
            PolozkaUctu polUctu_1_5 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_1 };

            PolozkaUctu polUctu_2_1 = new PolozkaUctu { polozkaMenu = polMenu_arasidy, ucet = ucet_2 };
            PolozkaUctu polUctu_2_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_2 };

            PolozkaUctu polUctu_dluhy_1 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_dluhy_jarda };
            PolozkaUctu polUctu_dluhy_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna, ucet = ucet_dluhy_jarda };

            List<PolozkaUctu> polUctu = new List<PolozkaUctu>{
                polUctu_bar_1,
                polUctu_bar_2,
                polUctu_bar_3,
                polUctu_bar_4,
                polUctu_bar_5,
                polUctu_bar_6,

                polUctu_1_1,
                polUctu_1_2,
                polUctu_1_3,
                polUctu_1_4,
                polUctu_1_5,

                polUctu_2_1,
                polUctu_2_2,

                polUctu_dluhy_1,
                polUctu_dluhy_2
            };
            polUctu.ForEach(a => context.PolozkyUctu.Add(a));
            context.SaveChanges();
            #endregion
        }
    }
}
