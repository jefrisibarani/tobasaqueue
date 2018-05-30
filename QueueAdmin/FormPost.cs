using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tobasa
{
    public partial class FormPost : Form
    {
        public event Action<EventArgs> DataChanged;
        private bool insertMode = false;
        private string postName;

        public FormPost(string post)
        {
            postName = post;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";
        }

        public FormPost()
        {
            insertMode = true;
            InitializeComponent();

            if (insertMode) btnAction.Text = "&Insert";
            else btnAction.Text = "&Update";
        }

        private void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(new EventArgs());
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
                    sql = "INSERT INTO posts (name,keterangan,numberprefix) VALUES (?,?,?)";
                else
                    sql = "UPDATE posts SET name = ? , keterangan = ? , numberprefix = ? WHERE name = ?";
                
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = txtPost.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.VarChar, 254).Value = txtRemark.Text.Trim();
                        cmd.Parameters.Add("?", OleDbType.Char, 2).Value = txtPrefix.Text.Trim();

                        if (!insertMode) // UPDATE
                            cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = postName;

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
            if (Database.Me.Connected)
            {
                string sql = "SELECT name,keterangan,numberprefix FROM posts WHERE name= ?";
                try
                {
                    Database.Me.OpenConnection();
                    using (OleDbCommand cmd = new OleDbCommand(sql, Database.Me.Connection))
                    {
                        cmd.Parameters.Add("?", OleDbType.VarChar, 32).Value = postName;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                
                                txtPost.Text = postName;

                                var remark = reader.GetValue(1);
                                if (remark != null)
                                    txtRemark.Text = remark.ToString().Trim();

                                var prefix = reader.GetValue(2);
                                if (prefix != null)
                                    txtPrefix.Text = prefix.ToString().Trim();
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
