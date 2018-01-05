using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Usb;
using Windows.Storage;

namespace PhotoFrame.Helpers
{
    class UsbLister
    {
        public async static Task<List<string>> List()
        {
            List<string> usb = new List<string>();

            foreach (StorageFolder device in await KnownFolders.RemovableDevices.GetFoldersAsync())
                usb.Add(device.Name);

            return usb;
        }
    }
}
