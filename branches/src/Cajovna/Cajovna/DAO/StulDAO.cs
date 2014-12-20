using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajovna.DAO
{
    public interface StulDAO
    {
        void create(Stul stul);
        Stul read(int id);
        void update(Stul stul);
        void delete(Stul stul);
        List<Stul> readAll();
    }
}
