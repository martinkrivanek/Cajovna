using Cajovna.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Cajovna.DAO
{
    public interface SlozeniDAO
    {
        void create(Slozeni slozeni);
        Slozeni read(int id);
        void update(Slozeni slozeni);
        void delete(Slozeni slozeni);
        List<Slozeni> readAll();
    }
}
