using PersonelVardiyaOtomasyonu.Class.DAL;
using PersonelVardiyaOtomasyonu.Class.Helper;
using PersonelVardiyaOtomasyonu.Entities;
using System;
using System.Windows.Forms;

namespace PersonelVardiyaOtomasyonu
{
    public partial class ShiftAdd : Form
    {
        public ShiftAdd()
        {
            InitializeComponent();
        }

        ShiftDal _shiftDal = new ShiftDal();
        UserDal _userDal = new UserDal();

        private void ShiftAdd_Load(object sender, EventArgs e)
        {
            GetUserList();
        }

        public void GetUserList()
        {
            cb_users.DisplayMember = "NameSurname";
            cb_users.ValueMember = "Id";
            cb_users.DataSource = _userDal.GetAll();
        }

        private void cb_locations_TextChanged(object sender, EventArgs e)
        {            
            cb_hours.Items.Clear();

            switch (cb_locations.Text)
            {
                case "Kampüs Girisi":
                    cb_hours.Items.Add("00:00 - 08:00");
                    cb_hours.Items.Add("08:00 - 16:00");
                    cb_hours.Items.Add("16:00 - 24:00");
                    break;

                case "Kampüs Içi":
                    cb_hours.Items.Add("08:00 - 16:00");
                    cb_hours.Items.Add("09:00 - 17:00");
                    break;

                default:
                    cb_hours.Items.Add("Saat bulunmamaktadır!");
                    break;
            }
        }

        private void btn_continue_Click(object sender, EventArgs e)
        {
            if (cb_locations.Text != "" && cb_hours.Text != "" && cb_users.Text != "")
            {
                Shift shift = new Shift
                {
                    RegistrantId = UserInformation.User.Id,
                    EmployeeId = Convert.ToInt32(cb_users.SelectedValue),
                    DateOfRegistration = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    Date = Convert.ToDateTime(dtp_date.Value.ToShortDateString()),
                    Location = cb_locations.Text,
                    Hours = cb_hours.Text,
                    IsNew = true,
                };

                if (_shiftDal.CheckByNewShiftWithEmployeeId(Convert.ToInt32(cb_users.SelectedValue)))
                {
                    if (MessageBox.Show("Seçili personelin aktif bir vardiyası bulunmaktadır.\n\nAktif vardiyasını pasif hale getirip!\n\nYeni vardiya eklensin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        _shiftDal.DeactivateAllNewShiftsTheEmployee(Convert.ToInt32(cb_users.SelectedValue));
                        _shiftDal.Add(shift);

                        Main main = (Main)Application.OpenForms["Main"];
                        if (main != null)
                        {
                            main.GetList();
                        }

                        this.Close();
                    }
                }
                else
                {
                    _shiftDal.Add(shift);

                    Main main = (Main)Application.OpenForms["Main"];
                    if (main != null)
                    {
                        main.GetList();
                    }

                    this.Close();
                }               
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
