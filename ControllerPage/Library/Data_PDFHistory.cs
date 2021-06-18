using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerPage.Library
{
    class Data_PDFHistory
    {

        public Data_PDFHistory(int id, string histories)
        {
            set(id, histories);
        }
        public int Id { set; get; }
        public string Histories { set; get; }
        public void set(int id, string histories)
        {
            Id = id;
            Histories = histories;
        }

    }
}
