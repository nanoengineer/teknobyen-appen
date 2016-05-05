package tbcoders.teknobyen;

import android.content.Intent;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.Query;
import com.firebase.client.ValueEventListener;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.Objects;

public class ProjectorBookings extends AppCompatActivity {

    ArrayList<Reservations> reservationList;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Firebase.setAndroidContext(this);
        setContentView(R.layout.activity_projector_bookings);
        OnClickBookListener();
        fillBookings();
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


    private void fillBookings(){
        final Firebase rootRef = new Firebase("https://teknobyen.firebaseio.com");
        final Firebase reservationRef = rootRef.child("reservations");

        reservationRef.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot snapshot) {
                reservationList = new ArrayList<Reservations>();

                reservationRef.child("reservations").child("3").setValue(null);


                for (DataSnapshot postSnapshot: snapshot.getChildren()) {
                    Reservations post = postSnapshot.getValue(Reservations.class);
                    reservationList.add(post);
                    fillListView();
                }
            }
            @Override
            public void onCancelled(FirebaseError firebaseError) {
                System.out.println("The read failed: " + firebaseError.getMessage());
            }
        });

    }
    private void fillListView(){
        Collections.sort(reservationList);
        Collections.reverse(reservationList);
        ArrayAdapter adapter = new ArrayAdapter(ProjectorBookings.this, android.R.layout.simple_list_item_1, reservationList);
        ListView bookingView = (ListView) findViewById(R.id.bookingView);
        bookingView.setAdapter(adapter);
    }


}
