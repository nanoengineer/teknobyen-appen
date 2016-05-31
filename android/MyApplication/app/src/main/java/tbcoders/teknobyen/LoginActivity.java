package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Toast;

import java.util.concurrent.ExecutionException;

import tbcoders.teknobyen.urlconnections.AuthenticateUser;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        addLoginButtonListener();
    }

    private void addLoginButtonListener() {
        Button loginButton = (Button) findViewById(R.id.loginLoginButton);
        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(checkIfValidAthentication()){
                    Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                    startActivity(intent);
                    finish();
                }
            }
        });
    }

    private boolean checkIfValidAthentication() {
        EditText username = (EditText) findViewById(R.id.loginUsernameText);
        EditText password = (EditText) findViewById(R.id.loginPasswordText);
        AuthenticateUser authenticateUser = new AuthenticateUser();

        assert password != null && username != null;
        if (username.getText().length() > 0 && password.getText().length() > 0) {
            try {
                String statusCode = authenticateUser.execute(username.getText().toString(), password.getText().toString()).get();
                if (statusCode.equals("200")) {
                    SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
                    SharedPreferences.Editor editor = sharedPref.edit();
                    editor.putBoolean("authenticated", true);
                    editor.putString("username", Base64EncryptDecrypt.encrypt(username.getText().toString()));
                    editor.putString("password", Base64EncryptDecrypt.encrypt(password.getText().toString()));
                    editor.apply();
                    System.out.println("Code is good");
                    return true;
                } else {
                    System.out.println("Code is bad " + statusCode);
                    Toast.makeText(LoginActivity.this, "Error, fikk ikke logget på!", Toast.LENGTH_SHORT).show();
                    return false;
                }
            } catch (InterruptedException | ExecutionException e) {
                e.printStackTrace();
            }
        }else{
            Toast.makeText(LoginActivity.this, "Fyll inn brukernavn og passord", Toast.LENGTH_SHORT).show();
            return false;
        }
        return false;
    }
}
