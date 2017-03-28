using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using WhatsOnEvents.Core.ViewModel;

namespace WhatsOnEvents.Droid.Fragments
{
    //This class derives from RecyclerView.ViewHolder
    public class CounterViewHolder : RecyclerView.ViewHolder
    {
        private readonly TextView _date;
        private readonly TextView _description;
        private readonly TextView _location;
        private readonly TextView _name;


        private CounterViewModel _counterViewModel;

        // Cardview holder, with longclick that leads to edit event layout
        public CounterViewHolder(View itemView) : base(itemView)
        {
            _name = itemView.FindViewById<TextView>(Resource.Id.counter_name);
            _description = itemView.FindViewById<TextView>(Resource.Id.counter_description);
            _location = itemView.FindViewById<TextView>(Resource.Id.counter_location);
            _date = itemView.FindViewById<TextView>(Resource.Id.counter_date);

            itemView.LongClick += ItemLongClick;
        }

        private void ItemLongClick(object sender, View.LongClickEventArgs e)
        {
            _counterViewModel.EditCommand.Execute(null);
        }

        //This method takes a CounterViewModel that refers to the item in the relevant position in the collection,
        //and this is stored in a field.
        public void BindCounterViewModel(CounterViewModel counterViewModel)
        {
            _counterViewModel = counterViewModel;
            _name.Text = counterViewModel.Name;
            _description.Text = counterViewModel.Description;
            _location.Text = counterViewModel.Location;
            _date.Text = counterViewModel.Date;
        }
    }
}