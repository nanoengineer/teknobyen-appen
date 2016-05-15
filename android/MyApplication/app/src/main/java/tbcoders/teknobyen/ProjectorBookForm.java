package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import com.firebase.client.Firebase;

import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;
import java.util.TimeZone;

public class ProjectorBookForm extends AppCompatActivity {
    //Booke form
    private NumberPicker pickHour = null;
    private NumberPicker pickMin = null;
    private NumberPicker pickDate = null;
    private String[] dateValues;
    private final String[] minValues = {"00", "15", "30", "45"};
    private final String[] hourValues = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"};
    private final String[] weekDays = {"Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"};
    private final String[] hourDurations = {"0 timer", "1 time", "2 timer", "3 timer", "4 timer"};
    private final String[] minuteDurations = {"0 min", "15 min", "30 min", "45 min"};
    private int startDayValue = 0;
    private int startHourValue = 0;
    private int startMinValue = 0;
    private String startDayName = "";
    private int durationHour = 0;
    private int durationMin = 0;
    private boolean startMsgSet = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_projector_book_form);
        OnClickStartListener();
        OnClickReserveListener();
        setupNumberPickers();
    }
    public void setupNumberPickers() {
        pickDate = (NumberPicker) findViewById(R.id.datePicker);
        Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
        //Veka startar på søndag som har verdi 1, derfor må eg trekke frå 1 når eg skal hente ut dag
        //frå lista over dagar
        int dayOfWeek = cal.get(Calendar.DAY_OF_WEEK) - 1;
        dateValues = new String[6];
        dateValues[0] = "I dag";
        for (int i = 1; i < 6; i++) {
            if (dayOfWeek + i > 6) {
                dateValues[i] = weekDays[dayOfWeek + i - 7];
            } else {
                dateValues[i] = weekDays[dayOfWeek + i];
            }
        }
        this.dateValues = dateValues;
        pickDate.setDisplayedValues(dateValues);
        pickDate.setMaxValue(5);
        pickDate.setMinValue(0);
        pickDate.setWrapSelectorWheel(false);

        pickHour = (NumberPicker) findViewById(R.id.startHourPicker);
        pickHour.setDisplayedValues(hourValues);
        pickHour.setMaxValue(23);
        pickHour.setMinValue(0);
        pickHour.setWrapSelectorWheel(true);

        pickMin = (NumberPicker) findViewById(R.id.startMinutePicker);
        pickMin.setMinValue(0);
        pickMin.setMaxValue(3);
        pickMin.setDisplayedValues(minValues);
        pickMin.setWrapSelectorWheel(true);
    }

    public void OnClickStartListener() {
        pickDate = (NumberPicker) findViewById(R.id.datePicker);
        pickHour = (NumberPicker) findViewById(R.id.startHourPicker);
        pickMin = (NumberPicker) findViewById(R.id.startMinutePicker);
        Button btn = (Button) findViewById(R.id.setBookStart);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int sdValue = pickDate.getValue();
                int shValue = pickHour.getValue();
                int smValue = pickMin.getValue();
                int trueMinValue = Integer.parseInt(minValues[smValue]);
                //Kontrollere om starttid er før slutttid før skriving og større enn current time
                if(startMsgSet == false){
                    if (controlCurrentTime(sdValue, shValue, trueMinValue)) {
                        writeStartTimeMessage(sdValue, shValue, smValue);
                    } else {
                        Toast.makeText(ProjectorBookForm.this, "Verdi for starttid må være større enn nåværende klokkeslett", Toast.LENGTH_SHORT).show();
                    }
                }else{
                    if(shValue > 0 || smValue > 0){
                        writeEndTimeMessage(shValue, smValue);
                    }else{
                        Toast.makeText(ProjectorBookForm.this, "Varighet må være større enn null", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        });
    }
    public boolean controlCurrentTime(int day, int hour, int minute) {
        Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
        int currentHour = cal.get(Calendar.HOUR_OF_DAY);
        int currentMin = cal.get(Calendar.MINUTE);
        if (day != 0) {
            return true;
        }
        if (hour >= currentHour) {
            if (hour > currentHour) {
                return true;
            } else {
                if (minute > currentMin) {
                    return true;
                }
            }
        }
        return false;
    }

    public void writeStartTimeMessage(int dayValue, int hourValue, int minValue) {
        TextView startText = (TextView) findViewById(R.id.starttimeString);
        startDayValue = dayValue;
        String day = dateValues[dayValue];
        String trueHourValue = hourValues[hourValue];
        startHourValue = hourValue;
        startMinValue = minValue;
        String trueMinValue = minValues[minValue];
        this.startDayName = day;
        startText.setText(day + " " + trueHourValue + ":" + trueMinValue);
        this.startMsgSet = true;

        //Endrar til duration
        pickDate.setVisibility(View.INVISIBLE);
        pickHour.setMaxValue(4);
        pickHour.setDisplayedValues(hourDurations);
        pickMin.setMaxValue(3);
        pickMin.setDisplayedValues(minuteDurations);
        Button btn = (Button) findViewById(R.id.setBookStart);
        btn.setText("Sett varighet");
    }
    public void writeEndTimeMessage(int shValue, int smValue){
        TextView endText = (TextView) findViewById(R.id.endtimeString);
        durationHour = shValue;
        durationMin = smValue;
        int endHourValue;
        int endMinValue;
        if(startHourValue + durationHour >= 24){
            endHourValue = startHourValue + durationHour - 24;
        }else{
            endHourValue = startHourValue + durationHour;
        }
        if(startMinValue + durationMin >= 4){
            endMinValue = startMinValue + durationMin - 4;
            if(endHourValue + 1 >= 24){
                endHourValue -= 23;
            }else{
                endHourValue += 1;
            }
        }else{
            endMinValue = startMinValue + durationMin;
        }
        String endDay = "";
        if(endHourValue < startHourValue){
            int i = Arrays.asList(weekDays).indexOf(startDayName);
            if(i + 1 > 6){
                endDay = weekDays[0];
            }else{
                endDay = weekDays[i+1];
            }
        }else{
            endDay = startDayName;
        }
        endText.setText(endDay + " " + hourValues[endHourValue] + ":" + minValues[endMinValue]);;
    }

    public void OnClickReserveListener() {
        Button btn = (Button) findViewById(R.id.bookreserveBTN);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                TextView startText = (TextView) findViewById(R.id.starttimeString);
                TextView endText = (TextView) findViewById(R.id.endtimeString);
                EditText bookDesEdit = (EditText) findViewById(R.id.bookDescriptionEdit);
                int startTextLength = startText.getText().length();
                int endTextLength = endText.getText().length();
                int descriptionTextLength = bookDesEdit.getText().toString().length();
                //Kontrollere om alle felt er utfylt før knappen kan trykkast
                if (startTextLength > 0 && endTextLength > 0 && descriptionTextLength > 0) {
                    //date
                    SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
                    Calendar calStart = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
                    calStart.add(Calendar.DATE, startDayValue);
                    String bookStartDate = bookDateFormat.format(calStart.getTime());
                    //startHour
                    String bookStartTime = "" + hourValues[startHourValue] + "." + minValues[startMinValue];
                    //description
                    String bookText = bookDesEdit.getText().toString();
                    SharedPreferences prefs = getSharedPreferences("mypref", 0);
                    String roomNr = prefs.getString("roomnumber", "");
                    //duration
                    String duration = "" + durationHour;
                    if(durationMin == 1){
                        duration += ".25";
                    }else if(durationMin == 2){
                        duration += ".5";
                    }else if(durationMin == 3) {
                        duration += ".75";
                    }
                    writeToFireBase(bookStartDate, bookStartTime, duration, roomNr, bookText);
                    Intent intent = new Intent(ProjectorBookForm.this, ProjectorBookings.class);
                    startActivity(intent);
                } else {
                    Toast.makeText(ProjectorBookForm.this, "Vennligst velg starttid, slutttid og varighet", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }

    public void writeToFireBase(String bookStartDate, String bookStartTime, String duration, String roomNr, String comment) {

        // TODO: FIX THIS!!!!
        String userID = "SampleID";
        String name = "Ola Nordmann";

        Firebase ref = new Firebase("https://teknobyen.firebaseio.com");
        Firebase newBooking = ref.child("reservations");
        Map<String, String> newBookingMap = new HashMap<String, String>();

        newBookingMap.put("name", name);
        newBookingMap.put("date", bookStartDate);
        newBookingMap.put("userID", userID);
        newBookingMap.put("startTime", bookStartTime);
        newBookingMap.put("comment", comment);
        newBookingMap.put("roomNumber", roomNr);
        newBookingMap.put("duration", duration);

        newBooking.push().setValue(newBookingMap);

        /*** FORMAT
         userId: AAAA BAAA AAAG AA
         comment: Game of Thrones
         name : Sindre
         roomnumber : 503
         date : 10.05.2016
         startTime : 20:00
         duration : 1.5
         ***/

    }
}