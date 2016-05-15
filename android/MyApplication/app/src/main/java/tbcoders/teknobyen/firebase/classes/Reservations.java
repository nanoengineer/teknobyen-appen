package tbcoders.teknobyen.firebase.classes;

import android.support.v7.app.AppCompatActivity;

import java.text.ParseException;
import java.text.SimpleDateFormat;

/**
 * Created by Alexander on 05/05/2016.
 */
public class Reservations implements Comparable<Reservations> {
    private SimpleDateFormat dateFormat = new SimpleDateFormat("dd.MM.yyyy kk.mm");

    private String name;
    private String date;
    private String userID;
    private String startTime;
    private String comment;
    private String roomNumber;
    private String duration;

    public Reservations() {
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

    public String getStartTime() {
        return startTime;
    }

    public String getDuration() {
        return duration;
    }

    @Override
    public String toString() {
        return roomNumber + ", " + name + "\n" +
                comment + "\n" +
                date + "\t\t" + startTime + ", " + duration + "hrs";
    }

    @Override
    public int compareTo(Reservations another) {
        try {
            return dateFormat.parse(date + " " + startTime).compareTo(dateFormat.parse(another.getDate() + " " + another.getStartTime()));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return 0;
    }
}
