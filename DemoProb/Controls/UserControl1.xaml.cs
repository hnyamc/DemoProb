using DemoProb.DB;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoProb.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private NavigationService _navigationService;
        private Service ser;
        private Action _isRemove;
        public UserControl1(Service service, Action isRemove)
        {
            InitializeComponent();
            ser = service;
            _isRemove = isRemove;
            TitleServiceTB.Text = ser.Title.ToString();

           
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "DemoProb/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

           
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));

            if (ser.Discount != null)
            {
               
                textDecorate.Text = $"{ser.Cost.Value.ToString("0.#")}";
                textDecorate.TextDecorations = TextDecorations.Strikethrough;

                CostAndTimeTB.Text = $"{((((double?)ser.Cost) - ((double?)ser.Cost) * ser.Discount / 100)).ToString()} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = $"* скидка {ser.Discount.ToString()}%";
            }
            else
            {
                CostAndTimeTB.Text = $"{ser.Cost.Value.ToString("0.#")} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = "";
            }

        }
        private void NavigateTo(object content)
        {
            Window window = Window.GetWindow(this);

            if (window == null)
                return;
            Frame mainFrame = LogicalTreeHelper.FindLogicalNode(window, "MainFrame") as Frame;
            mainFrame?.Navigate(content);
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            App.db.Service.Remove(ser);
            App.db.SaveChanges();
            _isRemove.Invoke();
            MessageBox.Show("Успешно удалено");

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            NavigateTo(new Pages.EditServicePage(ser));
        }


    }
}
