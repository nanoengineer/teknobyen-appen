package tbcoders.teknobyen.firebase.classes;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Locale;

/**
 * Created by Alexander on 14/05/2016.
 */
public class Washdays implements Comparable<Washdays>{

    private SimpleDateFormat dateFormat = new SimpleDateFormat("dd.MM.yyyy");
    private SimpleDateFormat prettyFormat = new SimpleDateFormat("EE dd.MM", new Locale("no"));
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

    public String getPrettyDate() {
        try {
            return prettyFormat.format(dateFormat.parse(date));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public int compareTo(Washdays another) {
        try {
            int cmpDate = dateFormat.parse(date).compareTo(dateFormat.parse(another.getDate()));
            return cmpDate != 0 ? cmpDate : assignment.compareTo(another.getAssignment());
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return 0;
    }
}
