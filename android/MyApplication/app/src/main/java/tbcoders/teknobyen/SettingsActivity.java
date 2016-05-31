package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import java.util.Arrays;
import java.util.concurrent.ExecutionException;

import tbcoders.teknobyen.urlconnections.AuthenticateUser;

public class SettingsActivity extends AppCompatActivity {
    private static EditText username;
    private static EditText password;
    private static EditText roomNrField;
    private static EditText nameEdit;
    private static Button login_btn;
    private static Button cancel_btn;
    private static Button roomBTN;
    private final Integer[] roomNumbers = {201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211,
            301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327,
            401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 420, 421, 422, 423, 424, 425, 426, 427,
            501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512, 513, 514, 515, 516, 517, 518, 519, 520, 521, 522, 523,
            601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616};
    String userS = "";
    String userP = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        fillTextFields();
        loginButton();
        roomButton();

    }

    private void fillTextFields() {
        roomNrField = (EditText) findViewById(R.id.roomNrEdit);
        nameEdit = (EditText) findViewById(R.id.nameEdit);
        username = (EditText) findViewById(R.id.username);
        password = (EditText) findViewById(R.id.password);

        SharedPreferences prefs = getSharedPreferences("mypref", MODE_PRIVATE);
        nameEdit.setText(prefs.getString("personname", ""));
        roomNrField.setText(prefs.getString("roomnumber", ""));
        username.setText(Base64EncryptDecrypt.decrypt(prefs.getString("username", "")));
        password.setText(Base64EncryptDecrypt.decrypt(prefs.getString("password", "")));
        checkHttpResponse(username.getText().toString(), password.getText().toString());
    }


    public void loginButton() {
        username = (EditText) findViewById(R.id.username);
        password = (EditText) findViewById(R.id.password);
        login_btn = (Button) findViewById(R.id.acceptBTN);
        cancel_btn = (Button) findViewById(R.id.cancelBTN);

        login_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                //For å lagre variabelar slik at dei skal vere tilgjengelige etter å ha lukka appen.
                SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
                SharedPreferences.Editor editor = sharedPref.edit();
                if (username.getText().toString().length() > 0 && password.getText().toString().length() > 0) {
                    String userS = Base64EncryptDecrypt.encrypt(username.getText().toString());
                    //krypterar passord med Base64
                    String userP = Base64EncryptDecrypt.encrypt(password.getText().toString());
                    editor.putString("username", userS);
                    editor.putString("password", userP);
                    editor.apply();

                    if (checkHttpResponse(username.getText().toString(), password.getText().toString())) {
                        editor.putBoolean("authenticated", true);
                        editor.apply();
                        Toast.makeText(SettingsActivity.this, "Gyldig bruker er autorisert", Toast.LENGTH_SHORT).show();
                    } else {
                        editor.putBoolean("authenticated", false);
                        editor.apply();
                        Toast.makeText(SettingsActivity.this, "Brukernavn eller passord er feil, kan ikke autentisere!", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(SettingsActivity.this, "Feltene for brukernavn og passord kan ikke være tomme.", Toast.LENGTH_SHORT).show();
                }
            }
        });
        cancel_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
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
                if (roomNr.length() == 3 && personName.length() > 0) {
                    if (Arrays.asList(roomNumbers).contains(Integer.valueOf(roomNr))) {
                        SharedPreferences sharedPref = getSharedPreferences("mypref", MODE_PRIVATE);
                        SharedPreferences.Editor editor = sharedPref.edit();
                        editor.putString("roomnumber", roomNr);
                        editor.putString("personname", personName);
                        editor.apply();
                        // return to previous page
                        Toast.makeText(SettingsActivity.this, "Navn og Romnummer er opdatert", Toast.LENGTH_SHORT).show();
                    } else {
                        Toast.makeText(SettingsActivity.this, "Skriv inn gyldig romnummer", Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Toast.makeText(SettingsActivity.this, "Skriv navn og 3-sifret romnummer", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }


    private boolean checkHttpResponse(String username, String password) {
        AuthenticateUser authenticateUser = new AuthenticateUser();
        ImageView check = (ImageView) findViewById(R.id.authenticationCheck);
        try {
            String statusCode = authenticateUser.execute(username, password).get();
            if (statusCode.equals("200")) {
                System.out.println("Code is good");
                assert check != null;
                check.setBackgroundColor(getResources().getColor(R.color.accepted));
                return true;
            } else {
                System.out.println("Code is bad " + statusCode);
                assert check != null;
                check.setBackgroundColor(getResources().getColor(R.color.declined));
                return false;
            }
        } catch (InterruptedException | ExecutionException e) {
            e.printStackTrace();
        }
        return false;
    }

}
