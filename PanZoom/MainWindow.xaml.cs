using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Zooms towards the center only
    /// </summary>
    public partial class MainWindow : Window
    {
        Point start;
        Point origin;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void image1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //var st = (ScaleTransform)image1.RenderTransform;
            var st = Scale;
            double zoom = e.Delta > 0 ? 1.1 : 0.9;
            st.ScaleX *= zoom;
            st.ScaleY *= zoom;
            if (st.ScaleX < 1)
            {
                st.ScaleX = 1;
                st.ScaleY = 1;
            }
        }


        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            image1.CaptureMouse();
            var tt = (TranslateTransform)((TransformGroup)image1.RenderTransform)
                .Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(border);
            origin = new Point(tt.X, tt.Y);
        }

        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            if (image1.IsMouseCaptured)
            {
                var tt = (TranslateTransform)((TransformGroup)image1.RenderTransform)
                    .Children.First(tr => tr is TranslateTransform);
                Vector v = start - e.GetPosition(border);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;
            }
        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image1.ReleaseMouseCapture();
        }

        private void image1_MouseMiddleButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                var st = Scale;
                st.ScaleX = 1;
                st.ScaleY = 1;
            }
        }

    }
}
