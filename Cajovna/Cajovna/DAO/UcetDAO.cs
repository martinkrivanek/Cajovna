using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajovna.DAO
{
    interface UcetDAO
    {
        void create(Ucet ucet);
        Ucet read(int id);
        void update(Ucet ucet);
        void delete(Ucet ucet);
        List<Ucet> readAll();
    }
}





