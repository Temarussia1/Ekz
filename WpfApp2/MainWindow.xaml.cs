using System;
using System.Windows;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private ЗаправочнаяСтанция заправочнаяСтанция;

        public MainWindow()
        {
            InitializeComponent();
            заправочнаяСтанция = new ЗаправочнаяСтанция();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Заправить_Click(object sender, RoutedEventArgs e)
        {
            double общееКоличествоТоплива;
            if (double.TryParse(txtОбщийОбъем.Text, out общееКоличествоТоплива))
            {
                заправочнаяСтанция.Хранилище.ОбщийОбъем = общееКоличествоТоплива;

                double текущийЗапасТоплива;
                if (double.TryParse(txtТекущийЗапас.Text, out текущийЗапасТоплива))
                {
                    заправочнаяСтанция.Хранилище.ТекущийЗапас = текущийЗапасТоплива;

                    double объемЗаправки;
                    if (double.TryParse(txtОбъемЗаправки.Text, out объемЗаправки))
                    {
                        заправочнаяСтанция.Заправить(объемЗаправки);
                        txtРезультат.Text = $"Текущий запас топлива: {заправочнаяСтанция.Хранилище.ТекущийЗапас} литров";
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат объема заправки.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат текущего запаса топлива.");
                }
            }
            else
            {
                MessageBox.Show("Неверный формат общего объема хранилища.");
            }
        }
    }
}
public class Топливо
{
    public double Объем { get; set; }
    public double Плотность { get; set; }

    public double ВычислитьМассу()
    {
        return Объем * Плотность;
    }
}

public class Хранилище
{
    public double ОбщийОбъем { get; set; }
    public double ТекущийЗапас { get; set; }

    public double ДоступныйОбъем()
    {
        return ОбщийОбъем - ТекущийЗапас;
    }
}

public class ЗаправочнаяСтанция
{
    public Хранилище Хранилище { get; set; }

    public void Заправить(double объемЗаправки)
    {
        if (объемЗаправки <= Хранилище.ДоступныйОбъем())
        {
            Хранилище.ТекущийЗапас += объемЗаправки;
            Console.WriteLine($"Заправлено {объемЗаправки} литров топлива.");
        }
        else
        {
            Console.WriteLine("Недостаточно места в хранилище для указанного объема заправки.");
        }
    }

    public void ДобавитьТопливо(Топливо топливо)
    {
        Заправить(топливо.Объем);
    }

    public void ИзвлечьТопливо(double объемИзвлечения)
    {
        if (объемИзвлечения <= Хранилище.ТекущийЗапас)
        {
            Хранилище.ТекущийЗапас -= объемИзвлечения;
            Console.WriteLine($"Извлечено {объемИзвлечения} литров топлива.");
        }
        else
        {
            Console.WriteLine("Недостаточно топлива в хранилище для указанного объема извлечения.");
        }
    }
}