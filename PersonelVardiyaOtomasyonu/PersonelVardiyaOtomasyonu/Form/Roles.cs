using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class Roles : Form
    {
        public Roles()
        {
            InitializeComponent();
        }

        RoleDal _roleDal = new RoleDal();

        private void Roles_Load(object sender, EventArgs e)
        {
            GetList();
        }

        public void GetList()
        {
            dgv_roles.Rows.Clear();

            var roles = _roleDal.GetAll();
            foreach (var role in roles)
            {
                dgv_roles.Rows.Add(role.Id, role.Name, role.Add, role.Update, role.Delete, role.Print, role.UsersManagement, role.RolesManagement,role.SeeAllShifts, role.Status);
            }

            dgv_roles.ClearSelection();
        }

        private void dgv_roles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_name.Text = dgv_roles.CurrentRow.Cells[1].Value.ToString();
            chb_add.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[2].Value);
            chb_update.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[3].Value);
            chb_delete.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[4].Value);
            chb_print.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[5].Value);
            chb_usersManagement.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[6].Value);
            chb_rolesManagement.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[7].Value);
            chb_seeAllShifts.Checked = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[8].Value);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_name.Text != "")
            {
                Role role = new Role
                {
                    Name = txt_name.Text,
                    Add = Convert.ToBoolean(chb_add.Checked),
                    Update = Convert.ToBoolean(chb_update.Checked),
                    Delete = Convert.ToBoolean(chb_delete.Checked),
                    Print = Convert.ToBoolean(chb_print.Checked),
                    UsersManagement = Convert.ToBoolean(chb_usersManagement.Checked),
                    RolesManagement = Convert.ToBoolean(chb_rolesManagement.Checked),
                    SeeAllShifts = Convert.ToBoolean(chb_seeAllShifts.Checked),
                    Status = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[9].Value)
                };

                _roleDal.Add(role);
                GetList();
                Clear();
            }
            else
            {
                MessageBox.Show("Rol adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dgv_roles.SelectedRows.Count != 0)
            {
                if (txt_name.Text != "")
                {
                    Role role = new Role
                    {
                        Id = Convert.ToInt32(dgv_roles.CurrentRow.Cells[0].Value),
                        Name = txt_name.Text,
                        Add = Convert.ToBoolean(chb_add.Checked),
                        Update = Convert.ToBoolean(chb_update.Checked),
                        Delete = Convert.ToBoolean(chb_delete.Checked),
                        Print = Convert.ToBoolean(chb_print.Checked),
                        UsersManagement = Convert.ToBoolean(chb_usersManagement.Checked),
                        RolesManagement = Convert.ToBoolean(chb_rolesManagement.Checked),
                        SeeAllShifts = Convert.ToBoolean(chb_seeAllShifts.Checked),
                        Status = Convert.ToBoolean(dgv_roles.CurrentRow.Cells[9].Value)
                    };

                    _roleDal.Update(role);
                    GetList();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Rol adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Güncellenecek rolu seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_roles.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Rolu silmek istediğinizden emin misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    _roleDal.Delete(Convert.ToInt32(dgv_roles.CurrentRow.Cells[0].Value));
                    GetList();
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Silinecek rolu seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Clear()
        {
            txt_name.Clear();
            chb_add.Checked = false;
            chb_update.Checked = false;
            chb_delete.Checked = false;
            chb_print.Checked = false;
            chb_usersManagement.Checked = false;
            chb_rolesManagement.Checked = false;
            chb_seeAllShifts.Checked = false;
        }
    }
}
