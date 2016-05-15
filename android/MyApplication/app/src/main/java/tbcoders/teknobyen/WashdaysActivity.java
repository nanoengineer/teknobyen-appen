package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.firebase.client.DataSnapshot;
import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;
import com.firebase.client.ValueEventListener;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.TimeZone;

import tbcoders.teknobyen.adapters.WashdaysAdapter;
import tbcoders.teknobyen.firebase.classes.Washdays;

/**
 * Created by Alexander on 14/05/2016.
 */
public class WashdaysActivity extends AppCompatActivity {
    Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
    SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
    String today = bookDateFormat.format(cal.getTime());

    ListView bookingView;
    String roomNr;

    ArrayList<Washdays> washdaysList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Firebase.setAndroidContext(this);
        setContentView(R.layout.activity_washlist);
        SharedPreferences prefs = getSharedPreferences("mypref", 0);
        roomNr = prefs.getString("roomnumber", "");
        System.out.println(roomNr);
        bookingView = (ListView) findViewById(R.id.washlistListView);
        fillAccessFirebase();
    }

    private void fillAccessFirebase() {
        final Firebase cleaningRef = new Firebase("https://teknobyen.firebaseio.com/washdays");

        cleaningRef.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot snapshot) {
                washdaysList = new ArrayList<>();

                for (DataSnapshot postSnapshot : snapshot.getChildren()) {
                    try {
                        Washdays post = postSnapshot.getValue(Washdays.class);
                        washdaysList.add(post);
                    } catch (Error e) {
                        System.out.println("Error");
                    }
                }
                fillListView();
                getInfoFromList();
                System.out.println("Done");
            }

            @Override
            public void onCancelled(FirebaseError firebaseError) {
                System.out.println("The read failed: " + firebaseError.getMessage());
            }
        });

    }

    private void fillListView() {
        Collections.sort(washdaysList);
        ArrayAdapter adapter = new WashdaysAdapter(WashdaysActivity.this, R.layout.custom_washlist_item, washdaysList);
        bookingView.setAdapter(adapter);

    }

    private void getInfoFromList() {
        for (int i = 0; i < bookingView.getCount(); i++) {
            if (washdaysList.get(i).getDate().equals(today)) {
                bookingView.setSelection(i);
            }
            if (washdaysList.get(i).getRoomNumber().toString().equals(roomNr)){
                TextView textView1 = (TextView) findViewById(R.id.washlistAssignmentText);
                TextView textView2 = (TextView) findViewById(R.id.washlistDateText);
                textView1.setText("Oppgave: " + washdaysList.get(i).getAssignment().toString());
                textView2.setText(washdaysList.get(i).getPrettyDate());
            }
        }
    }

}
