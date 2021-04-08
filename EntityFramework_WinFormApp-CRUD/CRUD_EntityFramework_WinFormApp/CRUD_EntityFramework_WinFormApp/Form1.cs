using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_EntityFramework_WinFormApp
{
    public partial class Form1 : Form
    {
        table_chatUsers tableChat = new table_chatUsers();
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Uygulamama Hoşgeldiniz");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView();
        }

       

        //Read Button
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView();
        }

        //Cancel button
        private void cnclBtn_Click(object sender, EventArgs e)
        {
            clear();
        }
        //Add button
        private void addBtn_Click(object sender, EventArgs e)
        {
            addData();
            clear();
            dataGridView();
            MessageBox.Show("Kayıt Başarılı bir şekilde eklendi.");
        }
        //Save button
        private void button1_Click_1(object sender, EventArgs e)
        {
            addData();
            clear();
            dataGridView();
            MessageBox.Show("Kayıt Başarılı bir şekilde yapıldı.");
        }
        //Delete button
        private void dltBtn_Click(object sender, EventArgs e)
        {
            deleteData();
            dataGridView();
            clear();
            MessageBox.Show("Başarılı Bir Şekilde Silme İşlemi Yapıldı.");
        }

        //Update button
        private void updateBtn_Click(object sender, EventArgs e)
        {
            updateData();
            dataGridView();
            clear();
            MessageBox.Show("Başarılı Bir Şekilde Güncelleme Yapıldı");
        }

        //Data Grid üzerine double click yapılınca veriyi çekmek için
        private void customerDataGrid_DoubleClick(object sender, EventArgs e)
        {
            if (customerDataGrid.CurrentRow.Index >= 0)
            {
                tableChat.user_id = Convert.ToInt32(customerDataGrid.CurrentRow.Cells["user_id"].Value);
                using (ntp_chatApplicationEntities1 db = new ntp_chatApplicationEntities1())
                {
                    tableChat = db.table_chatUsers.Where(x => x.user_id == tableChat.user_id).FirstOrDefault();
                    txtName.Text = tableChat.name;
                    txtSrName.Text = tableChat.surname;
                    txtMessage.Text = tableChat.message_text;
                }

            }

        }

        //App'ten çıkış yapmak için.
        private void quitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Bu methodu textboxları temizlemek için kullanıyoruz.
        /// </summary>
        void clear()
        {
            txtName.Text = "";
            txtSrName.Text = "";
            txtMessage.Text = "";
        }

        /// <summary>
        /// Database verilerini DataGridView'da göstermek için kullandığımız method.
        /// </summary>
        void dataGridView()
        {
            using(ntp_chatApplicationEntities1 db = new ntp_chatApplicationEntities1())
            {
                customerDataGrid.DataSource = db.table_chatUsers.ToList<table_chatUsers>();
            }
        }

        /// <summary>
        /// Database veri eklemek için kullandığımız method.
        /// </summary>
        void addData()
        {
            tableChat.name = txtName.Text;
            tableChat.surname = txtSrName.Text;
            tableChat.message_text = txtMessage.Text;

            using (ntp_chatApplicationEntities1 db = new ntp_chatApplicationEntities1())
            {
                db.table_chatUsers.Add(tableChat);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Database'den veri silmek için kullandığımız method.
        /// </summary>
        void deleteData()
        {
            using (ntp_chatApplicationEntities1 db = new ntp_chatApplicationEntities1())
            {
                //db.Entry 
                var entry = db.Entry(tableChat);
                if(entry.State==EntityState.Detached)
                {
                    db.table_chatUsers.Attach(tableChat);
                }
                db.table_chatUsers.Remove(tableChat);
                db.SaveChanges();
                dataGridView();
                clear();

                //tableChat.name = txtName.Text;
                //tableChat.surname = txtSrName.Text;
                //tableChat.message_text = txtMessage.Text;
                //db.table_chatUsers.Remove(tableChat);
                //db.SaveChanges();
            }
        }

        /// <summary>
        /// Databe update etmek için kullandığımız method.
        /// </summary>
        void updateData()
        {
            using (ntp_chatApplicationEntities1 db = new ntp_chatApplicationEntities1())
            {

                tableChat.name = txtName.Text;
                tableChat.surname = txtSrName.Text;
                tableChat.message_text = txtMessage.Text;
                db.Entry(tableChat).State = EntityState.Modified;
                db.SaveChanges();
            }
        }



        //Boş componentler
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void customerDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
