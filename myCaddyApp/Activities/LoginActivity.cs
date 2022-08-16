using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using Google.Android.Material.TextField;
using myCaddyApp.EventListeners;
using myCaddyApp.Fragments;
using myCaddyApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity
    {
        TextInputLayout emailText, passwordText;
        Button loginButton;
        TextView clickToRegisterTextView;
        FirebaseAuth auth;

        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();

        ProgressDialogFragment progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.login);

            emailText = (TextInputLayout)FindViewById(Resource.Id.emailLoginText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordLoginText);
            loginButton = (Button)FindViewById(Resource.Id.loginButton);

            clickToRegisterTextView = (TextView)FindViewById(Resource.Id.clickToRegister);

            loginButton.Click += loginButton_Click;

            clickToRegisterTextView.Click += clickToRegisterTextView_Click;

            auth = DataHelper.GetFirebaseAuth();

        }

        private void clickToRegisterTextView_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string email, password;

            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;

            if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please use a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 8)
            {
                Toast.MakeText(this, "Please use a password with at least 8 characters", ToastLength.Short).Show();
                return;

            }
            ShowProgressDialog("Verifying user...");
            //Sign in user
            auth.SignInWithEmailAndPassword(email, password).AddOnSuccessListener(taskCompletionListeners)
                .AddOnFailureListener(taskCompletionListeners);


            //Success or failuer event listeners
            taskCompletionListeners.Success += (success, args) =>
            {
                CloseProgressDialog();
                Toast.MakeText(this, "Login successful", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));
            };

            taskCompletionListeners.Failure += (success, args) =>
            {
                Toast.MakeText(this, "Login failed: " + args.Cause, ToastLength.Short).Show();
                CloseProgressDialog();
            };
        }

        public void ShowProgressDialog(string status)
        {
            progressDialog = new ProgressDialogFragment(status);
            var trans = SupportFragmentManager.BeginTransaction();
            progressDialog.Cancelable = false;
            progressDialog.Show(trans, "Progress");
        }
        public void CloseProgressDialog()
        {
            if (progressDialog != null)
            {
                progressDialog.Dismiss();
                progressDialog = null;
            }
        }
    }
}