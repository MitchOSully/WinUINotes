using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinUINotes.Models
{
    public class Note
    {
        private StorageFolder m_storageFolder = ApplicationData.Current.LocalFolder;
        public string m_sFilename { get; set; } = string.Empty;
        public string m_sText { get; set; } = string.Empty;
        public DateTime m_date { get; set; } = DateTime.Now;

        public Note()
        {
            m_sFilename = "notes" + DateTime.Now.ToBinary().ToString() + ".txt";
        }

        public async Task SaveAsync()
        {
            // Save the note to a file
            StorageFile noteFile = (StorageFile)await m_storageFolder.TryGetItemAsync(m_sFilename);
            if (noteFile is null)
            {
                noteFile = await m_storageFolder.CreateFileAsync(m_sFilename, CreationCollisionOption.ReplaceExisting);
            }
            await FileIO.WriteTextAsync(noteFile, m_sText);
        }

        public async Task DeleteAsync()
        {
            // Delete the ntoe form the file system
            StorageFile noteFile = (StorageFile)await m_storageFolder?.TryGetItemAsync(m_sFilename);
            if (noteFile is not null)
            {
                await noteFile.DeleteAsync();
            }
        }
    }
}
