using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Cajovna.DAO
{
    interface PolozkaUctuDAO
    {
        void create(PolozkaUctu polozkaUctu);
        PolozkaUctu read(int id);
        void update(PolozkaUctu polozkaUctu);
        void delete(PolozkaUctu polozkaUctu);
        List<PolozkaUctu> readAll();
    }
}
