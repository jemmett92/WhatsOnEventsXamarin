using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using WhatsOnEvents.Core.ViewModel;

namespace WhatsOnEvents.Droid.Fragments
{
    public class AddEventFragment : Fragment
    {
        private FloatingActionButton _floatingActionButton;

        public static AddEventFragment NewInstance()
        {
            var frag3 = new AddEventFragment {Arguments = new Bundle()};
            return frag3;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.AddEvent_fragment, null);

            _floatingActionButton = view.FindViewById<FloatingActionButton>(Resource.Id.floatingAddNewCounterButton);
            _floatingActionButton.SetCommand(nameof(FloatingActionButton.Click),
                ViewModelLocator.Counters.AddNewCounterCommand);

            return view;
        }
    }
}