using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajovna.DAO
{
    public interface SurovinyDAO
    {
        void create(Surovina surovina);
        Surovina read(int id);
        void update(Surovina surovina);
        void delete(Surovina surovina);
        List<Surovina> readAll();
    }
}
