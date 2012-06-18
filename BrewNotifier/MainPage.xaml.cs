using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BrewNotifier
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int ImageIndex { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void HandleBrewClick(object sender, RoutedEventArgs e)
        {
            // make up some coffee...
            var barista = new Barista();
            var result = barista.BrewMeAnything();

            // share the result...
            var dialog = new MessageDialog(result.ToString());
            await dialog.ShowAsync();
        }

        private void HandleBrewClickWithToast(object sender, RoutedEventArgs e)
        {
            // make up some coffee...
            var barista = new Barista();
            var result = barista.BrewMeAnything();

            // get the XML...
            var xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);

            // add some audio...
            var audio = xml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.IM");
            var toast = xml.SelectSingleNode("//toast");
            toast.AppendChild(audio);

            // replace the contents of 'text'...
            var text = toast.SelectSingleNode("//text");
            text.InnerText = string.Format("Here's your drink:\r\n{0}", result);

            // ask the OS to show it...
            var notification = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        private void HandleBrewClickWithTile(object sender, RoutedEventArgs e)
        {
            // make up some coffee...
            var barista = new Barista();
            var result = barista.BrewMeAnything();

            // update...
            UpdateLargeTile(result);
            UpdateSmallTile(result);
        }

        private void UpdateLargeTile(DeliciousDrink drink)
        {
            // get the XML...
            var xml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideImageAndText01);

            // replace the contents of 'text'...
            var tile = xml.SelectSingleNode("//tile");
            var text = tile.SelectSingleNode("//text");
            text.InnerText = drink.ToString();

            // put an image in...
            var image = tile.SelectSingleNode("//image");
            ImageIndex++;
            if (ImageIndex == 3)
                ImageIndex = 0;
            var url = string.Format("ms-appx:///Assets/Latte{0}.jpg", ImageIndex + 1);
            image.Attributes.GetNamedItem("src").NodeValue = url;

            // ask the OS to show it...
            var notification = new TileNotification(xml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void UpdateSmallTile(DeliciousDrink drink)
        {
            // get the XML...
            var xml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText04);

            // replace the contents of 'text'...
            var tile = xml.SelectSingleNode("//tile");
            var text = tile.SelectSingleNode("//text");
            text.InnerText = drink.ToString();

            // ask the OS to show it...
            var notification = new TileNotification(xml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
        }

        private void HandleBrewClickWithBadge(object sender, RoutedEventArgs e)
        {
            var xml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);

            // element...
            var badge = xml.SelectSingleNode("//badge");
            badge.Attributes.GetNamedItem("value").NodeValue = new Random().Next(100).ToString();

            // show...
            var notification = new BadgeNotification(xml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(notification);
        }
    }
}
