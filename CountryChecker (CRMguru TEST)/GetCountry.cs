using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountryChecker__CRMguru_TEST_
{
    class GetCountry
    {
        private static string TempStr;
        private static string[] Data = new string[6];
        private static WebClient Client = new WebClient();
        //----Async Data Hook----
        public static async void GetDataFromSite(string Country)
        {
            try
            {
                await Task.Delay(0);
                if (Country != "")
                {
                    for (byte i = 0; i != Data.Length; i++)
                    {
                        Data[i] = null;
                    }
                    TempStr = Client.DownloadString($"https://restcountries.eu/rest/v2/name/{Country}");
                    if (TempStr != "")
                    {
                        Data[0] = DataParser(TempStr, 0);
                        Data[1] = DataParser(TempStr, 1);
                        Data[2] = DataParser(TempStr, 2);
                        Data[3] = DataParser(TempStr, 3);
                        Data[4] = DataParser(TempStr, 14);
                        Data[5] = DataParser(TempStr, 15);
                    }
                }
            }
            catch 
            {

            }
        }
        //----Simple Parser----
        public static string DataParser(string Data, byte index)
        {
            string Name;
            string Region;
            string Capital;
            string Code;
            string Area;
            string Population;
            Regex OneData = new Regex(":\".*?\"");
            Regex TwoData = new Regex(":.*?,");
            MatchCollection match = OneData.Matches(Data);
            if (match.Count > 0)
            {
                if (index < 10)
                {
                    switch (index)
                    {
                        case 0:
                            Name = match[0].ToString().Trim(new char[] { ':', '"' });
                            return Name;
                        case 1:
                            Region = match[4].ToString().Trim(new char[] { ':', '"' });
                            return Region;
                        case 2:
                            Capital = match[3].ToString().Trim(new char[] { ':', '"' });
                            return Capital;
                        case 3:
                            Code = match[8].ToString().Trim(new char[] { ':', '"' });
                            return Code;
                        default:
                            return "ERROR 0x01";
                    }
                }
                else
                {
                    match = TwoData.Matches(Data);
                    if (match.Count > 0)
                    {
                        switch (index)
                        {
                            case 14:
                                Area = match[12].ToString().Trim(new char[] { ':', ',' });
                                return Area;
                            case 15:
                                Population = match[9].ToString().Trim(new char[] { ':', ',' });
                                return Population;
                            default:
                                return "ERROR 0x03";
                        }
                    }
                    else return "ERROR 0x02";
                }
            }
            else return "ERROR 0x00";
        }
        //----Simple indexator----
        public static string ReturnData(byte index)
        {
            return Data[index];
        }
    }
}
