using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerPage.Library
{
    class Data_Parameter
    {

        private int _id;
        private string _parameter;
        private string _value;

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }

        }

        public string Parameter
        {
            get { return _parameter; }
            set
            {
                if (_parameter != value)
                {
                    _parameter = value;
                }
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                }
            }
        }


        public void set(int id, string parameter, string value)
        {
            Id = id;
            Parameter = parameter;
            Value = value;

        }
        public Data_Parameter(int _id, string _parameter, string _value)
        {
            set(_id, _parameter, _value);
        }


    }
}
