using PhotoFrame.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoFrame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        public bool IsAnimationEnabled
        {
            get
            {
                if (localSettings.Values["Animation"] != null)
                {
                    if ((int)localSettings.Values["Animation"] == 1)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public double DelayValue
        {
            get
            {
                if (localSettings.Values["Interval"] != null)
                    return ((double)localSettings.Values["Interval"]);
                else
                    return 10;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;


        private void Page_Loading(FrameworkElement sender, object args)
        {
            interval_Slider.Value = DelayValue;
            animation_Switch.IsOn = IsAnimationEnabled;
        }

        public async void Start(StorageFolder folder)
        {
            foreach (StorageFile file in await folder.GetFilesAsync())
            {
                if (file.Name.EndsWith(".jpg") || file.Name.EndsWith(".png") || file.Name.EndsWith(".gif"))
                {
                    BitmapImage bImage = new BitmapImage();
                    await bImage.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));

                    img.Source = bImage;

                    if (IsAnimationEnabled && DelayValue <= 30)
                        zoom_Slow.Begin();

                    await Task.Delay((int)DelayValue * 1000);

                }
            }

            Start(folder);
        }


        private void img_Holding(object sender, HoldingRoutedEventArgs e)
        {
            if (e.HoldingState == Windows.UI.Input.HoldingState.Completed)
            {
                setup.Visibility = Visibility.Visible;
                img.Visibility = Visibility.Collapsed;

                continue_Button.Visibility = Visibility.Visible;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Explorer Xp = new Explorer();
            await Xp.ShowAsync();


            if (Xp.Result != null)
            {
                setup.Visibility = Visibility.Collapsed;
                img.Visibility = Visibility.Visible;

                Start(Xp.Result);
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;

            localSettings.Values["Interval"] = slider.Value;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch Switch = sender as ToggleSwitch;

            if (Switch.IsOn == true)
            {
                localSettings.Values["Animation"] = 1;
            }
            else
            {
                localSettings.Values["Animation"] = 1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            setup.Visibility = Visibility.Collapsed;
            img.Visibility = Visibility.Visible;
        }
    }
}
