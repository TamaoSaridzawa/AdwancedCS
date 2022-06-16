using System;

namespace CleanCode_Task9
{
    class Database {

        public const int NumberCharactersPassport = 10;

        public void checkButton_Click(object sender, EventArgs e)
        {
            if (this.passportTextbox.Text.Trim() == "")
            {
                EnterData();
            }
            else
            {
                 ProcessData();
            }
        }

        private void EnterData()
        {
            int data = (int)MessageBox.Show("Введите серию и номер паспорта");
        }

        private void ProcessData()
        {
            string rawData = this.passportTextbox.Text.Trim().Replace(" ", string.Empty);

            if (rawData.Length < NumberCharactersPassport)
            {
                OutputError();
            }
            else
            {
                CreateConnection(rawData);
            }
        }

        private void OutputError()
        {
            this.textResult.Text = "Неверный формат серии или номера паспорта";
        }

        private void CreateConnection(string rawData)
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

            try
            {
                SQLiteConnection connection = new SQLiteConnection(connectionString);

                connection.Open();

                CreateDataAdapter(commandText, connection);
            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode != 1)
                    return;

                int num2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }
        }

        private void CreateDataAdapter(string commandText, SQLiteConnection connection)
        {
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));

            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = dataTable1;

            sqLiteDataAdapter.Fill(dataTable2);

            WorkDataAdapter(dataTable1);

            connection.Close();
        }

        private void WorkDataAdapter(DataTable dataTable1)
        {
            CheckListParticipants(dataTable1);
        }

        private void CheckListParticipants(DataTable dataTable1)
        {
            if (dataTable1.Rows.Count > 0)
            {
                ShowResult(dataTable1);
            }
            else
            {
                this.textResult.Text = "Паспорт «" + this.passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
            }
        }

        private void ShowResult(DataTable dataTable1)
        {
            if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
            {
                this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
            }
            else
            {
                this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            }
        }
    }
}
