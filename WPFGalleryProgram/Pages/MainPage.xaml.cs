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

namespace WPFGalleryProgram.Pages
{
    public partial class MainPage : Page
    {
        public List<ImageSource> ImageSources { get; set; }

        public MainPage()
        {
            InitializeComponent();

            ImageSources = new();
        }
        private void lbx_DragOver(object sender, DragEventArgs e)
        {
            bool dropEnabled = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames =
                                 e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in filenames)
                {
                    if (System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPG" && System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".JPEG" && System.IO.Path.GetExtension(filename).ToUpperInvariant() != ".PNG")
                    {
                        dropEnabled = false;
                        break;
                    }
                }
            }
            else
            {
                dropEnabled = false;
            }

            if (!dropEnabled)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }


        private void lbx_Drop(object sender, DragEventArgs e)
        {
            foreach (var filename in e.Data.GetData(DataFormats.FileDrop) as string[])
            {
                var image = new Image()
                {
                    Source = new BitmapImage(new Uri(filename)),
                    Width = 100,
                    Height = 100,
                    MinHeight = 70,
                    MinWidth = 70,
                    Stretch = Stretch.Uniform
                };

                lbx.Items.Add(image);

                ImageSources.Add(image.Source);

            }

        }

        private void lbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbx.SelectedItem is Image image)
            {
                PhotoPage photoPage = new(image.Source, ImageSources);

                NavigationService.Navigate(photoPage);

            }

        }
    }
}
