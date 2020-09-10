using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CountryChecker__CRMguru_TEST_
{
    class SQLTasks
    {
        private static SqlConnection CON;
        private static SqlCommand CMD;
        private static string SQLTask;
        //----Connect to DataBase----
        public static void Connetion(string CONcfg)
        {
            //string CONcfg = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 23) + "CountrySummaryDataBase.mdf";
            string constr = String.Format($@"Data Source=(localdb)\MSSQLLocalDB;AttachDBFilename={CONcfg};Integrated Security=True");
            CON = new SqlConnection(constr);
            CON.Open();
        }
        public static void ConClose()
        {
            try
            {
                CON.Close();
            }
            catch { }
        }
        //----All Selector----
        public static DataTable SelectAll()
        {
            try
            {
                SQLTask = $@"SELECT [dbo].[t_dicCountries].[nID], [dbo].[t_dicCountries].[sName], [dbo].[t_dicRegion].[sName] as sRegion, [dbo].[t_dicCapital].[sName] as sCapital, [nCode], [sArea], [nPopulation] 
                                FROM [dbo].[t_dicCountries]
                                JOIN [dbo].[t_dicCapital] ON [nCapital] = [dbo].[t_dicCapital].[nID]
                                JOIN [dbo].[t_dicRegion] ON [nRegion] = [dbo].[t_dicRegion].[nID]";
                SqlDataAdapter Adapter = new SqlDataAdapter(SQLTask, CON);
                DataTable DT = new DataTable();
                Adapter.Fill(DT);
                return DT;
            }
            catch
            {
                return null;
            }
        }
        //----Insert Function----
        public static void Insert(string CName, string CRegion, string CCapital, string CCode, string CArea, string CPopulation)
        {
            try
            {
                int TCCapital = 0, TCRegion = 0;
                DataTable TempT = new DataTable();

                TCCapital = CheckCapital(CCapital);

                TCRegion = CheckRegion(CRegion);

                SQLTask = $@"SELECT [sName] FROM [dbo].[t_dicCountries] WHERE sName = '{CName}'";
                SqlDataAdapter Adapter = new SqlDataAdapter(SQLTask, CON);
                TempT = new DataTable();
                Adapter.Fill(TempT);
                if (TempT.Rows.Count == 0)
                {
                    SQLTask = $@"INSERT INTO [dbo].[t_dicCountries]([sName],[nRegion],[nCapital],[nCode],[sArea],[nPopulation]) VALUES('{CName}','{TCRegion}','{TCCapital}','{CCode}','{CArea}','{CPopulation}')";
                    CMD = new SqlCommand(SQLTask, CON);
                    CMD.ExecuteNonQuery();
                }
                else
                {
                    SQLTask = $@"UPDATE [dbo].[t_dicCountries] SET [nCode] = '{CCode}', [sArea] = '{CArea}', [nPopulation] = '{CPopulation}' WHERE [sName] = '{CName}'";
                    CMD = new SqlCommand(SQLTask, CON);
                    CMD.ExecuteNonQuery();
                }
            }
            catch { }
        }
        public static Int32 CheckCapital(string Capital)
        {
            int TCCapital = 0;
            SqlDataReader Reader;
            DataTable TempT = new DataTable();

            SQLTask = $@"SELECT [nID], [sName] FROM [dbo].[t_dicCapital] WHERE [sName] = '{Capital}'";
            SqlDataAdapter Adapter = new SqlDataAdapter(SQLTask, CON);
            Adapter.Fill(TempT);
            if (TempT.Rows.Count == 0)
            {
                SQLTask = $@"INSERT INTO [dbo].[t_dicCapital]([sName]) VALUES('{Capital}')";
                CMD = new SqlCommand(SQLTask, CON);
                CMD.ExecuteNonQuery();
            }
            SQLTask = $@"SELECT [nID] FROM [dbo].[t_dicCapital] WHERE [sName] = '{Capital}'";
            CMD = new SqlCommand(SQLTask, CON);
            Reader = CMD.ExecuteReader();
            while (Reader.Read())
            {
                TCCapital = Reader.GetInt32(0);
            }
            Reader.Close();
            TempT = null;

            return TCCapital;
        }

        public static Int32 CheckRegion(string Region)
        {
            int TCRegion = 0;
            SqlDataReader Reader;
            DataTable TempT = new DataTable();

            SQLTask = $@"SELECT [nID], [sName] FROM [dbo].[t_dicRegion] WHERE [sName] = '{Region}'";
            SqlDataAdapter Adapter = new SqlDataAdapter(SQLTask, CON);
            TempT = new DataTable();
            Adapter.Fill(TempT);
            if (TempT.Rows.Count == 0)
            {
                SQLTask = $@"INSERT INTO [dbo].[t_dicRegion]([sName]) VALUES('{Region}')";
                CMD = new SqlCommand(SQLTask, CON);
                CMD.ExecuteNonQuery();
            }
            SQLTask = $@"SELECT [nID] FROM [dbo].[t_dicRegion] WHERE [sName] = '{Region}'";
            CMD = new SqlCommand(SQLTask, CON);
            Reader = CMD.ExecuteReader();
            while (Reader.Read())
            {
                TCRegion = Reader.GetInt32(0);
            }
            Reader.Close();
            TempT = null;

            return TCRegion;
        }
    }
}
