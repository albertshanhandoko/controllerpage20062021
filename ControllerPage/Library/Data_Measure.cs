using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ControllerPage.Library
{
    class Data_Measure
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Data_Measure(int id, string measures, DateTime created_date)
        {
            set(id, measures, created_date);
        }
        public int Id { set; get; }
        //public List<int> Measures{set; get;}
        public string Measures { set; get; }
        public DateTime Created_date { set; get; } 
        public void set(int id,string measures, DateTime created_date)
        {
            Id = id;
            Measures = measures;
            Created_date = created_date;
        }

    }
}
