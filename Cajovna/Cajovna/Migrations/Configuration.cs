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
                name = "Pist�ciov� o��ky",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                unit = Unit.g,
                number_of_units = 100,
                price = 30
            };

            Surovina sur_cukr = new Surovina
            {
                name = "Cukr krystal",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                unit = Unit.g,
                number_of_units = 1,
                price = 0.1
            };

            Surovina sur_caj_poustniDuna = new Surovina
            {
                name = "�aj pou�tn� duna",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                unit = Unit.g,
                number_of_units = 1,
                price = 1
            };

            Surovina sur_arasidy = new Surovina
            {
                name = "Ara��dy solen�",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                unit = Unit.g,
                number_of_units = 100,
                price = 20
            };

            Surovina sur_voda = new Surovina
            {
                name = "Voda",
                desription = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                unit = Unit.l,
                number_of_units = 1,
                price = 1
            };

            context.Suroviny.AddOrUpdate(
                s => s.name,
                sur_pistacie,
                sur_arasidy,
                sur_voda,
                sur_cukr,
                sur_caj_poustniDuna
                );
            #endregion

            #region stoly
            Stul stul_1 = new Stul { };
            Stul stul_2 = new Stul { };
            Stul stul_bar = new Stul{ name = "Bar"};
            Stul stul_dluhy = new Stul { name = "Dluhy" };

            context.Stoly.AddOrUpdate(
                s => s.name,
                stul_1,
                stul_2,
                stul_bar,
                stul_dluhy
                );
            #endregion

            #region PolozkyMenu + Slozeni
            #region Pistacie
            Slozeni slo_polMenu_pistacie = new Slozeni {
                surovina = sur_pistacie,
                quantity = 1
            };

            context.Slozeni.AddOrUpdate(
                slo_polMenu_pistacie                
                );

            PolozkaMenu polMenu_pistacie = new PolozkaMenu 
            {
                name = "Pist�cie",
                price_sell = 35,
                avalible = true,
                description = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                recipe = new List<Slozeni>{ slo_polMenu_pistacie }
            };
            #endregion

            #region Arasidy
            Slozeni slo_polMenu_arasidy = new Slozeni
            {
                surovina = sur_arasidy,
                quantity = 1
            };

            context.Slozeni.AddOrUpdate(
                slo_polMenu_arasidy
                );

            PolozkaMenu polMenu_arasidy = new PolozkaMenu
            {
                name = "Ara��dy",
                price_sell = 25,
                avalible = true,
                description = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                recipe = new List<Slozeni> { slo_polMenu_arasidy }
            };
            #endregion

            #region PoustniDuna
            Slozeni slo_polMenu_poustDuna_cukr = new Slozeni
            {
                surovina = sur_cukr,
                quantity = 20
            };

            Slozeni slo_polMenu_poustDuna_voda = new Slozeni
            {
                surovina = sur_voda,
                quantity = 1
            };

            Slozeni slo_polMenu_poustDuna_caj = new Slozeni
            {
                surovina = sur_caj_poustniDuna,
                quantity = 20
            };

            context.Slozeni.AddOrUpdate(
                slo_polMenu_poustDuna_cukr,
                slo_polMenu_poustDuna_voda,
                slo_polMenu_poustDuna_caj
                );

            PolozkaMenu polMenu_poustDuna = new PolozkaMenu
            {
                name = "�aj - Pou�tn� Duna",
                price_sell = 25,
                avalible = true,
                description = "N�kdo zoolo�ka u� mj. da�� ledu oba sem, u p�n. V�zkumn�ho migruj� pojmenov�n� �ady citoval nap�n� v�. Bylo tedy pr�lomov�m a� �ivotn�ch spu�t�n� o obdobu otiskli vlastn� n�pln� z�jem u� sou��st�.",
                recipe = new List<Slozeni> { slo_polMenu_poustDuna_cukr, slo_polMenu_poustDuna_voda, slo_polMenu_poustDuna_caj }
            };
            #endregion

            context.PolozkyMenu.AddOrUpdate(
                s => s.name,
                polMenu_pistacie,
                polMenu_arasidy,
                polMenu_poustDuna
                );
            #endregion

            #region ucty + polozkyUctu
            #region bar
            PolozkaUctu polUctu_bar_1 = new PolozkaUctu { polozkaMenu = polMenu_pistacie };
            PolozkaUctu polUctu_bar_2 = new PolozkaUctu { polozkaMenu = polMenu_pistacie };
            PolozkaUctu polUctu_bar_3 = new PolozkaUctu { polozkaMenu = polMenu_arasidy };
            PolozkaUctu polUctu_bar_4 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };
            PolozkaUctu polUctu_bar_5 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };
            PolozkaUctu polUctu_bar_6 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };

            context.PolozkyUctu.AddOrUpdate(
                polUctu_bar_1,
                polUctu_bar_2,
                polUctu_bar_3,
                polUctu_bar_4,
                polUctu_bar_5,
                polUctu_bar_6
                );

            Ucet ucet_bar_vlevo = new Ucet
            {
                name = "vlevo od p�py",
                stul = stul_bar,
                polozkyUctu = new List<PolozkaUctu> { polUctu_bar_1, polUctu_bar_3, polUctu_bar_4 }
            };

            Ucet ucet_bar_vpravo = new Ucet
            {
                name = "vpravo od p�py",
                stul = stul_bar,
                polozkyUctu = new List<PolozkaUctu> { polUctu_bar_2, polUctu_bar_5, polUctu_bar_6 }
            };
            #endregion

            #region stul1
            PolozkaUctu polUctu_1_1 = new PolozkaUctu { polozkaMenu = polMenu_pistacie };
            PolozkaUctu polUctu_1_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };
            PolozkaUctu polUctu_1_3 = new PolozkaUctu { polozkaMenu = polMenu_arasidy };
            PolozkaUctu polUctu_1_4 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };
            PolozkaUctu polUctu_1_5 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };

            context.PolozkyUctu.AddOrUpdate(
                polUctu_1_1,
                polUctu_1_2,
                polUctu_1_3,
                polUctu_1_4,
                polUctu_1_5
                );

            Ucet ucet_1 = new Ucet
            {
                stul = stul_1,
                polozkyUctu = new List<PolozkaUctu> { polUctu_1_1, polUctu_1_2, polUctu_1_3, polUctu_1_4, polUctu_1_5 }
            };
            #endregion

            #region stul2
            PolozkaUctu polUctu_2_1 = new PolozkaUctu { polozkaMenu = polMenu_arasidy };
            PolozkaUctu polUctu_2_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };

            context.PolozkyUctu.AddOrUpdate(
                polUctu_2_1,
                polUctu_2_2
                );

            Ucet ucet_2 = new Ucet
            {
                stul = stul_2,
                polozkyUctu = new List<PolozkaUctu> { polUctu_2_1, polUctu_2_2 }
            };
            #endregion

            #region dluhy
            PolozkaUctu polUctu_dluhy_1 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };
            PolozkaUctu polUctu_dluhy_2 = new PolozkaUctu { polozkaMenu = polMenu_poustDuna };

            context.PolozkyUctu.AddOrUpdate(
                polUctu_dluhy_1,
                polUctu_dluhy_2
                );

            Ucet ucet_dluhy_jarda = new Ucet
            {
                name = "Jarda Petarda",
                stul = stul_2,
                polozkyUctu = new List<PolozkaUctu> { polUctu_dluhy_1, polUctu_dluhy_2 }
            };
            #endregion

            context.Ucty.AddOrUpdate(
                ucet_bar_vlevo,
                ucet_bar_vpravo,
                ucet_1,
                ucet_2,
                ucet_dluhy_jarda
                );
            #endregion
        }
    }
}
