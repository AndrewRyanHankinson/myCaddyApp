using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.Helpers
{
    public static class DataHelper
    {
        public static FirebaseFirestore GetFirestore()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseFirestore database;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("mycaddy-38c75")
                    .SetApplicationId("mycaddy-38c75")
                    .SetApiKey("AIzaSyDIQJ4CuOyqnzWMEGFm-IrPs_hMlCXe3_g")
                    .SetDatabaseUrl("https://mycaddy-38c75.firebaseio.com")
                    .SetStorageBucket("mycaddy-38c75.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseFirestore.GetInstance(app);
            }


            database = FirebaseFirestore.GetInstance(app);


            return database;
        }

        public static FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth auth;
            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("mycaddy-38c75")
                    .SetApplicationId("mycaddy-38c75")
                    .SetApiKey("AIzaSyDIQJ4CuOyqnzWMEGFm-IrPs_hMlCXe3_g")
                    .SetDatabaseUrl("https://mycaddy-38c75.firebaseio.com")
                    .SetStorageBucket("mycaddy-38c75.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                auth = FirebaseAuth.GetInstance(app);
            }


            auth = FirebaseAuth.GetInstance(app);

            return auth;
        }



    }
}