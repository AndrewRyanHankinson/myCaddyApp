using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.Fragments
{
    public class ProgressDialogFragment : AndroidX.Fragment.App.DialogFragment
    {
        string status;
        public ProgressDialogFragment(string _status)
        {
            status = _status;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.progress, container, false);

            TextView statusTextView = (TextView)view.FindViewById<TextView>(Resource.Id.progressStatus);
            statusTextView.Text = status;
            return view;
        }


    }
}