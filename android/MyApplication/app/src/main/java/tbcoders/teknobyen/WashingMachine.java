package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;

public class WashingMachine extends AppCompatActivity {
    private static ImageButton imageButton;
    private static Button loginbtn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_washing_machine);
        OnClickStatusListener();
        OnClickRefillListener();
        OnClickLoginListener();
    }

    private void OnClickLoginListener() {
        loginbtn = (Button)findViewById(R.id.alertLogin);
        loginbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(WashingMachine.this, LoginActivityWash.class);
                startActivity(intent);
            }
        });
    }

    public void OnClickStatusListener(){

        imageButton = (ImageButton) findViewById(R.id.BTN_washingS);
        imageButton.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        SharedPreferences myprefs = getSharedPreferences("mypref", 0);
                        String userS = myprefs.getString("username", "");
                        String userP = myprefs.getString("password", "");
                        if(userS.length()>0 && userP.length()>0){
                            Intent intent = new Intent(WashingMachine.this, MachineStatus.class);
                            startActivity(intent);
                        }else{
                            Intent intent = new Intent(WashingMachine.this, LoginActivityWash.class);
                            startActivity(intent);
                        }
                        //Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("http://129.241.152.11/LaundryState?lg=2&ly=9131"));

                    }
                }
        );
    }
    public void OnClickRefillListener(){
        imageButton = (ImageButton) findViewById(R.id.BTN_cardUse);
        imageButton.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        SharedPreferences myprefs = getSharedPreferences("mypref", 0);
                        String userS = myprefs.getString("username", "");
                        String userP = myprefs.getString("password", "");
                        if(userS.length()>0 && userP.length()>0){
                            Intent intent = new Intent(WashingMachine.this, MachineRefill.class);
                            startActivity(intent);
                        }else{
                            Intent intent = new Intent(WashingMachine.this, LoginActivityWash.class);
                            startActivity(intent);
                        }
                        //Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("http://129.241.152.11/SaldoForm?lg=2&ly=9131"));
                    }
                }
        );
    }
}
