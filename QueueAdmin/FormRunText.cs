using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tobasa
{
    public partial class FormRunText : Form
    {
        public event Action<EventArgs> DataChanged;
        private bool insertMode = false;
        private string _id;

        public FormRunText(string id)
        {
            _id = id;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";

        }

        public FormRunText()
        {
            insertMode = true;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";

        }

        private void OnDataChanged()
        {
            DataChanged?.Invoke(new EventArgs());
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnAction(object sender, EventArgs e)
        {
            if (Database.Me.Connected)
            {
                string msg = "";
                if (insertMode)
                    msg = "Do you want to insert record?";
                else
                    msg = "Do you want to update record?";

                if (MessageBox.Show(msg, "Action", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                string sql = "";
                if (insertMode)
                    sql = "INSERT INTO runningtexts (station_name,sticky,active,running_text) VALUES (?,?,?,?)";
                else
                    sql = "UPDATE runningtexts SET station_name = ? , sticky = ? , active = ?, running_text = ? WHERE id = ?";

                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = txtStation.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.Boolean).Value = chkSticky.Checked;
                        cmd.Parameters.Add("?", OleDbType.Boolean).Value = chkActive.Checked;
                        cmd.Parameters.Add("?", OleDbType.VarChar, 255).Value = txtRunText.Text.Trim();

                        if (!insertMode) // UPDATE
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = _id;

                        cmd.ExecuteNonQuery();

                        OnDataChanged();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {

            if (!insertMode) // Update
            {
                if (Database.Me.Connected)
                {
                    string sql = "SELECT station_name,sticky,active,running_text FROM runningtexts WHERE id= ?";
                    try
                    {
                        Database.Me.OpenConnection();
                        using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                        {
                            cmd.Parameters.Add("?", OleDbType.Integer).Value = _id;

                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();

                                    txtStation.Text = reader.GetString(0).Trim();
                                    chkSticky.Checked = reader.GetBoolean(1);
                                    chkActive.Checked = reader.GetBoolean(2);
                                    txtRunText.Text = reader.GetString(3).Trim();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("QueueAdmin : Exception : " + ex.Message);
                    }
                }
            }
        }
    }
}
