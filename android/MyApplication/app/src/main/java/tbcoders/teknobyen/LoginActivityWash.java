package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import java.util.Arrays;

public class LoginActivityWash extends AppCompatActivity {
    private static EditText username;
    private static EditText password;
    private static EditText roomNrField;
    private static EditText nameEdit;
    String userS = "";
    String userP = "";
    private static Button login_btn;
    private static Button cancel_btn;
    private static Button roomBTN;
    private final Integer[] roomNumbers = {201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211,
            301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327,
            401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427,
            501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523,
            601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616};

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login_activity_wash);
        loginButton();
        roomButton();

    }

    public void loginButton() {
        username = (EditText) findViewById(R.id.username);
        password = (EditText) findViewById(R.id.password);
        login_btn = (Button) findViewById(R.id.acceptBTN);
        cancel_btn = (Button) findViewById(R.id.cancelBTN);

        login_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (username.getText().toString().length() > 0 && password.getText().toString().length() > 0) {
                    //For å lagre variabelar slik at dei skal vere tilgjengelige etter å ha lukka appen.
                    SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
                    SharedPreferences.Editor editor = sharedPref.edit();
                    String userS = Base64EncryptDecrypt.encrypt(username.getText().toString());
                    //krypterar passord med Base64
                    String userP = Base64EncryptDecrypt.encrypt(password.getText().toString());
                    editor.remove("username");
                    editor.remove("password");
                    editor.putString("username", userS);
                    editor.putString("password", userP);
                    editor.commit();
                    Intent intent = new Intent(LoginActivityWash.this, WashingMachine.class);
                    startActivity(intent);
                } else {
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

    public void roomButton() {
        roomBTN = (Button) findViewById(R.id.romNrBTN);
        roomNrField = (EditText) findViewById(R.id.roomNrEdit);
        nameEdit = (EditText) findViewById(R.id.nameEdit);
        roomBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String personName = nameEdit.getText().toString();
                String roomNr = roomNrField.getText().toString();
                if (Arrays.asList(roomNumbers).contains(Integer.valueOf(roomNr)) && personName.length()>0) {
                    SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
                    SharedPreferences.Editor editor = sharedPref.edit();
                    editor.remove("roomnumber");
                    editor.putString("roomnumber", roomNr);
                    editor.putString("personname", personName);
                    editor.commit();
                    Intent intent = new Intent(LoginActivityWash.this, MainActivity.class);
                    startActivity(intent);
                } else {
                    Toast.makeText(LoginActivityWash.this, "Skriv inn gyldig romnummer", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }


}
