using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinUINotes.Models
{
    public class AllNotes
    {
        public ObservableCollection<Note> m_notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes()
        {
            LoadNotes();
        }

        public async void LoadNotes()
        {
            m_notes.Clear();
            // Get the folder where the notes are stored.
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            await GetFilesInFolderAsync(storageFolder);
        }

        private async Task GetFilesInFolderAsync(StorageFolder folder)
        {
            // Each StorageItem can be either a folder or a file.
            IReadOnlyList<IStorageItem> storageItems = await folder.GetItemsAsync();
            foreach (IStorageItem item in storageItems)
            {
                if (item.IsOfType(StorageItemTypes.Folder))
                {
                    // Recursively get items from subfolders
                    await GetFilesInFolderAsync((StorageFolder)item);
                }
                else if (item.IsOfType(StorageItemTypes.File))
                {
                    StorageFile file = (StorageFile)item;
                    Note note = new Note()
                    {
                        m_sFilename = file.Name,
                        m_sText = await FileIO.ReadTextAsync(file),
                        m_date = file.DateCreated.DateTime
                    };
                    m_notes.Add(note);
                }
            }
        }
    }
}
