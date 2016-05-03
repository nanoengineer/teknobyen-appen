package tbcoders.teknobyen;

import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class ProjectorBookings extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_projector_bookings);
        OnClickBookListener();
    }

    public void OnClickBookListener() {
        Button book_btn = (Button)findViewById(R.id.reserve_btn);
        book_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(ProjectorBookings.this, ProjectorBookForm.class);
                startActivity(intent);
            }
        });
    }
}
