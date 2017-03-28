using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;

namespace WhatsOnEvents.Droid.Fragments
{
    public class SortEventFragment : Fragment
    {
        private RecyclerView _recyclerView;

        public static SortEventFragment NewInstance()
        {
            return new SortEventFragment {Arguments = new Bundle()};
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.SortEventFragment, null);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.countersRecyclerView);
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false));
            _recyclerView.SetAdapter(new CountersAdapter());


            return view;
        }
    }
}