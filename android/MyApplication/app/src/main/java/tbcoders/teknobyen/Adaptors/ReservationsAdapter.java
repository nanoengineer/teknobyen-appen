package tbcoders.teknobyen.adaptors;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.ArrayList;

import tbcoders.teknobyen.R;
import tbcoders.teknobyen.firebase.classes.Reservations;

/**
 * Created by Alexander on 15/05/2016.
 */
public class ReservationsAdapter extends ArrayAdapter<Reservations>{

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
            holder.txtStartTime = (TextView) row.findViewById(R.id.txtReservationsStartTime);
            holder.txtDuration = (TextView) row.findViewById(R.id.txtReservationsDuration);


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
        holder.txtStartTime.setText(reservation.getStartTime());
        holder.txtDuration.setText(reservation.getDuration() + "hrs");
        return row;
    }

    static class ReservationsHolder
    {
        TextView txtRoomNumber;
        TextView txtName;
        TextView txtDate;
        TextView txtComment;
        TextView txtStartTime;
        TextView txtDuration;
    }
}