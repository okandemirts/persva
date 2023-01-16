using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        UserDal _userDal = new UserDal();
        RoleDal _roleDal = new RoleDal();

        private void Users_Load(object sender, EventArgs e)
        {
            GetRoleList();
            GetList();
        }

        public void GetRoleList()
        {
            cb_roles.DisplayMember = "Name";
            cb_roles.ValueMember = "Id";
            cb_roles.DataSource = _roleDal.GetAll();
        }

        public void GetList()
        {
            dgv_users.Rows.Clear();

            var users = _userDal.GetAll();
            foreach (var user in users)
            {
                var role = _roleDal.GetById(user.RoleId);

                dgv_users.Rows.Add(user.Id, user.RoleId, role.Name, user.IdentificationNumber, user.Name, user.Surname, user.Password, user.Address, user.EMail, user.PhoneNumber, user.Status);
            }

            dgv_users.ClearSelection();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (cb_roles.Text != "" && txt_identificationNumber.Text != "" && txt_name.Text != "" && txt_surname.Text != "" && txt_password.Text != "" && txt_address.Text != "" && txt_email.Text != "" && txt_phoneNumber.Text != "")
            {
                if (!_userDal.CheckByIdentificationNumber(txt_identificationNumber.Text))
                {
                    User user = new User
                    {
                        RoleId = Convert.ToInt32(cb_roles.SelectedValue),
                        IdentificationNumber = txt_identificationNumber.Text,
                        Name = txt_name.Text,
                        Surname = txt_surname.Text,
                        Password = txt_password.Text,
                        Address = txt_address.Text,
                        EMail = txt_email.Text,
                        PhoneNumber = txt_phoneNumber.Text,
                        Status = Convert.ToBoolean(dgv_users.CurrentRow.Cells[10].Value),
                    };

                    _userDal.Add(user);
                    GetList();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Aynı T.C. Kimlik Numaralı personel bulunmaktadır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dgv_users.SelectedRows.Count != 0)
            {
                if (cb_roles.Text != "" && txt_identificationNumber.Text != "" && txt_name.Text != "" && txt_surname.Text != "" && txt_password.Text != "" && txt_address.Text != "" && txt_email.Text != "" && txt_phoneNumber.Text != "")
                {
                    User user = new User
                    {
                        Id = Convert.ToInt32(dgv_users.CurrentRow.Cells[0].Value),
                        RoleId = Convert.ToInt32(cb_roles.SelectedValue),
                        IdentificationNumber = txt_identificationNumber.Text,
                        Name = txt_name.Text,
                        Surname = txt_surname.Text,
                        Password = txt_password.Text,
                        Address = txt_address.Text,
                        EMail = txt_email.Text,
                        PhoneNumber = txt_phoneNumber.Text,
                        Status = Convert.ToBoolean(dgv_users.CurrentRow.Cells[10].Value),
                    };

                    _userDal.Update(user);
                    GetList();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Güncellenecek kullanıcıyı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_users.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Kullanıcıyı silmek istediğinizden emin misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    _userDal.Delete(Convert.ToInt32(dgv_users.CurrentRow.Cells[0].Value));
                    GetList();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Silinecek kullanıcıyı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_users_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cb_roles.Text = dgv_users.CurrentRow.Cells[2].Value.ToString();
            txt_identificationNumber.Text = dgv_users.CurrentRow.Cells[3].Value.ToString();
            txt_name.Text = dgv_users.CurrentRow.Cells[4].Value.ToString();
            txt_surname.Text = dgv_users.CurrentRow.Cells[5].Value.ToString();
            txt_password.Text = dgv_users.CurrentRow.Cells[6].Value.ToString();
            txt_address.Text = dgv_users.CurrentRow.Cells[7].Value.ToString();
            txt_email.Text = dgv_users.CurrentRow.Cells[8].Value.ToString();
            txt_phoneNumber.Text = dgv_users.CurrentRow.Cells[9].Value.ToString();
        }

        public void Clear()
        {
            cb_roles.SelectedIndex = -1;
            txt_identificationNumber.Clear();
            txt_name.Clear();
            txt_surname.Clear();
            txt_password.Clear();
            txt_address.Clear();
            txt_email.Clear();
            txt_phoneNumber.Clear();
        }

    }
}
