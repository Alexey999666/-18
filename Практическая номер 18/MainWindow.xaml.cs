using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Практическая_номер_18.ModelsDB;

namespace Практическая_номер_18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Учет изделий, собранных в цехе за неделю. База данных должна содержать следующую \r\nинформацию: фамилию, имя, отчество сборщика, количество изготовленных изделий за \r\nкаждый день недели раздельно, название цеха, а также тип изделия и его стоимость.\r\n", "Задание", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void btnDeveloper_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Андрианов Алексей Вариант 14", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        private void btnRequestInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("", "Запросы", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbFiltr1.Clear();
            tbFiltr2.Clear();
            tbPoisk.Clear();
            tbIDDel.Clear();
            tbGradeAll.Clear();
            tbfamUpgrde.Clear();
            tbIDforUpd.Clear();
            tbnNameFind.Clear();
            tbCompUpgrde.Clear();
            tbCOUNTgrade.Clear();
            tbIDforCount.Clear();
            tbIDforUpdExam.Clear();
        }
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            LoudDataBaseDG();
        }
        void LoudDataBaseDG()
        {
            using (SessionContext _DB = new SessionContext())
            {
                int SelectedIndex = DGDataBase.SelectedIndex;
                DGDataBase.ItemsSource = _DB.SessionFirstCourseBts.ToList();
                if (SelectedIndex != -1)
                {
                    if (SelectedIndex >= DGDataBase.Items.Count) SelectedIndex = DGDataBase.Items.Count - 1;
                    DGDataBase.SelectedIndex = SelectedIndex;
                    DGDataBase.ScrollIntoView(DGDataBase.SelectedItem);
                }
                DGDataBase.Focus();
            }
        }

        private void btnAddEntry(object sender, RoutedEventArgs e)
        {
            FlagsForForm.FlagAdd = true;
            DataBaseSession.sessionFirstCourseBt = null;
            FormBlank f = new FormBlank();
            f.Owner = this;
            f.ShowDialog();
            LoudDataBaseDG();
           

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DGDataBase.SelectedItem != null)
            {
                FlagsForForm.FlagEdit = true;
                DataBaseSession.sessionFirstCourseBt = (SessionFirstCourseBt)DGDataBase.SelectedItem;
                FormBlank f = new FormBlank();
                f.Owner = this;
                f.ShowDialog();
                LoudDataBaseDG();
                
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SessionFirstCourseBt row = (SessionFirstCourseBt)DGDataBase.SelectedItem;
                    //Изделия row = (Изделия)DGDataBase.SelectedValue;
                    //int indexRow = DGDataBase.SelectedIndex;
                    if (row != null)
                    {
                        using (SessionContext _db = new SessionContext())
                        {
                            _db.SessionFirstCourseBts.Remove(row);
                            _db.SaveChanges();
                        }
                        LoudDataBaseDG();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка заполнения");
                }

            }
            else DGDataBase.Focus();
        }

        private void btnFiltered1_Click(object sender, RoutedEventArgs e)
        {
            if (tbFiltr1.Text.IsNullOrEmpty() == false)
            {
                using (SessionContext _db = new SessionContext())
                {
                    var filtered = _db.SessionFirstCourseBts.Where(p => p.Gender == tbFiltr1.Text);
                    DGDataBase.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoudDataBaseDG();
            }
        }
        private void btnFiltered2_Click(object sender, RoutedEventArgs e)
        {
            if (tbFiltr2.Text.IsNullOrEmpty() == false)
            {
                using (SessionContext _db = new SessionContext())
                {
                    var filtered = _db.SessionFirstCourseBts.Where(p => p.FamilyStatus == tbFiltr2.Text);
                    DGDataBase.ItemsSource = filtered.ToList();
                }
            }
            else
            {
                LoudDataBaseDG();
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<SessionFirstCourseBt> listItem = (List<SessionFirstCourseBt>)DGDataBase.ItemsSource;
            var filtered = listItem.Where(p => p.Lastname.Contains(tbPoisk.Text) || (!string.IsNullOrEmpty(p.Middlename) ? p.Middlename.Contains(tbPoisk.Text) : false));
            if (filtered.Count() > 0)
            {
                var item = filtered.First();
                DGDataBase.SelectedItem = item;
                DGDataBase.ScrollIntoView(item);
                DGDataBase.Focus();
            }
        }

        private void btnViewing_Click(object sender, RoutedEventArgs e)
        {
            if (DGDataBase.SelectedItem != null)
            {
                FlagsForForm.FlagView = true;
                DataBaseSession.sessionFirstCourseBt = (SessionFirstCourseBt)DGDataBase.SelectedItem;
                FormBlank f = new FormBlank();
                f.Owner = this;
                f.ShowDialog();
                LoudDataBaseDG();
                
            }
        }

        private void btnGenderFind_Click(object sender, RoutedEventArgs e)
        {
            using(SessionContext _db = new SessionContext())
            {

                String? gender = cbGen.Text;
               
                var gen = _db.SessionFirstCourseBts.FromSql($"Select * From SessionFirstCourseBt where Gender = {gender}");
                DGDataBase.ItemsSource = gen.ToList();
            }
        }

        private void btnDeleteID_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String id = tbIDDel.Text;
                int Index = _db.Database.ExecuteSql($"Delete FROM SessionFirstCourseBt where IndexGroup = {id}");
               
            }
        }

        private void btnFUpgrde_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String famil = tbfamUpgrde.Text;
                String id = tbIDforUpd.Text;
                var FAM = _db.Database.ExecuteSql($"Update SessionFirstCourseBt set Lastname = {famil} where IndexGroup = {id}");
            }
        }

        private void btnNFind_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String name = tbnNameFind.Text + "%";
                var Nam = _db.SessionFirstCourseBts.FromSql($"SELECT * FROM SessionFirstCourseBt WHERE Firstname Like {name}");
                DGDataBase.ItemsSource = Nam.ToList();
            }
        }

        private void btALLGrade_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String grade = tbGradeAll.Text;
                var ALL = _db.SessionFirstCourseBts.FromSql($"SELECT * FROM SessionFirstCourseBt WHERE MathGrade = {grade} AND ComputerScienceGrade = {grade} AND HistoryGrade = {grade} AND PhysicalEducationGrade = {grade} AND LiteratureGrade = {grade}");
                DGDataBase.ItemsSource = ALL.ToList();
            }
        }

        private void btNULLPatronic_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                
                var NULLPAT = _db.SessionFirstCourseBts.FromSql($"SELECT * FROM SessionFirstCourseBt WHERE Middlename IS NULL");
                DGDataBase.ItemsSource = NULLPAT.ToList();
            }
        }

        private void btCOGrade_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String gr = tbCOUNTgrade.Text;
                String id = tbIDforCount.Text;
                var IDGRADE = _db.SessionFirstCourseBts.FromSql($" SELECT * FROM SessionFirstCourseBt WHERE IndexGroup = {id} AND (MathGrade = {gr} OR ComputerScienceGrade = {gr} OR HistoryGrade = {gr} Or PhysicalEducationGrade = {gr} Or LiteratureGrade = {gr})  ");
                DGDataBase.ItemsSource = IDGRADE.ToList();
            }
        }

        private void btnCUpgrde_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                String Comp = tbCompUpgrde.Text;
                String id = tbIDforUpdExam.Text;
                var COMPUPD = _db.Database.ExecuteSql($"Update SessionFirstCourseBt set ComputerScienceGrade = {Comp} where IndexGroup = {id}");
            }
        }
    }
}