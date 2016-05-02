package tbcoders.teknobyen;

import android.content.Intent;
import android.media.MediaPlayer;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageButton;

public class MainActivity extends AppCompatActivity {
    private static ImageButton washingButton;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        OnClickWashingButtonListener();

        final MediaPlayer stemmerDetSound = MediaPlayer.create(this, R.raw.stemmerlyd);
        ImageButton playStemmerDetSound = (ImageButton) this.findViewById(R.id.btn_stemmer_det);
        playStemmerDetSound.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                stemmerDetSound.start();
            }
        });
    }
    public void OnClickWashingButtonListener(){
        washingButton = (ImageButton) findViewById(R.id.BTN_washMachine);
        washingButton.setOnClickListener(
                new View.OnClickListener() {
                     @Override
                     public void onClick(View view) {
                         Intent intent = new Intent(MainActivity.this, WashingMachine.class);
                         startActivity(intent);
                     }
                 }
        );
    }
}
