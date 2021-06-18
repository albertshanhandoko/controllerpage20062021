using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ControllerPage.Library
{
    class data_measure_2 : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _id;
        private string _measures;
        private string _created_date;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }

        }

        public string Measures 
        {
            get { return _measures; }
            set
            {
                if (_measures != value)
                {
                    _measures = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Measures)));
                }
            }
        }

        public string Created_date 
        {
            get { return _created_date; }
            set
            {
                if (_created_date != value)
                {
                    _created_date = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Created_date)));
                }
            }
        }
        
        
        public void set(int id, string measures, string created_date)
        {
            Id = id;
            Measures = measures;
            Created_date = created_date;
            
        }
        public data_measure_2(int _id, string _measures, string _created_date)
        {
            set(_id, _measures, _created_date);
        }
        

    }
}
