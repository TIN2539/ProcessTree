using System.Windows;
using System.Windows.Controls;

namespace ProcessTree.ViewModels
{
    public sealed class BindableTreeView : TreeView
    {
        public static readonly DependencyProperty BindableSelectedItemProperty =
            DependencyProperty.Register("BindableSelectedItem", typeof(object), typeof(BindableTreeView), new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedSomeProcessProperty =
            DependencyProperty.Register("IsSelectedSomeProcess", typeof(bool), typeof(BindableTreeView), new PropertyMetadata(null));

        public BindableTreeView()
        {
            SelectedItemChanged += (sender, e) => BindableSelectedItem = SelectedItem;
            SelectedItemChanged += (sender, e) => IsSelectedSomeProcess = SelectedItem != null ? true : false;
        }

        public object BindableSelectedItem
        {
            get => GetValue(BindableSelectedItemProperty);
            set => SetValue(BindableSelectedItemProperty, value);
        }

        public bool IsSelectedSomeProcess
        {
            get { return (bool)GetValue(IsSelectedSomeProcessProperty); }
            set { SetValue(IsSelectedSomeProcessProperty, value); }
        }
    }
}