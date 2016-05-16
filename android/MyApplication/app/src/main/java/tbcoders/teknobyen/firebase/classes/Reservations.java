package tbcoders.teknobyen.firebase.classes;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.TimeZone;

/**
 * Created by Alexander on 05/05/2016.
 */
public class Reservations implements Comparable<Reservations> {
    private SimpleDateFormat dateFormat = new SimpleDateFormat("dd.MM.yyyy kk.mm");
    private SimpleDateFormat timeFormat = new SimpleDateFormat("kk.mm");
    private Calendar cal = Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo"));

    private String name;
    private String date;
    private String userID;
    private String startTime;
    private String comment;
    private String roomNumber;
    private String duration;

    public Reservations() {
    }
    public Reservations(String name, String userID, String roomNumber){
        this.name = name;
        this.userID = userID;
        this.roomNumber = roomNumber;
    }

    public String getRoomNumber() {
        return roomNumber;
    }

    public String getUserID() {
        return userID;
    }

    public String getName() {
        return name;
    }

    public String getComment() {
        return comment;
    }

    public String getDate() {
        return date;
    }
    public void setDate(String date){
        this.date = date;
    }

    public String getStartTime() {
        return startTime;
    }
    public void setStartTime(String startTime){
        this.startTime = startTime;
    }

    public String getDuration() {
        return duration;
    }
    public void setDuration(String duration){
        this.duration = duration;
    }

    public Calendar getStartCal(){
        int year = Integer.parseInt(date.substring(6, date.length()));
        int month = Integer.parseInt(date.substring(3, 5)) - 1;
        int day = Integer.parseInt(date.substring(0, 2));
        int hour = Integer.parseInt(startTime.substring(0, 2));
        int minute = Integer.parseInt(startTime.substring(3, 5));
        Calendar startCal = new GregorianCalendar(year, month, day, hour, minute);
        return startCal;
    }
    public String getEndTime(){
        Calendar endCal = getStartCal();
        endCal.add(Calendar.MINUTE, (int) (60*Double.parseDouble(duration)));
        SimpleDateFormat hourFormat = new SimpleDateFormat("HH.mm");
        return hourFormat.format(endCal.getTime());
    }

    public Calendar getEndCal(){
        Calendar endCal = getStartCal();
        endCal.add(Calendar.HOUR, Integer.parseInt(duration.substring(0,1)));
        if(duration.length()>1){
            endCal.add(Calendar.MINUTE, (Integer.parseInt(duration.substring(2,duration.length()))*60)/100);
        }
        return endCal;
    }

    @Override
    public int compareTo(Reservations another) {
        try {
            int cmpDate =  dateFormat.parse(date + " " + startTime).compareTo(dateFormat.parse(another.getDate() + " " + another.getStartTime()));
            return cmpDate != 0 ? cmpDate : timeFormat.parse(getEndTime()).compareTo(timeFormat.parse(another.getEndTime()));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return 0;
    }
}
