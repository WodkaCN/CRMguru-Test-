using System;
using System.Collections.Generic;
using System.Text;

namespace CountryChecker__CRMguru_TEST_
{
    class Region
    {
        public int ID = 0;
        public string RegionN
        {
            get { return RegionN; }
            private set { RegionN = value; }
        }
        public Region(string pr1)
        {
            RegionN = pr1;
            ID++;
        }
    }
}
