using System;
using System.Collections.Generic;
using System.Text;

namespace CountryChecker__CRMguru_TEST_
{
    class Capital
    {
        public int ID = 0;
        public string CapitalN
        {
            get { return CapitalN; }
            private set { CapitalN = value; }
        }
        public Capital(string pr1)
        {
            CapitalN = pr1;
            ID++;
        }
    }
}
