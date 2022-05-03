using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beauty_saloon.model
{
    class ListTitle: ObservableCollection<Client>
    {
        public ListTitle()
        {
            DbSet<Client> postaveks = MainWindow.DataEntitiesEmployee.Clients;
            var queryTilte = from Postavik in postaveks select Postavik;
            foreach (Client tilt in queryTilte)
            {
                this.Add(tilt);
            }
        }
    }
}
