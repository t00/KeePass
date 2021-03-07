using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KeePassCore.Views
{
    public class GroupTreeView : UserControl
    {
        public GroupTreeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}