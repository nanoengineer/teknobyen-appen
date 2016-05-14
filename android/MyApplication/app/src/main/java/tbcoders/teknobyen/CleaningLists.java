package tbcoders.teknobyen;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.ArrayAdapter;
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

/**
 * Created by Alexander on 14/05/2016.
 */
public class CleaningLists extends AppCompatActivity {
    Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
    ArrayList<Washdays> washdaysList;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Firebase.setAndroidContext(this);
        setContentView(R.layout.activity_washlist);
        fillBookings();
    }

    private void fillBookings(){
        final Firebase reservationRef = new Firebase("https://teknobyen.firebaseio.com/washdays");

        reservationRef.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot snapshot) {
                washdaysList = new ArrayList<Washdays>();

                System.out.println(snapshot);

                for (DataSnapshot postSnapshot: snapshot.getChildren()) {
                    try{
                        System.out.println(postSnapshot.getValue());
                        for (DataSnapshot bla :
                                postSnapshot.getChildren()) {
                            System.out.println(bla +" " + bla.getValue().getClass());
                        }


                        Washdays post = postSnapshot.getValue(Washdays.class);
                        washdaysList.add(post);

                    }catch (Error e){
                        System.out.println("Error");
                    }
                }
                fillListView();
                System.out.println("Done");
            }
            @Override
            public void onCancelled(FirebaseError firebaseError) {
                System.out.println("The read failed: " + firebaseError.getMessage());
            }
        });

    }
    private void fillListView(){
        Collections.sort(washdaysList);
        //Collections.reverse(washdaysList);
        SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
        String today = bookDateFormat.format(cal.getTime());

        ArrayAdapter adapter = new ArrayAdapter(CleaningLists.this, android.R.layout.simple_list_item_1, washdaysList);
        ListView bookingView = (ListView) findViewById(R.id.washingListView);
        bookingView.setAdapter(adapter);



        for (int i = 0; i < washdaysList.size(); i++) {
            if(washdaysList.get(i).getDate().equals(today)){
                bookingView.setSelection(i);
                break;
            }
        }
    }

}
