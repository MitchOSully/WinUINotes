using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage;
using WinUINotes.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUINotes.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotePage : Page
    {
        private Note m_noteModel;
        public NotePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Note note)
            {
                m_noteModel = note;
            }
            else
            {
                m_noteModel = new Note();
            }
        }

        private async void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (m_noteModel is not null)
            {
                await m_noteModel.SaveAsync();
            }
        }

        private async void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (m_noteModel is not null)
            {
                await m_noteModel.DeleteAsync();
            }

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
