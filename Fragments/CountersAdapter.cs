using System.Collections.Specialized;
using Android.Support.V7.Widget;
using Android.Views;
using WhatsOnEvents.Core.ViewModel;

namespace WhatsOnEvents.Droid.Fragments
{
    //Adapters are just a view model for the recycling view, it tells the view how many items it contains and allows the recycling nature to be employed. 
    public class CountersAdapter : RecyclerView.Adapter
    {
        public CountersAdapter()
        {
            ((INotifyCollectionChanged) ViewModelLocator.Counters.Counters).CollectionChanged += OnCollectionChanged;
        }

        //Returns the count of how many items are in the collection 
        public override int ItemCount => ViewModelLocator.Counters.Counters.Count;

        private void OnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            NotifyDataSetChanged();
        }

        //OnBindViewHolder is responsible for updating the view holder to reflect the relevant item in the collection
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = ViewModelLocator.Counters.Counters[position];
            ((CounterViewHolder) holder).BindCounterViewModel(item);
        }

        //Oncreateviewholder is called whenever on the creation of the recycler view the first time it happens.
        //This needs to create a view and wrap it in a class derived from recyclerview.viewholder
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.counter_view, parent, false);
            return new CounterViewHolder(itemView);
        }
    }
}