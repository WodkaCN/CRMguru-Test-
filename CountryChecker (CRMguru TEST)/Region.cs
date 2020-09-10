using System;
using System.Collections.Generic;
using System.Text;

namespace CountryChecker__CRMguru_TEST_
{
    class Region
    {
        private string _region;
        public int ID = 0;
        public string RegionN
        {
            get { return _region; }
            private set { _region = value; }
        }
        public Region(string pr1)
        {
            RegionN = pr1;
            ID++;
        }
    }
}
