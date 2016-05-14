package tbcoders.teknobyen;

/**
 * Created by Alexander on 14/05/2016.
 */
public class Washdays implements Comparable<Washdays>{


    private long assignment;
    private long roomNumber;
    private String date;

    public Washdays() {
    }

    public long getRoomNumber() {
        return roomNumber;
    }

    public String getDate() {
        return date;
    }

    public long getAssignment() {
        return assignment;
    }

    @Override
    public String toString() {
        return "Washdays{" +
                "assignment='" + assignment + '\'' +
                ", date='" + date + '\'' +
                ", roomNumber='" + roomNumber + '\'' +
                '}';
    }

    @Override
    public int compareTo(Washdays another) {
        return date.compareTo(another.date);
    }
}
