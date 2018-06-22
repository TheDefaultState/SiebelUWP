using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Siebel_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //Loading nav items into menu 
        private void NavView_Loaded(Object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

            contentFrame.Navigated += On_Navigated;

            //Adding keyboard detection/accelorators
            KeyboardAccelerator GoBack = new KeyboardAccelerator();
            GoBack.Key = VirtualKey.GoBack;
            GoBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(GoBack);
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }

        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch(item.Tag)
            {
                case "Home":
                    contentFrame.Navigate(typeof(MainPage));
                    break;

                case "details":
                    contentFrame.Navigate(typeof(CustomerDetails));
                    break;

                case "contacts":
                    contentFrame.Navigate(typeof(Contacts));
                    break;

                case "profiles":
                    contentFrame.Navigate(typeof(Profiles));
                    break;

                case "premise":
                    contentFrame.Navigate(typeof(Premise));
                    break;

                case "idv":
                    contentFrame.Navigate(typeof(IDV));
                    break;

                case "addresses":
                    contentFrame.Navigate(typeof(Address));
                    break;

                case "creditvetting":
                    contentFrame.Navigate(typeof(CreditVetting));
                    break;

                case "orders":
                    contentFrame.Navigate(typeof(Orders));
                    break;
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            bool navigated = false;

            //Stops the user going back if the nav pane if overlayed
            if(NavView.IsPaneOpen && (NavView.DisplayMode == NavigationViewDisplayMode.Compact || NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
            {
                return false;
            }
            else
            {
                if(contentFrame.CanGoBack)
                {
                    navigated = true;
                }
            }
            return navigated;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = contentFrame.CanGoBack;

            if (contentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
            }

            else
            {
                Dictionary<Type, string> lookup = new Dictionary<Type, string>()
                {
                    {typeof(MainPage), "Home"},
                    {typeof(CustomerDetails), "details"},
                    {typeof(Contacts), "contacts"},
                    {typeof(Address), "addresses"},
                    {typeof(CreditVetting), "creditvetting"},
                    {typeof(IDV), "idv" },
                    {typeof(Premise), "premise" },
                    {typeof(Profiles), "profiles" },
                    {typeof(Orders), "orders" }
                };

                String stringtag = lookup[contentFrame.SourcePageType];

                //Sets the new form
                foreach (NavigationViewItemBase item in NavView.MenuItems)
                {
                    if (item is NavigationViewItem && item.Tag.Equals(stringtag))
                    {
                        item.IsSelected = true;
                        break;
                    }
                }
            }
        }
    }
}
