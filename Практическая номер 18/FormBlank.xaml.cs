using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Практическая_номер_18.ModelsDB;

namespace Практическая_номер_18
{
    /// <summary>
    /// Логика взаимодействия для FormBlank.xaml
    /// </summary>
    public partial class FormBlank : Window
    {
        public FormBlank()
        {
            InitializeComponent();
        }
        SessionContext _db = new SessionContext();
        SessionFirstCourseBt _sessionFirstCourseBt;
        
        private void WindowBlankLoaded(object sender, RoutedEventArgs e)
        {
            if (DataBaseSession.sessionFirstCourseBt == null && FlagsForForm.FlagAdd == true)
            {

                TheFormBlank.Title = "Добавить запись";
                btnFormAdd.Content = "Добавить";
                _sessionFirstCourseBt = new SessionFirstCourseBt();

            }
            else if (FlagsForForm.FlagView == true)
            {
                TheFormBlank.Title = "Посмотреть запись";
                btnFormAdd.Visibility = Visibility.Hidden;
                tbLastName.IsReadOnly = true;
                tbFirstName.IsReadOnly = true;
                tbSureName.IsReadOnly = true;
                tbMath.IsReadOnly = true ;
                tbGen.IsEnabled = false;
                tbtatus.IsEnabled = false;
                tbPhysical.IsReadOnly = true;
                tbLiterature.IsReadOnly = true;
                tbHistory.IsReadOnly=true;
                tbComputer.IsReadOnly= true;
                _sessionFirstCourseBt = _db.SessionFirstCourseBts.Find(DataBaseSession.sessionFirstCourseBt.IndexGroup);
            }
            else
            {

                TheFormBlank.Title = "Изменить запись";
                btnFormAdd.Content = "Изменить";
                _sessionFirstCourseBt = _db.SessionFirstCourseBts.Find(DataBaseSession.sessionFirstCourseBt.IndexGroup);
            }
            TheFormBlank.DataContext = _sessionFirstCourseBt;
        }

        private void btnFormAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text.Length == 0 || tbFirstName.Text.Length == 0 || tbSureName.Text.Length == 0 || tbComputer.Text.Length == 0 || tbHistory.Text.Length == 0 || tbMath.Text.Length == 0 || tbPhysical.Text.Length == 0 || tbLiterature.Text.Length == 0 )
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            StringBuilder errors = new StringBuilder();
            if (tbLastName.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (tbFirstName.Text.Length == 0) errors.AppendLine("Введите имя");
       
            if (int.TryParse(tbLiterature.Text, out int literature) && literature > 0 && literature >= 2 && literature <= 5) { } else errors.AppendLine("Некорректная оценка по литературе");
            if (int.TryParse(tbMath.Text, out int math) && math > 0 && math >=2 && math <=5) { } else errors.AppendLine("Некорректная оценка по математике");
            if (int.TryParse(tbHistory.Text, out int history) && history > 0 && history >= 2 && history <= 5) { } else errors.AppendLine("Некорректная оценка по истории");
            if (int.TryParse(tbComputer.Text, out int computer) && computer > 0 && computer >= 2 && computer <= 5) { } else errors.AppendLine("Некорректная оценка по информатике");
            if (int.TryParse(tbPhysical.Text, out int physical) && physical > 0 && physical >= 2 && physical <= 5) { } else errors.AppendLine("Некорректная оценка по физкультуре");
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString()); return;
            }
            try
            {
                if (DataBaseSession.sessionFirstCourseBt == null)
                {
                    _db.SessionFirstCourseBts.Add(_sessionFirstCourseBt);
                    _db.SaveChanges();
                }
                else
                {

                    _db.Entry(_sessionFirstCourseBt).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _db.SaveChanges();
                }
                MessageBox.Show("Информация сохранена");
                this.Close();
            }
            catch (Exception ex)
            {
                _db.SessionFirstCourseBts.Remove(_sessionFirstCourseBt);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
