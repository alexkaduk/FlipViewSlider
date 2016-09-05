using FlipViewSlider.Model;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlipViewSlider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string[][] urls = new string[][]
            {
                new string[] { "google.com", "http://yandex.ru" },
                new string[] { "http://news.google.com", "http://news.yandex.ru" },
                new string[] { "http://gmail.com", "http://mail.yandex.ru" }
            };

        int itemsStep;

        List<SliderItem> sliderItems;

        public MainPage()
        {
            this.InitializeComponent();

            sliderItems = InitializeSliderItems(urls);
            itemsStep = urls[0].Length - 1;
        }

        private List<SliderItem> InitializeSliderItems(string[][] items)
        {
            List<SliderItem> sliderItems = new List<SliderItem>();
            int counter = 0;

            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < items[0].Length; j++)
                {
                    if (String.IsNullOrEmpty(items[i][j]))
                    {
                        items[i][j] = "about:blank";
                    }

                    if (!items[i][j].StartsWith("http://") && !items[i][j].StartsWith("https://"))
                    {
                        items[i][j] = "http://" + items[i][j];
                    }

                    Uri targetUri;

                    sliderItems.Add(new SliderItem
                    {
                        UrlAddress = items[i][j],
                        Uri = (Uri.TryCreate(urls[i][j], UriKind.Absolute, out targetUri)) ? targetUri : new Uri("about:blank"),
                        OrderId = counter
                    });

                    counter++;
                }
            }

            return sliderItems;
        }

        private void FlipView_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case VirtualKey.Up:
                    if ((FlipView.SelectedIndex - itemsStep) > 0)
                    {
                        FlipView.SelectedIndex -= itemsStep;
                    }
                    break;
                case VirtualKey.Down:
                    if ((FlipView.SelectedIndex + itemsStep) < sliderItems.Count)
                    {
                        FlipView.SelectedIndex += itemsStep;
                    }
                    break;
            }
        }
    }
}
