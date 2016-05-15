package tbcoders.teknobyen.firebase.classes;

import java.text.ParseException;
import java.text.SimpleDateFormat;

/**
 * Created by Alexander on 14/05/2016.
 */
public class Washdays implements Comparable<Washdays>{

    private SimpleDateFormat f = new SimpleDateFormat("dd.MM.yyyy");
    private Long assignment;
    private Long roomNumber;
    private String date;

    public Washdays() {
    }

    public Long getRoomNumber() {
        return roomNumber;
    }

    public String getDate() {
        return date;
    }

    public Long getAssignment() {
        return assignment;
    }

    @Override
    public String toString() {
        return date + "\t\t\t" + assignment + "\t\t" + roomNumber;
    }

    @Override
    public int compareTo(Washdays another) {
        try {
            int cmpDate = f.parse(date).compareTo(f.parse(another.getDate()));
            return cmpDate != 0 ? cmpDate : assignment.compareTo(another.getAssignment());
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return 0;
    }
}
