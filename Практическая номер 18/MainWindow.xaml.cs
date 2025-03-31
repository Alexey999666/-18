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
using System.Windows.Resources;
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
            MessageBox.Show("Результаты сессии на первом курсе кафедры ВТ. База данных должна содержать следующую информацию: индекс группы, фамилию, имя, отчество студента, пол студента, семейное положение и оценки по пяти экзаменам.", "Задание", MessageBoxButton.OK, MessageBoxImage.Question);
        }

        private void btnDeveloper_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Андрианов Алексей Вариант 14", "Разработчик", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        private void btnRequestInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Запрос 1 Находит студентов по полу 'Мужской' или 'Женский'\r\nЗапрос 2 Находит имена по первой букве\r\nЗапрос 3 Находит людей у которых по всем предметам одинаковые оценки\r\nЗапрос 4 Находит людей без отчества\r\nЗапрос 5 Определяет есть ли у выбраного по id студента искомая оценка, если нет то таблица будет пустой\r\nЗапрос 6 Обновление фамилии выбраного по id студента на ту которую указывает пользователь\r\nЗапрос 7 Обновление оценки выбраного по id студента по информатике на веденную пользователемм\r\nЗапрос 8 Удаление студента по id", "Запросы", MessageBoxButton.OK, MessageBoxImage.Question);
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
            tbFindgrade.Clear();
            tbIDforFind.Clear();
            tbIDforUpdExam.Clear();
        }
        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo cur = Application.GetResourceStream(new Uri("/Cursor/волщебная палочка.ani", UriKind.Relative));
            Cursor customCursor = new Cursor(cur.Stream);
            this.Cursor = customCursor;
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
            using (SessionContext _db = new SessionContext())
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
                if (int.TryParse(tbIDDel.Text, out int ID))
                {
                    int id = ID;
                    int Index = _db.Database.ExecuteSql($"Delete FROM SessionFirstCourseBt where IndexGroup = {id}");
                }
                else MessageBox.Show("Некоректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnFUpgrde_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                if (int.TryParse(tbIDforUpd.Text, out int ID))
                {
                    String famil = tbfamUpgrde.Text;
                    int id = ID;
                    var FAM = _db.Database.ExecuteSql($"Update SessionFirstCourseBt set Lastname = {famil} where IndexGroup = {id}");
                }
                else MessageBox.Show("Некоректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (int.TryParse(tbGradeAll.Text, out int all) && all > 1 && all < 6)
                {
                    int grade = all;
                    var ALL = _db.SessionFirstCourseBts.FromSql($"SELECT * FROM SessionFirstCourseBt WHERE MathGrade = {grade} AND ComputerScienceGrade = {grade} AND HistoryGrade = {grade} AND PhysicalEducationGrade = {grade} AND LiteratureGrade = {grade}");
                    DGDataBase.ItemsSource = ALL.ToList();
                }
                else MessageBox.Show("Некоректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btFiGrade_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                if (int.TryParse(tbIDforFind.Text, out int ID) && int.TryParse(tbFindgrade.Text, out int GR) && GR > 1 && GR<6 )
                {
                    int gr = GR;
                    int id = ID;
                    var IDGRADE = _db.SessionFirstCourseBts.FromSql($" SELECT * FROM SessionFirstCourseBt WHERE IndexGroup = {id} AND (MathGrade = {gr} OR ComputerScienceGrade = {gr} OR HistoryGrade = {gr} Or PhysicalEducationGrade = {gr} Or LiteratureGrade = {gr})  ");
                    DGDataBase.ItemsSource = IDGRADE.ToList();
                }
                else MessageBox.Show("Некоректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCUpgrde_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                if (int.TryParse(tbIDforUpdExam.Text, out int ID) && int.TryParse(tbCompUpgrde.Text, out int CoUp) && CoUp > 1 && CoUp<6)
                {
                    int Comp = CoUp;
                    int id = ID;
                    var COMPUPD = _db.Database.ExecuteSql($"Update SessionFirstCourseBt set ComputerScienceGrade = {Comp} where IndexGroup = {id}");
                }
                else MessageBox.Show("Некоректные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Entry E = new Entry();
            E.ShowDialog();
            if (DataBaseSession.Login == false) Close();
            if (DataBaseSession.Right == "Администратор")
            {

            }
            else if (DataBaseSession.Right == "Менеджер")
            {
                btnADD.IsEnabled = false;
                btnEdit.IsEnabled = false;
                ZP8.IsEnabled = false;
                btnDelete.IsEnabled = false;

            }
            else if (DataBaseSession.Right == "Зарегестрированный пользователь")
            {
                btnADD.IsEnabled = false;
                btnEdit.IsEnabled = false;
                ZP1.IsEnabled = true;
                ZP2.IsEnabled = true;
                ZP3.IsEnabled = true;
                ZP4.IsEnabled = true;
                ZP5.IsEnabled = true;
                ZP6.IsEnabled = false;
                ZP7.IsEnabled = false;
                ZP8.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            else
            {
                btnADD.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnFilt1.IsEnabled = false;
                btnFilt2.IsEnabled = false; 
                btnFind.IsEnabled = false;
                ZP1.IsEnabled = false;
                ZP2.IsEnabled = false;
                ZP3.IsEnabled = false;
                ZP4.IsEnabled = false;
                ZP5.IsEnabled = false;
                ZP6.IsEnabled = false;
                ZP7.IsEnabled = false;
                ZP8.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            mainWindow.Title = mainWindow.Title + " " + DataBaseSession.UserSurname + " " + DataBaseSession.UserName + " " + DataBaseSession.UserPatronymic + " (" + DataBaseSession.Right + ")"; 

        }
    }
}