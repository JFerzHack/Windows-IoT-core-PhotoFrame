using PhotoFrame.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoFrame.Views.Dialogs
{
    public sealed partial class Explorer : ContentDialog
    {
       // public StorageItemTypes Type { get; set; }
        public StorageFolder Result { get; set; }

        public Explorer()
        {
            this.InitializeComponent();
          //  Type = type;
        }

        ExplorerHelper ExplorerH = new ExplorerHelper();


        private async void ContentDialog_Loading(FrameworkElement sender, object args)
        {
            itemList.ItemsSource = await UsbLister.List();
        }


        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Result = await ExplorerH.Return(itemList.SelectedItem.ToString());
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }



        private async void itemList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            //await ExplorerH.AddToPath(itemList.SelectedItem.ToString());
            itemList.ItemsSource = await ExplorerH.GetItems(itemList.SelectedItem.ToString());
            //path.Text = ExplorerH.Path;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ExplorerH.GoBack();
            itemList.ItemsSource = await ExplorerH.GetItems();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var Picker = new Windows.Storage.Pickers.FolderPicker();
            Picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            StorageFolder Folder = await Picker.PickSingleFolderAsync();

            if (Folder != null)
            {
                Result = Folder;
                Hide();
            }
            else
            {
            }
        }
    }
}
