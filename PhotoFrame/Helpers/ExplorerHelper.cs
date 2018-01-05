using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PhotoFrame.Helpers
{
    public class ExplorerHelper
    {
        //Removed all not needed code as this gets only folders

        public StorageFolder Current { get; set; }
       // public StorageItemTypes Type { get; set; }

        public ExplorerHelper()
        {
           // Path = ApplicationData.Current.LocalFolder.Path;
        }

        public void GoForward()
        {

        }

        public void GoBack()
        {

        }
        

        public async Task<List<string>> GetItems()
        {
            List<string> items = new List<string>();

            foreach(StorageFolder item in await Current.GetFoldersAsync())
            {
                items.Add(item.Name);
            }

            return items;
        }

        public async Task<List<string>> GetItems(string name)
        {
            if(Current == null)
                Current = await KnownFolders.RemovableDevices.GetFolderAsync(name);
            else
            {
                StorageFolder folder = await Current.GetFolderAsync(name);
                Current = folder;
            }
                
            return await GetItems();

                 /*
            List<string> usb = new List<string>();
            StorageFolder f = await KnownFolders.RemovableDevices.GetFolderAsync(name);

            foreach (StorageFolder device in await f.GetFoldersAsync())
                usb.Add(device.Name);

            return usb;*/
            
        }

        public async Task<StorageFolder> Return(string name)
        {
            StorageFolder item = await Current.GetFolderAsync(name);
            
           return item;
        }
    }
}
