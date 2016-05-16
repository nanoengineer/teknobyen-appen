package tbcoders.teknobyen;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;

import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.ValueEventListener;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.TimeZone;

import tbcoders.teknobyen.adapters.ReservationsAdapter;
import tbcoders.teknobyen.firebase.classes.Reservations;

public class ProjectorActivity extends AppCompatActivity {
    private Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
    private ArrayList<Reservations> reservationList;

    private SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
    private String today = bookDateFormat.format(cal.getTime());

    private ListView bookingView;
    private int scrollposition = -1;

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
                Intent intent = new Intent(ProjectorActivity.this, ProjectorBookActivity.class);
                startActivity(intent);
            }
        });
    }


    private void fillBookings() {
        final Firebase reservationRef = new Firebase("https://teknobyen.firebaseio.com/reservations");

        reservationRef.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot snapshot) {
                if (bookingView != null) {
                    scrollposition = bookingView.getFirstVisiblePosition();
                }
                reservationList = new ArrayList<>();

                for (DataSnapshot postSnapshot : snapshot.getChildren()) {
                    try {
                        Reservations post = postSnapshot.getValue(Reservations.class);
                        reservationList.add(post);

                    } catch (Error e) {
                        System.out.println("Error");
                    }
                }
                fillListView();
                scrollToPosition();
                System.out.println("Done");
            }

            @Override
            public void onCancelled(FirebaseError firebaseError) {
                System.out.println("The read failed: " + firebaseError.getMessage());
            }
        });

    }

    private void fillListView() {
        Collections.sort(reservationList);
        //Collections.reverse(reservationList);
        ArrayAdapter adapter = new ReservationsAdapter(ProjectorActivity.this, R.layout.custom_projector_item, reservationList);
        bookingView = (ListView) findViewById(R.id.bookingView);
        bookingView.setAdapter(adapter);
    }

    private void scrollToPosition() {
        if (scrollposition == -1) {
            for (int i = 0; i < reservationList.size(); i++) {
                if (reservationList.get(i).getDate().equals(today)) {
                    bookingView.setSelection(i);
                    break;
                }
            }
        } else {
            bookingView.setSelection(scrollposition);
        }
    }


}
