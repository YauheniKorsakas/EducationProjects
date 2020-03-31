using System;

using Education.Xamarin.Models;

namespace Education.Xamarin.ViewModels {
    public class ItemDetailViewModel : BaseViewModel {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null) {
            Title = item?.Text;
            Item = item;
        }
    }
}
