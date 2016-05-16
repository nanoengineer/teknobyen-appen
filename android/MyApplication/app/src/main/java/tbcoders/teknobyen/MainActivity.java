package tbcoders.teknobyen;

import android.content.Intent;
import android.media.MediaPlayer;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageButton;

public class MainActivity extends AppCompatActivity {
    private static ImageButton imgBTN;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        System.out.println("THIS IS THE BEGINNING OF THE END");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        OnClickWashingButtonListener();
        OnClickProjectorButtonListener();
        OnClickFyllaButtonListener();
        OnClickWashListButtonListener();

        final MediaPlayer stemmerDetSound = MediaPlayer.create(this, R.raw.stemmerlyd);
        ImageButton playStemmerDetSound = (ImageButton) this.findViewById(R.id.btn_stemmer_det);
        playStemmerDetSound.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                stemmerDetSound.start();
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        MenuInflater menuInflater = getMenuInflater();
        menuInflater.inflate(R.menu.menu_main, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if(item.getItemId() == R.id.settings){
            Intent intent = new Intent(MainActivity.this, LoginActivityWash.class);
            startActivity(intent);
        }
        return super.onOptionsItemSelected(item);
    }

    private void OnClickWashListButtonListener() {
        imgBTN = (ImageButton)findViewById(R.id.BTN_washList);
        imgBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, WashdaysActivity.class);
                startActivity(intent);
            }
        });
    }

    private void OnClickProjectorButtonListener() {
        imgBTN = (ImageButton)findViewById(R.id.BTN_projector);
        imgBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, ProjectorActivity.class);
                startActivity(intent);
            }
        });
    }

    public void OnClickWashingButtonListener(){
        imgBTN = (ImageButton) findViewById(R.id.BTN_washMachine);
        imgBTN.setOnClickListener(
                new View.OnClickListener() {
                     @Override
                     public void onClick(View view) {
                         Intent intent = new Intent(MainActivity.this, WashingMachine.class);
                         startActivity(intent);
                     }
                 }
        );
    }
    public void OnClickFyllaButtonListener(){
        imgBTN = (ImageButton)findViewById(R.id.BTN_fylla);
        imgBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Uri gmmIntentUri = Uri.parse("google.navigation:q=Teknobyen studentboliger,+Klæbuveien+Trondheim=w");
                Intent mapIntent = new Intent(Intent.ACTION_VIEW, gmmIntentUri);
                mapIntent.setPackage("com.google.android.apps.maps");
                startActivity(mapIntent);
            }
        });
    }
}
