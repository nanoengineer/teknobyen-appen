package tbcoders.teknobyen;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.NumberPicker;
import android.widget.TextView;
import android.widget.Toast;

import java.util.Arrays;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

public class ProjectorBookForm extends AppCompatActivity {
    NumberPicker pickStartHour = null;
    NumberPicker pickstartMin = null;
    NumberPicker pickDate = null;
    String[] dateValues;
    String[] minValues = {"0", "15", "30", "45", "60"};

    int startDayValue = 0;
    int startHourValue = 0;
    int startMinValue = 0;

    int endDayValue = 0;
    int endHourValue = 0;
    int endMinValue = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_projector_book_form);

        OnClickStartListener();
        OnClickEndListener();

        String[] weekDays = {"Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"};
        pickDate = (NumberPicker)findViewById(R.id.datePicker);
        Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));
        //Veka startar på søndag som har verdi 1, derfor må eg trekke frå 1 når eg skal hente ut dag
        //frå lista over dagar
        int dayOfWeek = cal.get(Calendar.DAY_OF_WEEK) - 1;
        dateValues = new String[6];
        for(int i = 0; i < 6; i++){
            if(dayOfWeek - 1 + i > 6){
                dateValues[i] = weekDays[dayOfWeek + i - 7];
            }else{
                dateValues[i] = weekDays[dayOfWeek - 1 + i];
            }
        }
        this.dateValues = dateValues;
        pickDate.setDisplayedValues(dateValues);
        pickDate.setMaxValue(5);
        pickDate.setMinValue(0);
        pickDate.setWrapSelectorWheel(false);

        pickStartHour = (NumberPicker)findViewById(R.id.startHourPicker);
        pickStartHour.setMaxValue(24);
        pickStartHour.setMinValue(0);
        pickStartHour.setWrapSelectorWheel(false);

        pickstartMin = (NumberPicker)findViewById(R.id.startMinutePicker);

        pickstartMin.setMinValue(0);
        pickstartMin.setMaxValue(4);
        pickstartMin.setDisplayedValues(minValues);
        pickstartMin.setWrapSelectorWheel(false);
    }

    public void OnClickStartListener() {
        Button btn = (Button)findViewById(R.id.setBookStart);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                pickDate = (NumberPicker)findViewById(R.id.datePicker);
                pickStartHour = (NumberPicker)findViewById(R.id.startHourPicker);
                pickstartMin = (NumberPicker)findViewById(R.id.startMinutePicker);
                TextView startText = (TextView)findViewById(R.id.starttimeString);

                int dayValue = pickDate.getValue();
                startDayValue = dayValue;

                String day = dateValues[dayValue];
                int hourValue = pickStartHour.getValue();
                startHourValue = hourValue;

                int minValue = pickstartMin.getValue();
                startMinValue = minValue;
                String trueMinValue = minValues[minValue];
                startText.setText(day + " " + hourValue + ":" + trueMinValue);


            }
        });
    }
    public void OnClickEndListener(){
        pickDate = (NumberPicker)findViewById(R.id.datePicker);
        pickStartHour = (NumberPicker)findViewById(R.id.startHourPicker);
        pickstartMin = (NumberPicker)findViewById(R.id.startMinutePicker);
        final TextView endText = (TextView)findViewById(R.id.endtimeString);
        Button btn = (Button)findViewById(R.id.setBookEnd);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(pickDate.getValue()>=startDayValue) {
                    if(pickDate.getValue()>startDayValue){
                        int dayValue = pickDate.getValue();
                        endDayValue = dayValue;
                        String day = dateValues[dayValue];
                        int hourValue = pickStartHour.getValue();
                        endHourValue = hourValue;
                        int minValue = pickstartMin.getValue();
                        endMinValue = minValue;
                        String trueMinValue = minValues[minValue];
                        endText.setText(day + " " + hourValue + ":" + trueMinValue);
                    }else{
                        if(pickStartHour.getValue()>=startHourValue){
                            if(pickStartHour.getValue()>startHourValue){
                                int dayValue = pickDate.getValue();
                                endDayValue = dayValue;
                                String day = dateValues[dayValue];
                                int hourValue = pickStartHour.getValue();
                                endHourValue = hourValue;
                                int minValue = pickstartMin.getValue();
                                endMinValue = minValue;
                                String trueMinValue = minValues[minValue];
                                endText.setText(day + " " + hourValue + ":" + trueMinValue);
                            }
                            else{
                                if(pickstartMin.getValue()>startMinValue){
                                    int dayValue = pickDate.getValue();
                                    endDayValue = dayValue;
                                    String day = dateValues[dayValue];
                                    int hourValue = pickStartHour.getValue();
                                    endHourValue = hourValue;
                                    int minValue = pickstartMin.getValue();
                                    endMinValue = minValue;
                                    String trueMinValue = minValues[minValue];
                                    endText.setText(day + " " + hourValue + ":" + trueMinValue);
                                }else{
                                    Toast.makeText(ProjectorBookForm.this, "Verdien for slutttid må være større enn starttid", Toast.LENGTH_SHORT).show();
                                }
                            }
                        }else{
                            Toast.makeText(ProjectorBookForm.this, "Verdien for slutttid må være større enn starttid", Toast.LENGTH_SHORT).show();
                        }
                    }
                }else{
                    Toast.makeText(ProjectorBookForm.this, "Verdien for slutttid må være større enn starttid", Toast.LENGTH_SHORT).show();
                }
            }
        });

    }
}
