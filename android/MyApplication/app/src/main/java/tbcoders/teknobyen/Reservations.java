package tbcoders.teknobyen;

/**
 * Created by Alexander on 05/05/2016.
 */
public class Reservations implements Comparable<Reservations>{

    private String userID;
    private String name;
    private String roomnumber;
    private String comment;
    private String date;
    private String startTime;
    private String duration;

    public Reservations() {
    }

    public String getUserID() {
        return userID;
    }

    public String getName() {
        return name;
    }

    public String getRoomnumber() {
        return roomnumber;
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
        return roomnumber + ", " + name + "\n" +
                comment + "\n" +
                date + "\t\t" + startTime + ", " + duration + "hrs";
    }

    @Override
    public int compareTo(Reservations another) {
        int lastCmp = date.compareTo(another.date);
        return (lastCmp != 0 ? lastCmp : startTime.compareTo(another.startTime));
    }
}
