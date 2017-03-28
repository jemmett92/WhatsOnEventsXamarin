using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using WhatsOnEvents.Core;
using WhatsOnEvents.Core.ViewModel;

namespace WhatsOnEvents.Droid.Activities
{
    // This class derived from the baseactivity and points to the axml page new_counter
    [Activity(Label = "New Event")]
    public class NewCounterActivity : BaseActivity
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        private EditText _date;

        private EditText _description;

        private EditText _location;

        private EditText _name;
        public CounterViewModel ViewModel { get; private set; }
        public EditText Name => _name ?? (_name = FindViewById<EditText>(Resource.Id.new_counter_name));

        public EditText Description
            => _description ?? (_description = FindViewById<EditText>(Resource.Id.new_counter_description));

        public EditText Location => _location ?? (_location = FindViewById<EditText>(Resource.Id.new_counter_location));
        public EditText Date => _date ?? (_date = FindViewById<EditText>(Resource.Id.new_counter_date));

        protected override int LayoutResource => Resource.Layout.new_counter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = GetCounterViewModel();

            Bind();
        }

        protected virtual CounterViewModel GetCounterViewModel()
        {
            return new CounterViewModel(new Counter(),
                ViewModelLocator.DatabaseHelper,
                ViewModelLocator.DialogService,
                ViewModelLocator.NavigationService);
        }

        private void Bind()
        {
            _bindings.Add(this.SetBinding(() => ViewModel.Name, () => Name.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => ViewModel.Description, () => Description.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => ViewModel.Location, () => Location.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => ViewModel.Date, () => Date.Text, BindingMode.TwoWay));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            Toolbar.InflateMenu(Resource.Menu.new_counter_menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ViewModel.GoBackCommand.Execute(null);
                    return true;
                case Resource.Id.action_save_counter:
                    ViewModel.SaveCommand.Execute(null);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}