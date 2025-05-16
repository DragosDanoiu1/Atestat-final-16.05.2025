using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Threading.Tasks;

namespace Atestat4.Views
{
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        }

        // Trimitere mesaj la click sau Enter
        async void OnSendClicked(object sender, EventArgs e)
        {
            var text = InputEntry.Text?.Trim();
            if (string.IsNullOrEmpty(text))
                return;

            // 1) Mesajul utilizatorului
            var userFrame = CreateMessageFrame(text, isUser: true);
            MessagesStack.Children.Add(userFrame);
            await Task.Yield();
            await ((ScrollView)MessagesStack.Parent)
                .ScrollToAsync(userFrame, ScrollToPosition.End, false);
            InputEntry.Text = "";

            // 2) Răspuns AI simulativ
            await Task.Delay(300);
            var aiResponse = $"AI: I heard you say “{text}”";
            var aiFrame = CreateMessageFrame(aiResponse, isUser: false);
            MessagesStack.Children.Add(aiFrame);
            await Task.Yield();
            await ((ScrollView)MessagesStack.Parent)
                .ScrollToAsync(aiFrame, ScrollToPosition.End, false);
        }

        // Butonul Back din header
        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Navighează absolut la HomePage (ruta definită în AppShell)
            await Shell.Current.GoToAsync("//home");
        }

        // Creează balon pentru mesaj
        Frame CreateMessageFrame(string message, bool isUser)
        {
            return new Frame
            {
                BackgroundColor = isUser
                    ? Color.FromArgb("#DCF8C6")
                    : Color.FromArgb("#EAEAEA"),
                CornerRadius = 12,
                Padding = 10,
                HasShadow = false,
                MaximumWidthRequest = 300,
                HorizontalOptions = isUser
                    ? LayoutOptions.End
                    : LayoutOptions.Start,
                Content = new Label
                {
                    Text = message,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    LineBreakMode = LineBreakMode.WordWrap,
                    HorizontalTextAlignment = TextAlignment.Start
                }
            };
        }
    }
}
