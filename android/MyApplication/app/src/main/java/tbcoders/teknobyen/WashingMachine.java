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
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_washing_machine);
        OnClickStatusListener();
        OnClickRefillListener();
    }

    public void OnClickStatusListener(){
        imageButton = (ImageButton) findViewById(R.id.BTN_washingS);
        imageButton.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        SharedPreferences myprefs = getSharedPreferences("mypref", 0);
                        if(myprefs.getBoolean("authenticated", false)){
                            Intent intent = new Intent(WashingMachine.this, MachineStatusActivity.class);
                            startActivity(intent);
                        }else{
                            Intent intent = new Intent(WashingMachine.this, SettingsActivity.class);
                            startActivity(intent);
                        }
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
                        if(myprefs.getBoolean("authenticated", false)){
                            Intent intent = new Intent(WashingMachine.this, MachineRefill.class);
                            startActivity(intent);
                        }else{
                            Intent intent = new Intent(WashingMachine.this, SettingsActivity.class);
                            startActivity(intent);
                        }
                    }
                }
        );
    }
}
