using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Cajovna.DAO
{
    public interface PolozkyMenuDAO
    {
        void create(PolozkaMenu polozkaMenu);
        PolozkaMenu read(int id);
        void update(PolozkaMenu polozkaMenu);
        void delete(PolozkaMenu polozkaMenu);
        List<PolozkaMenu> readAll();
    }
}
