package tbcoders.teknobyen;

import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageButton;

public class washingMachine extends AppCompatActivity {
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
                        //Intent intent = new Intent(washingMachine.this, machineStatus.class)
                        Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("http://129.241.152.11/LaundryState?lg=2&ly=9131"));
                        startActivity(intent);
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
                        //Intent intent = new Intent(washingMachine.this, machineStatus.class)
                        Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse("http://129.241.152.11/SaldoForm?lg=2&ly=9131"));
                        startActivity(intent);
                    }
                }
        );
    }
}
