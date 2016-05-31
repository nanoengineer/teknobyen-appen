package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.annotation.BoolRes;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ImageView;

import java.util.concurrent.ExecutionException;

import tbcoders.teknobyen.urlconnections.AuthenticateUser;

public class StartupActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        SharedPreferences prefs = getSharedPreferences("mypref", MODE_PRIVATE);

        Boolean authorized = prefs.getBoolean("authenticated", false);
        System.out.println(authorized);
        if (authorized) {
            Intent intent = new Intent(StartupActivity.this, MainActivity.class);
            startActivity(intent);
        } else {
            Intent intent = new Intent(StartupActivity.this, LoginActivity.class);
            startActivity(intent);
        }
        finish();
    }
}
