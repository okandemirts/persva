using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        ShiftDal _shiftDal = new ShiftDal();
        UserDal _userDal = new UserDal();

        private void Main_Load(object sender, EventArgs e)
        {
            CheckUserRole();
            GetList();
        }

        public void GetList()
        {
            dgv_shifts.Rows.Clear();

            List<Shift> shifts;
            if (UserInformation.Role.SeeAllShifts)
            {
                 shifts = _shiftDal.GetAllWithDate(dtp_startDate.Value, dtp_endDate.Value);
            }
            else
            {
                 shifts = _shiftDal.GetAllWithDateAndEmployee(dtp_startDate.Value, dtp_endDate.Value, UserInformation.User.Id);
            }

            foreach (Shift shift in shifts)
            {
                var registrantUser = _userDal.GetById(shift.RegistrantId);
                var user = _userDal.GetById(shift.EmployeeId);

                string durum = shift.IsNew ? "Aktif Vardiya" : "Pasif Vardiya";

                dgv_shifts.Rows.Add(shift.Id, shift.RegistrantId, registrantUser.NameSurname, shift.EmployeeId, user.NameSurname, shift.DateOfRegistration.ToShortDateString(), shift.Date.ToShortDateString(), shift.Location, shift.Hours, shift.IsNew, durum);
            }

            dgv_shifts.ClearSelection();
        }

        private void dtp_endDate_ValueChanged(object sender, EventArgs e)
        {
            GetList();
        }

        public void CheckUserRole()
        {
            int collapse = flp_buttons.Controls.Count;

            lbl_nameSurname.Text = UserInformation.User.Name + " " + UserInformation.User.Surname.ToUpper();
            lbl_roleName.Text = "(" + UserInformation.Role.Name + ")";

            if (!UserInformation.Role.Add)
            {
                btn_add.Visible = false;
                collapse--;
            }

            if (!UserInformation.Role.Update)
            {
                btn_update.Visible = false;
                collapse--;
            }

            if (!UserInformation.Role.Delete)
            {
                btn_delete.Visible = false;
                collapse--;
            }

            if (!UserInformation.Role.Print)
            {
                btn_print.Visible = false;
                collapse--;
            }

            if (!UserInformation.Role.UsersManagement)
            {
                btn_users.Visible = false;
                collapse--;
            }

            if (!UserInformation.Role.RolesManagement)
            {
                btn_roles.Visible = false;
                collapse--;
            }

            if (collapse == 0)
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Login loginCheck = (Login)Application.OpenForms["Login"];

            if (loginCheck != null)
            {
                loginCheck.Show();
            }
            else
            {
                Login login = new Login();
                login.Show();
            }

            this.Close();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 160;
        }

        #region LeftMenu
        private void btn_roles_Click(object sender, EventArgs e)
        {
            Roles roles = new Roles();
            roles.ShowDialog();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ShiftAdd shiftAdd = new ShiftAdd();
            shiftAdd.ShowDialog();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (dgv_shifts.SelectedRows.Count != 0)
            {
                ShiftUpdate shiftUpdate = new ShiftUpdate(Convert.ToInt32(dgv_shifts.CurrentRow.Cells[0].Value));
                shiftUpdate.ShowDialog();
            }
            else
            {
                MessageBox.Show("Güncellenecek vardiyayı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.ShowDialog();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (dgv_shifts.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Vardiyayı silmek istediğinizden emin misiniz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    _shiftDal.Delete(Convert.ToInt32(dgv_shifts.CurrentRow.Cells[0].Value));
                    GetList();
                    dgv_shifts.ClearSelection();
                }
            }
            else
            {
                MessageBox.Show("Silinecek vardiyayı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void btn_print_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Kayıt Eden Personel");
            dataTable.Columns.Add("Personel");
            dataTable.Columns.Add("Kayıt Tarihi");
            dataTable.Columns.Add("Vardiya Tarihi");
            dataTable.Columns.Add("Lokasyon");
            dataTable.Columns.Add("Saatler");
            dataTable.Columns.Add("Durum");

            for (int i = 0; i < dgv_shifts.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.NewRow();

                dataRow["Kayıt Eden Personel"] = dgv_shifts.Rows[i].Cells[2].Value;
                dataRow["Personel"] = dgv_shifts.Rows[i].Cells[4].Value;
                dataRow["Kayıt Tarihi"] = dgv_shifts.Rows[i].Cells[5].Value;
                dataRow["Vardiya Tarihi"] = dgv_shifts.Rows[i].Cells[6].Value;
                dataRow["Lokasyon"] = dgv_shifts.Rows[i].Cells[7].Value;
                dataRow["Saatler"] = dgv_shifts.Rows[i].Cells[8].Value;
                dataRow["Durum"] = dgv_shifts.Rows[i].Cells[10].Value;

                dataTable.Rows.Add(dataRow);
            }

            PrintPDFBuilder.Print(dataTable, dtp_startDate.Value, dtp_endDate.Value);
        }
    }
}
