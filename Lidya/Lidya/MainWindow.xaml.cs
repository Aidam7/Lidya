using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Lidya
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

        private void Window_Initialized(object sender, EventArgs e)
        {
            EnumWindows(new WindowEnumCallback(this.AddWnd), 0);
            foreach (string? window in Windows) {
                AvailableProgramsCombo.Items.Add(window);
            }
        }

        private void ExecuteActionButton_OnClick(object sender, EventArgs e)
        {
            // TODO: Open new window above the desired window
            string desiredWindow = (string)AvailableProgramsCombo.SelectedItem;
        }

        /* --- code from https://stackoverflow.com/a/10819641/16638833 --- */
        public delegate bool WindowEnumCallback(int hwnd, int lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(WindowEnumCallback lpEnumFunc, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(int h);

        private readonly List<string> Windows = new List<string>();
        private bool AddWnd(int hwnd, int lparam)
        {
            if (IsWindowVisible(hwnd)) {
                StringBuilder sb = new StringBuilder(255);
                GetWindowText(hwnd, sb, sb.Capacity);
                Windows.Add(sb.ToString());
            }
            return true;
        }
        /* --- End of the stolen code --- */
    }
}
