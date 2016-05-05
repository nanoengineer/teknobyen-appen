package tbcoders.teknobyen;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
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
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;
import java.util.TimeZone;

public class ProjectorBookForm extends AppCompatActivity {
    NumberPicker pickHour = null;
    NumberPicker pickMin = null;
    NumberPicker pickDate = null;
    String[] dateValues;
    String[] minValues = {"00", "15", "30", "45"};
    String[] hourValues = {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"};
    String[] weekDays = {"Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"};

    int startDayValue = 0;
    int startHourValue = 0;
    int startMinValue = 0;

    int endDayValue = 5;
    int endHourValue = 23;
    int endMinValue = 45;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_projector_book_form);

        OnClickStartListener();
        OnClickEndListener();
        OnClickReserveListener();
        setupNumberPickers();

    }
    public void setupNumberPickers(){
        pickDate = (NumberPicker)findViewById(R.id.datePicker);
        Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));

        //Veka startar på søndag som har verdi 1, derfor må eg trekke frå 1 når eg skal hente ut dag
        //frå lista over dagar
        int dayOfWeek = cal.get(Calendar.DAY_OF_WEEK) - 1;
        dateValues = new String[6];
        dateValues[0] = "I dag";
        for(int i = 1; i < 6; i++){
            if(dayOfWeek + i > 6){
                dateValues[i] = weekDays[dayOfWeek + i - 7];
            }else{
                dateValues[i] = weekDays[dayOfWeek + i];
            }
        }this.dateValues = dateValues;
        pickDate.setDisplayedValues(dateValues);
        pickDate.setMaxValue(5);
        pickDate.setMinValue(0);
        pickDate.setWrapSelectorWheel(false);

        pickHour = (NumberPicker)findViewById(R.id.startHourPicker);
        pickHour.setDisplayedValues(hourValues);
        pickHour.setMaxValue(23);
        pickHour.setMinValue(0);
        pickHour.setWrapSelectorWheel(false);

        pickMin = (NumberPicker)findViewById(R.id.startMinutePicker);
        pickMin.setMinValue(0);
        pickMin.setMaxValue(3);
        pickMin.setDisplayedValues(minValues);
        pickMin.setWrapSelectorWheel(false);
    }

    public void OnClickStartListener() {
        pickDate = (NumberPicker)findViewById(R.id.datePicker);
        pickHour = (NumberPicker)findViewById(R.id.startHourPicker);
        pickMin = (NumberPicker)findViewById(R.id.startMinutePicker);
        Button btn = (Button)findViewById(R.id.setBookStart);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int sdValue = pickDate.getValue();
                int shValue = pickHour.getValue();
                int smValue = pickMin.getValue();
                int trueMinValue = Integer.parseInt(minValues[smValue]);
                //Kontrollere om starttid er før slutttid før skriving og større enn current time
                if(controlCurrentTime(sdValue, shValue, trueMinValue)){
                    if(sdValue<=endDayValue) {
                        if(sdValue<endDayValue){
                            writeStartTimeMessage();
                        }else{
                            if(shValue<=endHourValue){
                                if(shValue<endHourValue){
                                    writeStartTimeMessage();
                                }
                                else{
                                    if(smValue<endMinValue){
                                        writeStartTimeMessage();
                                    }else{
                                        alertMsg('e');
                                    }
                                }
                            }else{
                                alertMsg('e');
                            }
                        }
                    }else{
                        alertMsg('e');
                    }
                }else{
                    alertMsg('w');
                }
            }
        });
    }
    public boolean controlCurrentTime(int day, int hour, int minute){
        Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
        int currentHour = cal.get(Calendar.HOUR_OF_DAY);
        int currentMin = cal.get(Calendar.MINUTE);
        if(day != 0){
            return true;
        }
        if(hour >= currentHour){
            if(hour > currentHour){
                return true;
            }else{
                if(minute > currentMin){
                    return true;
                }else{
                    return false;
                }
            }
        }else{
            return false;
        }
    }
    public void OnClickEndListener(){
        pickDate = (NumberPicker)findViewById(R.id.datePicker);
        pickHour = (NumberPicker)findViewById(R.id.startHourPicker);
        pickMin = (NumberPicker)findViewById(R.id.startMinutePicker);
        Button btn = (Button)findViewById(R.id.setBookEnd);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Ulovlig å sette slutttid før starttid er satt (slik slepp vi å sjekke med current time)
                TextView startText = (TextView)findViewById(R.id.starttimeString);
                if(startText.getText().toString().length()>0){
                    int sdValue = pickDate.getValue();
                    int shValue = pickHour.getValue();
                    int smValue = pickMin.getValue();
                    if(sdValue>=startDayValue) {
                        if(sdValue>startDayValue){
                            writeEndTimeMessage();
                        }else{
                            if(shValue>=startHourValue){
                                if(shValue>startHourValue){
                                    writeEndTimeMessage();
                                }
                                else{
                                    if(smValue>startMinValue){
                                        writeEndTimeMessage();
                                    }else{
                                        alertMsg('e');
                                    }
                                }
                            }else{
                                alertMsg('e');
                            }
                        }
                    }else{
                        alertMsg('e');
                    }
                }else{
                    Toast.makeText(ProjectorBookForm.this, "Vennligst velg starttid først", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }

    public void writeStartTimeMessage(){
        final TextView startText = (TextView)findViewById(R.id.starttimeString);
        int dayValue = pickDate.getValue();
        startDayValue = dayValue;
        String day = dateValues[dayValue];
        int hourValue = pickHour.getValue();
        String trueHourValue = hourValues[hourValue];
        startHourValue = hourValue;
        int minValue = pickMin.getValue();
        startMinValue = minValue;
        String trueMinValue = minValues[minValue];
        startText.setText(day + " " + trueHourValue + ":" + trueMinValue);
    }
    public void writeEndTimeMessage(){
        final TextView endText = (TextView)findViewById(R.id.endtimeString);
        int dayValue = pickDate.getValue();
        endDayValue = dayValue;
        String day = dateValues[dayValue];
        int hourValue = pickHour.getValue();
        String trueHourValue = hourValues[hourValue];
        endHourValue = hourValue;
        int minValue = pickMin.getValue();
        endMinValue = minValue;
        String trueMinValue = minValues[minValue];
        endText.setText(day + " " + trueHourValue + ":" + trueMinValue);
    }
    private void alertMsg(char c){
        String msg = "";
        if(c == 's'){
            msg = "Verdien for starttid må være mindre enn slutttid";
        }else if(c == 'e'){
            msg = "Verdien for slutttid må være større enn starttid";
        }else if(c == 'w'){
            msg = "Verdien for starttid må være større enn klokkeslettet som er nå";
        }else if(c == 'r'){
            msg = "Vennligst velg starttid, slutttid og fyll inn beskrivelse";
        }
        Toast.makeText(ProjectorBookForm.this, msg, Toast.LENGTH_SHORT).show();
    }
    public void OnClickReserveListener(){
        Button btn = (Button)findViewById(R.id.bookreserveBTN);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                TextView startText = (TextView)findViewById(R.id.starttimeString);
                TextView endText = (TextView)findViewById(R.id.endtimeString);
                EditText bookDesEdit = (EditText)findViewById(R.id.bookDescriptionEdit);
                int startTextLength = startText.getText().length();
                int endTextLength = endText.getText().length();
                int descriptionTextLength = bookDesEdit.getText().toString().length();
                //Kontrollere om alle felt er utfylt før knappen kan trykkast
                if(startTextLength > 0 && endTextLength > 0 && descriptionTextLength > 0){
                    SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
                    Calendar calStart = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
                    calStart.add(Calendar.DATE, startDayValue);
                    String bookStartDate = bookDateFormat.format(calStart.getTime());
                    String bookStartTime = "" + hourValues[startHourValue] + ":" + minValues[startMinValue];

                    Calendar calEnd = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
                    calEnd.add(Calendar.DATE, endDayValue);
                    String bookEndDate = bookDateFormat.format(calEnd.getTime());
                    String bookEndTime = "" + hourValues[endHourValue] + ":" + minValues[endMinValue];


                    String bookText = bookDesEdit.getText().toString();
                    SharedPreferences prefs = getSharedPreferences("mypref", 0);
                    String roomNr = prefs.getString("roomnumber", "");

                    //Toast.makeText(ProjectorBookForm.this, bookStartDate + " " + bookStartTime + " - " + bookEndDate + " " + bookEndTime + " " + bookText, Toast.LENGTH_SHORT).show();


                    writeToFireBase(bookStartDate, bookStartTime, bookEndDate, bookEndTime, roomNr, "4", bookText);
                    Intent intent = new Intent(ProjectorBookForm.this, ProjectorBookings.class);
                    startActivity(intent);
                }else{
                    alertMsg('r');
                }
            }
        });
    }
    public void writeToFireBase(String bookStartDate, String bookStartTime, String bookEndDate, String bookEndTime, String roomNr, String id, String comment){
        Firebase ref = new Firebase("https://teknobyen.firebaseio.com");
        Random rand = new Random();
        //int n = rand.nextInt(50) + 1;
        Firebase newBooking = ref.child("reservations");
        Map<String, String> newBookingMap = new HashMap<String, String>();
        newBookingMap.put("startHour", bookStartTime);
        newBookingMap.put("date", bookStartDate);
        newBookingMap.put("comment", comment);
        newBookingMap.put("roomNumber", roomNr);
        newBookingMap.put("id", id);
        newBookingMap.put("stopHour", bookEndTime);
        newBooking.push().setValue(newBookingMap);
    }
}
