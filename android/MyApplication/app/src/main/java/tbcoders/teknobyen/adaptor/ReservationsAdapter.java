package tbcoders.teknobyen.adaptor;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Random;
import java.util.TimeZone;

import tbcoders.teknobyen.R;
import tbcoders.teknobyen.firebase.classes.Reservations;

/**
 * Created by Alexander on 15/05/2016.
 */
public class ReservationsAdapter extends ArrayAdapter<Reservations>{

    SimpleDateFormat bookDateFormat = new SimpleDateFormat("dd.MM.yyyy");
    String today = bookDateFormat.format(Calendar.getInstance(TimeZone.getTimeZone("Europe/Oslo")).getTime());
    Random random = new Random();

    Context context;
    int layoutResourceId;
    ArrayList<Reservations> data;

    public ReservationsAdapter(Context context, int layoutResourceId, ArrayList<Reservations> data) {
        super(context, layoutResourceId, data);
        this.layoutResourceId = layoutResourceId;
        this.context = context;
        this.data = data;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View row = convertView;
        ReservationsHolder holder = null;

        if(row == null)
        {
            LayoutInflater inflater = ((Activity)context).getLayoutInflater();
            row = inflater.inflate(layoutResourceId, parent, false);

            holder = new ReservationsHolder();
            holder.txtRoomNumber = (TextView) row.findViewById(R.id.txtReservationsRoomNumber);
            holder.txtName = (TextView) row.findViewById(R.id.txtReservationsName);
            holder.txtDate = (TextView) row.findViewById(R.id.txtReservationsDate);
            holder.txtComment = (TextView) row.findViewById(R.id.txtReservationsComment);
            holder.txtStartEnd = (TextView) row.findViewById(R.id.txtReservationsStartEnd);


            row.setTag(holder);
        }
        else
        {
            holder = (ReservationsHolder)row.getTag();
        }

        Reservations reservation = data.get(position);
        holder.txtRoomNumber.setText(reservation.getRoomNumber());
        holder.txtName.setText(reservation.getName());
        holder.txtDate.setText(reservation.getDate());
        holder.txtComment.setText(reservation.getComment());
        holder.txtStartEnd.setText(reservation.getStartTime() + "-" + reservation.getDuration());

        if (today.compareTo(reservation.getDate()) > 0) {
            row.setBackgroundColor(Color.argb(25,255,0,0));
        } else {
            row.setBackgroundColor(Color.argb(25,0,255,0));
        }

        return row;
    }

    static class ReservationsHolder
    {
        TextView txtRoomNumber;
        TextView txtName;
        TextView txtDate;
        TextView txtComment;
        TextView txtStartEnd;
    }
}