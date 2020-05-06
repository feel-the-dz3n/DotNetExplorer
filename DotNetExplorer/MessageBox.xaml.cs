using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DotNetExplorer
{
    public class MessageBox : Window
    {
        public DialogButton? DialogResult;

        public MessageBox()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public MessageBox(string text, string title = ".NET Explorer", DialogButtons buttons = DialogButtons.OK) : this()
        {
            this.Title = title;
            this.FindControl<TextBlock>("TbText").Text = text;

            var panel = this.FindControl<WrapPanel>("PanelButtons");

            if (buttons == DialogButtons.OK)
                panel.Children.Add(CreateButton(DialogButton.OK));
            else if (buttons == DialogButtons.YesNo)
                panel.Children.AddRange(new Button[] {
                    CreateButton(DialogButton.Yes),
                    CreateButton(DialogButton.No)});
            else if (buttons == DialogButtons.YesNoCancel)
                panel.Children.AddRange(new Button[] {
                    CreateButton(DialogButton.Yes),
                    CreateButton(DialogButton.No),
                    CreateButton(DialogButton.Cancel)});
        }

        private Button CreateButton(DialogButton button)
        {
            var btn = new Button()
            {
                Content = button.ToString(),
                Tag = button,
                Margin = new Thickness(5, 0, 0, 0),
                MinWidth = 75
            };
            btn.Click += ResultButtonClick;
            return btn;
        }

        private void ResultButtonClick(object sender, RoutedEventArgs args)
        {
            var b = (Button)sender;
            if (b.Tag is DialogButton)
                DialogResult = (DialogButton)b.Tag;
            this.Close(b.Tag);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
