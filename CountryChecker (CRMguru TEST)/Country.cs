using System;
using System.Collections.Generic;
using System.Text;

namespace CountryChecker__CRMguru_TEST_
{
    class Country
    {
        private string _name, _region, _capital, _code, _area, _population;
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }
        public string Region
        {
            get { return _region; }
            private set { _region = value; }
        }
        public string Capital
        {
            get { return _capital; }
            private set { _capital = value; }
        }
        public string Code
        {
            get { return _code; }
            private set { _code = value; }
        }
        public string Area
        {
            get { return _area; }
            private set { _area = value; }
        }
        public string Population
        {
            get { return _population; }
            private set { _population = value; }
        }
        public Country(string pr1, string pr2, string pr3, string pr4, string pr5, string pr6)
        {
            Name = pr1;
            Region = pr2;
            Capital = pr3;
            Code = pr4;
            Area = pr5;
            Population = pr6;
        }
    }
}
