using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Google.Android.Material.TextField;
using Java.Util;
using myCaddyApp.EventListeners;
using myCaddyApp.Fragments;
using myCaddyApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class RegisterActivity : AppCompatActivity
    {


        TextInputLayout nameText;
        TextInputLayout emailText;
        TextInputLayout passwordText;
        TextInputLayout passwordConfirmText;

        Button registerButton;
        TextView clickToLoginTextView;

        FirebaseFirestore database;
        FirebaseAuth mAuth;

        string name, email, password, confirm;

        TaskCompletionListeners taskCompletion = new TaskCompletionListeners();


        ProgressDialogFragment progressDialog;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);

            nameText = (TextInputLayout)FindViewById(Resource.Id.nameRegText);
            emailText = (TextInputLayout)FindViewById(Resource.Id.emailRegText);
            passwordText = (TextInputLayout)FindViewById(Resource.Id.passwordRegText);
            passwordConfirmText = (TextInputLayout)FindViewById(Resource.Id.passwordConfirmRegText);

            registerButton = (Button)FindViewById(Resource.Id.registerButton);
            registerButton.Click += RegisterButton_Click;

            clickToLoginTextView = (TextView)FindViewById(Resource.Id.clickToLogin);
            clickToLoginTextView.Click += clickToLoginTextView_Click;
            database = DataHelper.GetFirestore();
            mAuth = FirebaseAuth.Instance;
        }

        private void clickToLoginTextView_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(LoginActivity));
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            
            name = nameText.EditText.Text;
            email = emailText.EditText.Text;
            password = passwordText.EditText.Text;
            confirm = passwordConfirmText.EditText.Text;

            if (name.Length < 4)
            {
                Toast.MakeText(this, "Please enter a valid name", ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email address", ToastLength.Short).Show();
                return;
            }
            else if (password.Length < 8)
            {
                Toast.MakeText(this, "Please enter a password up to 8 characters long", ToastLength.Short).Show();
                return;
            }
            else if (password != confirm)
            {
                Toast.MakeText(this, "Passwords do not match", ToastLength.Short).Show();
                return;
            }



            ShowProgressDialog("Signing Up...");

            mAuth.CreateUserWithEmailAndPassword(email, password).AddOnSuccessListener(this, taskCompletion)
                .AddOnFailureListener(this, taskCompletion);



            // Registration success
            taskCompletion.Success += (success, args) =>
            {
                CloseProgressDialog();
                HashMap userMap = new HashMap();
                userMap.Put("email", email);
                userMap.Put("fullname", name);

                DocumentReference userReeference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
                userReeference.Set(userMap);
                StartActivity(typeof(LoginActivity));
            };

            //Registration failure
            taskCompletion.Failure += (failure, args) =>
            {
                CloseProgressDialog();
                Toast.MakeText(this, "Registration Failed : " + args.Cause, ToastLength.Short).Show();
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