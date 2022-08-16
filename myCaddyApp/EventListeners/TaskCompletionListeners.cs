using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.EventListeners
{
    public class TaskCompletionListeners : Java.Lang.Object, IOnSuccessListener, IOnFailureListener
    {

        public event EventHandler<TaskCompletionSuccessEventArgs> Success;
        public event EventHandler<TaskCompletionFailureEventArgs> Failure;



        public class TaskCompletionFailureEventArgs : EventArgs
        {
            public string Cause { get; set; }
        }

        public class TaskCompletionSuccessEventArgs : EventArgs
        {
            public Java.Lang.Object Result { get; set; }
        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Failure?.Invoke(this, new TaskCompletionFailureEventArgs { Cause = e.Message });
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Success?.Invoke(this, new TaskCompletionSuccessEventArgs { Result = result });
        }

    }

}
