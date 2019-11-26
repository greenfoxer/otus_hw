using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace ATMDataModel
{
    public class DataSetModel
    {
        public DataSet ATMModel;
        public DataSetModel()
        {
            ATMModel = new DataSet("dbATM");
            DataTable tUser = new DataTable("tUser");
            ATMModel.Tables.Add(tUser);

            DataColumn idColumn = new DataColumn("id", Type.GetType("System.Int64"));
            idColumn.Unique = true;
            idColumn.AllowDBNull = false;
            //idColumn.AutoIncrement = true;
            //idColumn.AutoIncrementSeed = 1;
            //idColumn.AutoIncrementStep = 1;

            DataColumn nameColumn = new DataColumn("name", Type.GetType("System.String"));
            DataColumn surnameColumn = new DataColumn("surname", Type.GetType("System.String"));
            DataColumn middlenameColumn = new DataColumn("middlename", Type.GetType("System.String"));
            DataColumn phoneColumn = new DataColumn("phone", Type.GetType("System.String"));
            DataColumn identityColumn = new DataColumn("identitydata", Type.GetType("System.String"));
            DataColumn dateregColumn = new DataColumn("datereg", Type.GetType("System.DateTime"));
            DataColumn loginColumn = new DataColumn("login", Type.GetType("System.String"));
            loginColumn.Unique = true;
            DataColumn passwordColumn = new DataColumn("password", Type.GetType("System.String"));

            tUser.Columns.Add(idColumn);
            tUser.Columns.Add(nameColumn);
            tUser.Columns.Add(surnameColumn);
            tUser.Columns.Add(middlenameColumn);
            tUser.Columns.Add(phoneColumn);
            tUser.Columns.Add(identityColumn);
            tUser.Columns.Add(dateregColumn);
            tUser.Columns.Add(loginColumn);
            tUser.Columns.Add(passwordColumn);

            tUser.PrimaryKey = new DataColumn[] { tUser.Columns["id"] };

            DataTable tAccount = new DataTable("tAccount");
            ATMModel.Tables.Add(tAccount);

            DataColumn idAccountColumn = new DataColumn("id", Type.GetType("System.Int64"));
            idAccountColumn.Unique = true;
            idAccountColumn.AllowDBNull = false;
            //idAccountColumn.AutoIncrement = true;
            //idAccountColumn.AutoIncrementSeed = 1;
            //idAccountColumn.AutoIncrementStep = 1;

            DataColumn datecrationColumn = new DataColumn("datecreation", Type.GetType("System.DateTime"));
            DataColumn totalColumn = new DataColumn("total", Type.GetType("System.Decimal"));
            DataColumn idownerColumn = new DataColumn("idowner", Type.GetType("System.Int64"));

            tAccount.Columns.Add(idAccountColumn);
            tAccount.Columns.Add(datecrationColumn);
            tAccount.Columns.Add(totalColumn);
            tAccount.Columns.Add(idownerColumn);

            ForeignKeyConstraint fktUser_tAccount = new ForeignKeyConstraint("tUser_tAccount", idColumn, idownerColumn);
            fktUser_tAccount.DeleteRule = Rule.SetNull;
            fktUser_tAccount.UpdateRule = Rule.Cascade;
            fktUser_tAccount.AcceptRejectRule = AcceptRejectRule.None;

            tAccount.Constraints.Add(fktUser_tAccount);

            tAccount.PrimaryKey = new DataColumn[] { tAccount.Columns["id"] };

            DataTable tHistory = new DataTable("tHistory");
            ATMModel.Tables.Add(tHistory);

            DataColumn idHistoryColumn = new DataColumn("id", Type.GetType("System.Int64"));
            idHistoryColumn.Unique = true;
            idHistoryColumn.AllowDBNull = false;
            //idHistoryColumn.AutoIncrement = true;
            //idHistoryColumn.AutoIncrementSeed = 1;
            //idHistoryColumn.AutoIncrementStep = 1;

            DataColumn dateoperationColumn = new DataColumn("dateoperation", Type.GetType("System.DateTime"));
            DataColumn amouncColumn = new DataColumn("amount", Type.GetType("System.Decimal"));
            DataColumn idaccountColumn = new DataColumn("idaccount", Type.GetType("System.Int64"));
            DataColumn idtypeColumn = new DataColumn("typeop", Type.GetType("System.Int32"));

            tHistory.Columns.Add(idHistoryColumn);
            tHistory.Columns.Add(dateoperationColumn);
            tHistory.Columns.Add(idtypeColumn);
            tHistory.Columns.Add(amouncColumn);
            tHistory.Columns.Add(idaccountColumn);

            tHistory.PrimaryKey = new DataColumn[] { tHistory.Columns["id"] };

            ForeignKeyConstraint fktAccount_tHistory = new ForeignKeyConstraint("tAccount_tHistory", idAccountColumn, idaccountColumn);
            fktAccount_tHistory.DeleteRule = Rule.SetNull;
            fktAccount_tHistory.UpdateRule = Rule.Cascade;
            fktAccount_tHistory.AcceptRejectRule = AcceptRejectRule.None;

            tHistory.Constraints.Add(fktAccount_tHistory);

            Fill();
        }
        void Fill()
        {
            DataTable tUser = ATMModel.Tables["tUser"];
            tUser.Rows.Add(new object[] { 1, "Иван", "Иванов", "Иванович", "8-987-654-32-11", "9988-123654", DateTime.Now, "test", "1234" });
            tUser.Rows.Add(new object[] { 2, "Петр", "Петров", "Петрович", "8-965-489-25-16", "9782-127636", DateTime.Now, "petro", "qwer" });
            tUser.Rows.Add(new object[] { 3, "Сидор", "Сидоров", "Сидорович", "8-967-664-62-61", "9163-183143", DateTime.Now, "cider", "cider" });
            tUser.Rows.Add(new object[] { 4, "Маша", "Иванова", "Ивановна", "8-546-654-32-12", "9912-124154", DateTime.Now, "mariya", "qazwsx" });
            tUser.Rows.Add(new object[] { 5, "Глаша", "Петрова", "Ивановна", "8-937-654-71-11", "9188-122854", DateTime.Now, "pewpew", "atata" });

            DataTable tAccount = ATMModel.Tables["tAccount"];
            tAccount.Rows.Add(new object[] { 1, new DateTime(2017,12,12), 100000, 1});
            tAccount.Rows.Add(new object[] { 2, new DateTime(2017, 11, 01), 10000, 2 });
            tAccount.Rows.Add(new object[] { 3, new DateTime(2017, 10, 02), 100500, 3 });
            tAccount.Rows.Add(new object[] { 4, new DateTime(2017, 9, 05), 200, 4 });
            tAccount.Rows.Add(new object[] { 5, new DateTime(2017, 8, 07), 1, 5 });
            tAccount.Rows.Add(new object[] { 6, new DateTime(2019, 1, 31), 70000, 2 });
            tAccount.Rows.Add(new object[] { 7, new DateTime(2018, 2, 28), 1000000, 1 });
            tAccount.Rows.Add(new object[] { 8, new DateTime(2019, 3, 28), 50000, 4 });
            tAccount.Rows.Add(new object[] { 9, new DateTime(2018, 4, 18), 300000, 2 });
            tAccount.Rows.Add(new object[] { 10, new DateTime(2019, 5, 23), 10000, 3 });
            tAccount.Rows.Add(new object[] { 11, new DateTime(2018, 6, 2), 100000, 2 });
            tAccount.Rows.Add(new object[] { 12, new DateTime(2019, 7, 16), 10000, 2 });

            DataTable tHistory = ATMModel.Tables["tHistory"];
            tHistory.Rows.Add(new object[] { 1, new DateTime(2018, 01, 15, 14, 42, 10),  1, 1000, 1});
            tHistory.Rows.Add(new object[] { 2, new DateTime(2018, 01, 20, 21, 23, 10),  2, 2000, 2 });
            tHistory.Rows.Add(new object[] { 3, new DateTime(2018, 01, 21, 6, 26, 10),   2, 3000, 3 });
            tHistory.Rows.Add(new object[] { 4, new DateTime(2018, 01, 26, 18, 17, 10),  1, 4000, 4 });
            tHistory.Rows.Add(new object[] { 5, new DateTime(2018, 01, 30, 23, 12, 10),  2, 5000, 5 });
            tHistory.Rows.Add(new object[] { 6, new DateTime(2018, 02, 5, 12, 47, 10),   2, 6000, 6 });
            tHistory.Rows.Add(new object[] { 7, new DateTime(2018, 02, 6, 7, 28, 10),    1, 7000, 7 });
            tHistory.Rows.Add(new object[] { 8, new DateTime(2018, 02, 10, 9, 59, 10),   2, 8000, 3 });
            tHistory.Rows.Add(new object[] { 9, new DateTime(2018, 02, 12, 12, 58, 10),  1, 9000, 4 });
            tHistory.Rows.Add(new object[] { 10, new DateTime(2018, 02, 28, 11, 51, 10), 2, 10000, 10 });
            tHistory.Rows.Add(new object[] { 11, new DateTime(2018, 03, 10, 17, 47, 10), 2, 11000, 1 });
            tHistory.Rows.Add(new object[] { 12, new DateTime(2018, 03, 25, 21, 31, 10), 1, 12000, 9 });
            tHistory.Rows.Add(new object[] { 13, new DateTime(2018, 04, 12, 22, 28, 10), 2, 13000, 3 });
            tHistory.Rows.Add(new object[] { 14, new DateTime(2018, 05, 7, 18, 17, 10),  2, 14000, 12 });
            tHistory.Rows.Add(new object[] { 15, new DateTime(2018, 06, 1, 23, 49, 10),  2, 15000, 9 });
            tHistory.Rows.Add(new object[] { 16, new DateTime(2018, 06, 21, 15, 45, 10), 1, 16000, 9 });
            tHistory.Rows.Add(new object[] { 17, new DateTime(2018, 07, 17, 16, 45, 10), 2, 17000, 1 });
            tHistory.Rows.Add(new object[] { 18, new DateTime(2018, 08, 27, 18, 23, 10), 2, 18000, 6 });
            tHistory.Rows.Add(new object[] { 19, new DateTime(2018, 09, 3, 22, 32, 10),  1, 19000, 11 });
            tHistory.Rows.Add(new object[] { 20, new DateTime(2018, 11, 15, 10, 12, 10), 1, 20000, 2 });
            tHistory.Rows.Add(new object[] { 21, new DateTime(2018, 12, 17, 19, 15, 10), 2, 21000, 3 });
            tHistory.Rows.Add(new object[] { 22, new DateTime(2019, 01, 3, 12, 45, 10),  2, 22000, 8 });
            tHistory.Rows.Add(new object[] { 23, new DateTime(2019, 3, 17, 14, 28, 10),  1, 23000, 7 });
            tHistory.Rows.Add(new object[] { 24, new DateTime(2019, 4, 13, 13, 56, 10),  2, 24000, 4 });
            tHistory.Rows.Add(new object[] { 25, new DateTime(2019, 5, 1, 3, 7, 10),     2, 25000, 5 });
            tHistory.Rows.Add(new object[] { 26, new DateTime(2019, 6, 30, 16, 43, 10),  1, 26000, 1 });
            tHistory.Rows.Add(new object[] { 27, new DateTime(2019, 7, 20, 2, 3, 10),    2, 12700, 12 });
            tHistory.Rows.Add(new object[] { 28, new DateTime(2019, 10, 15, 5, 8, 10),   2, 29000, 2 });
            tHistory.Rows.Add(new object[] { 29, new DateTime(2019, 11, 3, 15, 23, 10),  1, 30000, 5 });
        }
    }
}
