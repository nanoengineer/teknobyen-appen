package tbcoders.teknobyen;

import android.content.res.Resources;

/**
 * Created by Alexander on 05/05/2016.
 */
public class Reservations implements Comparable<Reservations>{
    private String roomNumber;
    private String comment;
    private String date;
    private String startHour;
    private String stopHour;
    private String id;

    public Reservations() {
    }

    public String getRoomNumber() {
        return roomNumber;
    }

    public String getComment() {
        return comment;
    }

    public String getDate() {
        return date;
    }

    public String getStartHour() {
        return startHour;
    }

    public String getStopHour() {
        return stopHour;
    }

    public String getId() {
        return id;
    }

    @Override
    public String toString() {
        return "Room " + roomNumber + "\n" +
                comment + "\n" +
                date + "\t\t" + startHour + " - " + stopHour;
    }

    @Override
    public int compareTo(Reservations another) {
        int lastCmp = date.compareTo(another.date);
        return (lastCmp != 0 ? lastCmp : startHour.compareTo(another.startHour));
    }
}
