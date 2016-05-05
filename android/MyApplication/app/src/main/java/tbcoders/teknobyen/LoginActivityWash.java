package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivityWash extends AppCompatActivity {
    private static EditText username;
    private static EditText password;
    private static EditText roomNrField;
    String userS = "";
    String userP = "";
    private static Button login_btn;
    private static Button cancel_btn;
    private static Button roomBTN;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login_activity_wash);
        loginButton();
        roomButton();

    }
    public void loginButton(){
        username = (EditText)findViewById(R.id.username);
        password = (EditText)findViewById(R.id.password);
        login_btn = (Button)findViewById(R.id.acceptBTN);
        cancel_btn = (Button)findViewById(R.id.cancelBTN);

        login_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(username.getText().toString().length()>0 && password.getText().toString().length()>0){
                    //For å lagre variabelar slik at dei skal vere tilgjengelige etter å ha lukka appen.
                    SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
                    SharedPreferences.Editor editor = sharedPref.edit();
                    String userS = username.getText().toString();
                    String userP = password.getText().toString();
                    editor.remove("username");
                    editor.remove("password");
                    editor.putString("username", userS);
                    editor.putString("password", userP);
                    editor.commit();
                    Intent intent = new Intent(LoginActivityWash.this, WashingMachine.class);
                    startActivity(intent);
                }else{
                    Toast.makeText(LoginActivityWash.this, "Feltene for brukernavn og passord kan ikke være tomme", Toast.LENGTH_SHORT).show();
                }
            }
        });
        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(LoginActivityWash.this, WashingMachine.class);
                startActivity(intent);
            }
        });
    }
    public void roomButton(){
        roomBTN = (Button)findViewById(R.id.romNrBTN);
        roomNrField = (EditText)findViewById(R.id.roomNrEdit);
        roomBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
                SharedPreferences.Editor editor = sharedPref.edit();
                String roomNr = roomNrField.getText().toString();
                editor.remove("roomnumber");
                editor.putString("roomnumber", roomNr);
                editor.commit();
                Intent intent = new Intent(LoginActivityWash.this, MainActivity.class);
                startActivity(intent);
            }
        });
    }

}
