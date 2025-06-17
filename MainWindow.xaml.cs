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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Ingredient
        {
            public string Name { get; set; }
            public double Cost { get; set; }
        }

        private List<Ingredient> _ingredients = new List<Ingredient>();
        private double _totalCost = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Парсим введенные данные
            if (!double.TryParse(Price.Text, out double price) ||
                !double.TryParse(Quantity.Text, out double quantity))
            {
                MessageBox.Show("Ошибка ввода чисел!");
                return;
            }

            string unit = (Unit.SelectedItem as ComboBoxItem).Content.ToString();
            string name = IngredientName.Text;

            // Пересчитываем стоимость в зависимости от единиц
            double cost = price * quantity;
            switch (unit)
            {
                case "г": cost = price * (quantity / 1000); break; // цена за кг → переводим в граммы
                case "мл": cost = price * (quantity / 1000); break; // цена за литр → переводим в мл
                case "шт": cost = price * quantity; break; // цена за штуку
                                                           // кг и л — без изменений
            }

            // Добавляем в список
            _ingredients.Add(new Ingredient { Name = name, Cost = cost });
            _totalCost += cost;

            // Обновляем интерфейс
            IngredientsList.ItemsSource = null;
            IngredientsList.ItemsSource = _ingredients;
            TotalCost.Text = $"{_totalCost:F2} руб.";

            // Очищаем поля ввода
            IngredientName.Text = "";
            Price.Text = "";
            Quantity.Text = "";
        }
    }
}
