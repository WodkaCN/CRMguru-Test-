using System;
using System.Collections.Generic;
using System.Text;

namespace CountryChecker__CRMguru_TEST_
{
    class Country
    {
        private int n = 0;
        public string Name
        {
            get { return Name; }
            private set { Name = value; }
        }
        public string Region
        {
            get { return Region; }
            private set { Region = value; }
        }
        public string Capital
        {
            get { return Capital; }
            private set { Capital = value; }
        }
        public string Code
        {
            get { return Code; }
            private set { Code = value; }
        }
        public string Area
        {
            get { return Area; }
            private set { Area = value; }
        }
        public string Population
        {
            get { return Population; }
            private set { Population = value; }
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
